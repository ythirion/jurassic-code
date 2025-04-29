using JurassicCode.Domain.Events;
using JurassicCode.Domain.Exceptions;
using JurassicCode.Domain.ValueObjects;

namespace JurassicCode.Domain.Entities;

/// <summary>
/// Zone aggregate root representing a containment area within the park
/// </summary>
public class Zone
{
    private readonly List<Dinosaur> _dinosaurs = new();
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public string Name { get; }
    public ZoneStatus Status { get; private set; }
    public IReadOnlyCollection<Dinosaur> Dinosaurs => _dinosaurs.AsReadOnly();
    
    // Constructor for creating a new zone
    public Zone(string name, ZoneStatus initialStatus = ZoneStatus.Closed)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ZoneDomainException("Zone name cannot be empty");
            
        Name = name;
        Status = initialStatus;
        
        _domainEvents.Add(new ZoneCreatedEvent(this));
    }
    
    // Constructor for reconstructing from persistence
    private Zone(string name, ZoneStatus status, IEnumerable<Dinosaur> dinosaurs)
    {
        Name = name;
        Status = status;
        _dinosaurs.AddRange(dinosaurs);
    }
    
    public static Zone Reconstruct(string name, ZoneStatus status, IEnumerable<Dinosaur> dinosaurs)
    {
        return new Zone(name, status, dinosaurs);
    }
    
    // Domain behaviors
    public bool Open()
    {
        if (Status == ZoneStatus.Open)
            return false;
            
        Status = ZoneStatus.Open;
        _domainEvents.Add(new ZoneStatusChangedEvent(this));
        return true;
    }
    
    public bool Close()
    {
        if (Status == ZoneStatus.Closed)
            return false;
            
        Status = ZoneStatus.Closed;
        _domainEvents.Add(new ZoneStatusChangedEvent(this));
        return true;
    }
    
    public bool ToggleStatus()
    {
        return Status == ZoneStatus.Open ? Close() : Open();
    }
    
    public bool AddDinosaur(Dinosaur dinosaur)
    {
        if (dinosaur == null)
            throw new ZoneDomainException("Cannot add a null dinosaur to zone");
            
        if (Status == ZoneStatus.Closed)
            throw new ZoneDomainException($"Cannot add dinosaur to closed zone '{Name}'");
            
        if (_dinosaurs.Any(d => d.Name == dinosaur.Name))
            throw new ZoneDomainException($"Dinosaur '{dinosaur.Name}' already exists in zone '{Name}'");
            
        // Check compatibility with existing dinosaurs
        foreach (var existingDinosaur in _dinosaurs)
        {
            if (!dinosaur.CanCoexistWith(existingDinosaur))
                throw new ZoneDomainException($"Dinosaur '{dinosaur.Name}' cannot coexist with '{existingDinosaur.Name}' in zone '{Name}'");
        }
        
        _dinosaurs.Add(dinosaur);
        _domainEvents.Add(new DinosaurAddedToZoneEvent(this, dinosaur));
        return true;
    }
    
    public bool RemoveDinosaur(string dinosaurName)
    {
        var dinosaur = _dinosaurs.FirstOrDefault(d => d.Name == dinosaurName);
        if (dinosaur == null)
            return false;
            
        var removed = _dinosaurs.Remove(dinosaur);
        if (removed)
            _domainEvents.Add(new DinosaurRemovedFromZoneEvent(this, dinosaur));
            
        return removed;
    }
    
    public Dinosaur? FindDinosaur(string name)
    {
        return _dinosaurs.FirstOrDefault(d => d.Name == name);
    }
    
    public bool ContainsDinosaur(string name)
    {
        return _dinosaurs.Any(d => d.Name == name);
    }
    
    public bool IsEmpty()
    {
        return !_dinosaurs.Any();
    }
    
    public int CountDinosaurs()
    {
        return _dinosaurs.Count;
    }
    
    public int CountDangerousDinosaurs()
    {
        return _dinosaurs.Count(d => d.IsDangerous());
    }
    
    public int CountSickDinosaurs()
    {
        return _dinosaurs.Count(d => d.HealthStatus == HealthStatus.Sick);
    }
    
    public bool IsDangerous()
    {
        return CountDangerousDinosaurs() > 0;
    }
    
    // Domain events handling
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    
    // Equality based on identity
    public override bool Equals(object? obj)
    {
        if (obj is not Zone other)
            return false;
            
        return Name == other.Name;
    }
    
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
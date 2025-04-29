using JurassicCode.Domain.Events;
using JurassicCode.Domain.Exceptions;
using JurassicCode.Domain.ValueObjects;

namespace JurassicCode.Domain.Entities;

/// <summary>
/// Dinosaur aggregate root representing a dinosaur in the park
/// </summary>
public class Dinosaur
{
    private readonly List<IDomainEvent> _domainEvents = new();
    
    public string Name { get; }
    public SpeciesType Species { get; }
    public HealthStatus HealthStatus { get; private set; }
    public DateTime LastFed { get; private set; }
    
    // Constructor for creating a new dinosaur
    public Dinosaur(string name, SpeciesType species)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DinosaurDomainException("Dinosaur name cannot be empty");
        
        if (species == null)
            throw new DinosaurDomainException("Dinosaur species cannot be null");
            
        Name = name;
        Species = species;
        HealthStatus = HealthStatus.Healthy;
        LastFed = DateTime.Now;
        
        _domainEvents.Add(new DinosaurCreatedEvent(this));
    }
    
    // Constructor for reconstructing from persistence
    private Dinosaur(string name, SpeciesType species, HealthStatus healthStatus, DateTime lastFed)
    {
        Name = name;
        Species = species;
        HealthStatus = healthStatus;
        LastFed = lastFed;
    }
    
    public static Dinosaur Reconstruct(string name, string speciesName, HealthStatus healthStatus, DateTime lastFed)
    {
        var species = SpeciesType.FromName(speciesName);
        return new Dinosaur(name, species, healthStatus, lastFed);
    }
    
    // Domain behaviors - Note the "Tell, Don't Ask" principle
    public void MarkAsSick()
    {
        if (HealthStatus != HealthStatus.Sick)
        {
            HealthStatus = HealthStatus.Sick;
            _domainEvents.Add(new DinosaurHealthChangedEvent(this));
        }
    }
    
    public void MarkAsHealthy()
    {
        if (HealthStatus != HealthStatus.Healthy)
        {
            HealthStatus = HealthStatus.Healthy;
            _domainEvents.Add(new DinosaurHealthChangedEvent(this));
        }
    }
    
    public void Feed()
    {
        LastFed = DateTime.Now;
        _domainEvents.Add(new DinosaurFedEvent(this));
    }
    
    public bool CanCoexistWith(Dinosaur other)
    {
        return SpeciesType.AreCompatible(Species, other.Species);
    }
    
    public bool NeedsFeeding()
    {
        return DateTime.Now - LastFed > TimeSpan.FromHours(8);
    }
    
    public string GetFormattedFeedingTime()
    {
        return LastFed.ToString("g");
    }
    
    public TimeSpan GetTimeSinceLastFed()
    {
        return DateTime.Now - LastFed;
    }
    
    public bool IsDangerous()
    {
        return Species.Diet == DietType.Carnivore;
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
        if (obj is not Dinosaur other)
            return false;
            
        return Name == other.Name;
    }
    
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
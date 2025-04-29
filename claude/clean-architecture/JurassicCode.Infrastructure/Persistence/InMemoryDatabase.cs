using JurassicCode.Domain.Entities;

namespace JurassicCode.Infrastructure.Persistence;

/// <summary>
/// In-memory database for storing zones and dinosaurs
/// </summary>
public class InMemoryDatabase
{
    private readonly Dictionary<string, Zone> _zones = new();
    private readonly Dictionary<string, Dinosaur> _dinosaurs = new();
    
    public IReadOnlyDictionary<string, Zone> Zones => _zones;
    public IReadOnlyDictionary<string, Dinosaur> Dinosaurs => _dinosaurs;
    
    public void AddZone(Zone zone)
    {
        if (zone == null)
            throw new ArgumentNullException(nameof(zone));
            
        _zones[zone.Name] = zone;
    }
    
    public void UpdateZone(Zone zone)
    {
        if (zone == null)
            throw new ArgumentNullException(nameof(zone));
            
        _zones[zone.Name] = zone;
    }
    
    public void AddDinosaur(Dinosaur dinosaur)
    {
        if (dinosaur == null)
            throw new ArgumentNullException(nameof(dinosaur));
            
        _dinosaurs[dinosaur.Name] = dinosaur;
    }
    
    public void UpdateDinosaur(Dinosaur dinosaur)
    {
        if (dinosaur == null)
            throw new ArgumentNullException(nameof(dinosaur));
            
        _dinosaurs[dinosaur.Name] = dinosaur;
    }
    
    public void RemoveDinosaur(string dinosaurName)
    {
        if (string.IsNullOrWhiteSpace(dinosaurName))
            throw new ArgumentException("Dinosaur name cannot be empty", nameof(dinosaurName));
            
        _dinosaurs.Remove(dinosaurName);
    }
}
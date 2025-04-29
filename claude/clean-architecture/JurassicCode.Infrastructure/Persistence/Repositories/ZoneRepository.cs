using JurassicCode.Application.Interfaces;
using JurassicCode.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace JurassicCode.Infrastructure.Persistence.Repositories;

/// <summary>
/// In-memory repository implementation for Zone aggregate
/// </summary>
public class ZoneRepository : IZoneRepository
{
    private readonly InMemoryDatabase _database;
    private readonly ILogger<ZoneRepository> _logger;
    
    public ZoneRepository(InMemoryDatabase database, ILogger<ZoneRepository> logger)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public Task<Zone?> GetByNameAsync(string zoneName)
    {
        _logger.LogDebug("Getting zone with name {ZoneName}", zoneName);
        
        if (_database.Zones.TryGetValue(zoneName, out var zone))
            return Task.FromResult<Zone?>(zone);
            
        return Task.FromResult<Zone?>(null);
    }
    
    public Task<IEnumerable<Zone>> GetAllAsync()
    {
        _logger.LogDebug("Getting all zones");
        return Task.FromResult<IEnumerable<Zone>>(_database.Zones.Values);
    }
    
    public Task<bool> ExistsAsync(string zoneName)
    {
        return Task.FromResult(_database.Zones.ContainsKey(zoneName));
    }
    
    public Task AddAsync(Zone zone)
    {
        if (zone == null)
            throw new ArgumentNullException(nameof(zone));
            
        _logger.LogDebug("Adding zone {ZoneName}", zone.Name);
        _database.AddZone(zone);
        
        return Task.CompletedTask;
    }
    
    public Task UpdateAsync(Zone zone)
    {
        if (zone == null)
            throw new ArgumentNullException(nameof(zone));
            
        _logger.LogDebug("Updating zone {ZoneName}", zone.Name);
        _database.UpdateZone(zone);
        
        return Task.CompletedTask;
    }
}
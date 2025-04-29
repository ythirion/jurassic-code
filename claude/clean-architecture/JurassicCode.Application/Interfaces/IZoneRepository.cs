using JurassicCode.Domain.Entities;

namespace JurassicCode.Application.Interfaces;

/// <summary>
/// Repository interface for Zone aggregate
/// </summary>
public interface IZoneRepository
{
    Task<Zone?> GetByNameAsync(string zoneName);
    Task<IEnumerable<Zone>> GetAllAsync();
    Task<bool> ExistsAsync(string zoneName);
    Task AddAsync(Zone zone);
    Task UpdateAsync(Zone zone);
}
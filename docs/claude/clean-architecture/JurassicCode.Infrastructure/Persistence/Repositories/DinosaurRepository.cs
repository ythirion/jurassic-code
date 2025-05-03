using JurassicCode.Application.Interfaces;
using JurassicCode.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace JurassicCode.Infrastructure.Persistence.Repositories;

/// <summary>
/// In-memory repository implementation for Dinosaur aggregate
/// </summary>
public class DinosaurRepository : IDinosaurRepository
{
    private readonly InMemoryDatabase _database;
    private readonly ILogger<DinosaurRepository> _logger;
    
    public DinosaurRepository(InMemoryDatabase database, ILogger<DinosaurRepository> logger)
    {
        _database = database ?? throw new ArgumentNullException(nameof(database));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public Task<Dinosaur?> GetByNameAsync(string dinosaurName)
    {
        _logger.LogDebug("Getting dinosaur with name {DinosaurName}", dinosaurName);
        
        if (_database.Dinosaurs.TryGetValue(dinosaurName, out var dinosaur))
            return Task.FromResult<Dinosaur?>(dinosaur);
            
        return Task.FromResult<Dinosaur?>(null);
    }
    
    public Task<IEnumerable<Dinosaur>> GetAllAsync()
    {
        _logger.LogDebug("Getting all dinosaurs");
        return Task.FromResult<IEnumerable<Dinosaur>>(_database.Dinosaurs.Values);
    }
    
    public Task<bool> ExistsAsync(string dinosaurName)
    {
        return Task.FromResult(_database.Dinosaurs.ContainsKey(dinosaurName));
    }
    
    public Task AddAsync(Dinosaur dinosaur)
    {
        if (dinosaur == null)
            throw new ArgumentNullException(nameof(dinosaur));
            
        _logger.LogDebug("Adding dinosaur {DinosaurName}", dinosaur.Name);
        _database.AddDinosaur(dinosaur);
        
        return Task.CompletedTask;
    }
    
    public Task UpdateAsync(Dinosaur dinosaur)
    {
        if (dinosaur == null)
            throw new ArgumentNullException(nameof(dinosaur));
            
        _logger.LogDebug("Updating dinosaur {DinosaurName}", dinosaur.Name);
        _database.UpdateDinosaur(dinosaur);
        
        return Task.CompletedTask;
    }
    
    public Task RemoveAsync(string dinosaurName)
    {
        _logger.LogDebug("Removing dinosaur {DinosaurName}", dinosaurName);
        _database.RemoveDinosaur(dinosaurName);
        
        return Task.CompletedTask;
    }
}
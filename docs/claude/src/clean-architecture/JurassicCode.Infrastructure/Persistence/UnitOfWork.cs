using JurassicCode.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace JurassicCode.Infrastructure.Persistence;

/// <summary>
/// Unit of work implementation for coordinating transactions across repositories
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly ILogger<UnitOfWork> _logger;
    
    public UnitOfWork(
        IZoneRepository zoneRepository,
        IDinosaurRepository dinosaurRepository,
        ILogger<UnitOfWork> logger)
    {
        ZoneRepository = zoneRepository ?? throw new ArgumentNullException(nameof(zoneRepository));
        DinosaurRepository = dinosaurRepository ?? throw new ArgumentNullException(nameof(dinosaurRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public IZoneRepository ZoneRepository { get; }
    public IDinosaurRepository DinosaurRepository { get; }
    
    public Task<bool> SaveChangesAsync()
    {
        // Since we're using an in-memory database, there's no actual transaction to commit
        // In a real implementation, this would commit the transaction
        _logger.LogDebug("Saving changes to database");
        return Task.FromResult(true);
    }
}
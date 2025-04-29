using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Common;
using JurassicCode.Domain.Entities;
using JurassicCode.Domain.Exceptions;

namespace JurassicCode.Application.UseCases.Zones;

/// <summary>
/// Use case for toggling a zone's status (open/closed)
/// </summary>
public class ToggleZoneStatusUseCase : IUseCase<string, Zone>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _eventDispatcher;
    
    public ToggleZoneStatusUseCase(
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
    }
    
    public async Task<Zone> ExecuteAsync(string zoneName)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
            
        // Get zone
        var zone = await _unitOfWork.ZoneRepository.GetByNameAsync(zoneName);
        
        if (zone == null)
            throw new ZoneDomainException($"Zone '{zoneName}' not found");
            
        // Toggle zone status
        zone.ToggleStatus();
            
        // Save changes
        await _unitOfWork.ZoneRepository.UpdateAsync(zone);
        await _unitOfWork.SaveChangesAsync();
        
        // Dispatch domain events
        await _eventDispatcher.DispatchEventsAsync(zone.DomainEvents);
        zone.ClearDomainEvents();
        
        return zone;
    }
}
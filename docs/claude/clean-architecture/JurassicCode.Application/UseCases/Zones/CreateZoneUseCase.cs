using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Common;
using JurassicCode.Domain.Entities;
using JurassicCode.Domain.Exceptions;
using JurassicCode.Domain.ValueObjects;

namespace JurassicCode.Application.UseCases.Zones;

public class CreateZoneRequest
{
    public string Name { get; }
    public bool IsOpen { get; }
    
    public CreateZoneRequest(string name, bool isOpen = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Zone name cannot be empty", nameof(name));
            
        Name = name;
        IsOpen = isOpen;
    }
}

/// <summary>
/// Use case for creating a new zone
/// </summary>
public class CreateZoneUseCase : IUseCase<CreateZoneRequest, Zone>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _eventDispatcher;
    
    public CreateZoneUseCase(
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
    }
    
    public async Task<Zone> ExecuteAsync(CreateZoneRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
            
        // Check if zone with the same name already exists
        if (await _unitOfWork.ZoneRepository.ExistsAsync(request.Name))
            throw new ZoneDomainException($"Zone with name '{request.Name}' already exists");
            
        // Create new zone
        var zone = new Zone(
            request.Name, 
            request.IsOpen ? ZoneStatus.Open : ZoneStatus.Closed);
            
        // Save zone to repository
        await _unitOfWork.ZoneRepository.AddAsync(zone);
        await _unitOfWork.SaveChangesAsync();
        
        // Dispatch domain events
        await _eventDispatcher.DispatchEventsAsync(zone.DomainEvents);
        zone.ClearDomainEvents();
        
        return zone;
    }
}
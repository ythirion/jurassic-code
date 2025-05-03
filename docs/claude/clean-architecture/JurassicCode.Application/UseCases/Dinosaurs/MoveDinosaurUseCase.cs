using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Common;
using JurassicCode.Domain.Entities;
using JurassicCode.Domain.Exceptions;

namespace JurassicCode.Application.UseCases.Dinosaurs;

public class MoveDinosaurRequest
{
    public string DinosaurName { get; }
    public string FromZoneName { get; }
    public string ToZoneName { get; }
    
    public MoveDinosaurRequest(string dinosaurName, string fromZoneName, string toZoneName)
    {
        if (string.IsNullOrWhiteSpace(dinosaurName))
            throw new ArgumentException("Dinosaur name cannot be empty", nameof(dinosaurName));
            
        if (string.IsNullOrWhiteSpace(fromZoneName))
            throw new ArgumentException("Source zone name cannot be empty", nameof(fromZoneName));
            
        if (string.IsNullOrWhiteSpace(toZoneName))
            throw new ArgumentException("Destination zone name cannot be empty", nameof(toZoneName));
            
        DinosaurName = dinosaurName;
        FromZoneName = fromZoneName;
        ToZoneName = toZoneName;
    }
}

/// <summary>
/// Use case for moving a dinosaur from one zone to another
/// </summary>
public class MoveDinosaurUseCase : IUseCase<MoveDinosaurRequest, Dinosaur>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _eventDispatcher;
    
    public MoveDinosaurUseCase(
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
    }
    
    public async Task<Dinosaur> ExecuteAsync(MoveDinosaurRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
        
        // Get source zone
        var sourceZone = await _unitOfWork.ZoneRepository.GetByNameAsync(request.FromZoneName);
        if (sourceZone == null)
            throw new ZoneDomainException($"Source zone '{request.FromZoneName}' not found");
        
        // Get destination zone
        var destinationZone = await _unitOfWork.ZoneRepository.GetByNameAsync(request.ToZoneName);
        if (destinationZone == null)
            throw new ZoneDomainException($"Destination zone '{request.ToZoneName}' not found");
        
        // Check that destination zone is open
        if (destinationZone.Status != Domain.ValueObjects.ZoneStatus.Open)
            throw new ZoneDomainException($"Cannot move dinosaur to closed zone '{request.ToZoneName}'");
        
        // Get dinosaur
        var dinosaur = sourceZone.FindDinosaur(request.DinosaurName);
        if (dinosaur == null)
            throw new DinosaurDomainException($"Dinosaur '{request.DinosaurName}' not found in zone '{request.FromZoneName}'");
        
        // Check compatibility with dinosaurs in destination zone
        foreach (var existingDino in destinationZone.Dinosaurs)
        {
            if (!dinosaur.CanCoexistWith(existingDino))
            {
                throw new ZoneDomainException(
                    $"Dinosaur '{dinosaur.Name}' cannot coexist with '{existingDino.Name}' in zone '{destinationZone.Name}'");
            }
        }
        
        // Remove from source zone
        sourceZone.RemoveDinosaur(request.DinosaurName);
        
        // Add to destination zone
        destinationZone.AddDinosaur(dinosaur);
        
        // Save changes
        await _unitOfWork.ZoneRepository.UpdateAsync(sourceZone);
        await _unitOfWork.ZoneRepository.UpdateAsync(destinationZone);
        await _unitOfWork.SaveChangesAsync();
        
        // Dispatch domain events
        var allEvents = sourceZone.DomainEvents.Concat(destinationZone.DomainEvents);
        await _eventDispatcher.DispatchEventsAsync(allEvents);
        
        sourceZone.ClearDomainEvents();
        destinationZone.ClearDomainEvents();
        
        return dinosaur;
    }
}
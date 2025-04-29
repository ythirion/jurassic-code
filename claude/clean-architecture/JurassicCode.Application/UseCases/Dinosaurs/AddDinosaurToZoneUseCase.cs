using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Common;
using JurassicCode.Domain.Entities;
using JurassicCode.Domain.Exceptions;
using JurassicCode.Domain.ValueObjects;

namespace JurassicCode.Application.UseCases.Dinosaurs;

public class AddDinosaurToZoneRequest
{
    public string ZoneName { get; }
    public string DinosaurName { get; }
    public string SpeciesName { get; }
    
    public AddDinosaurToZoneRequest(string zoneName, string dinosaurName, string speciesName)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
            
        if (string.IsNullOrWhiteSpace(dinosaurName))
            throw new ArgumentException("Dinosaur name cannot be empty", nameof(dinosaurName));
            
        if (string.IsNullOrWhiteSpace(speciesName))
            throw new ArgumentException("Species name cannot be empty", nameof(speciesName));
            
        ZoneName = zoneName;
        DinosaurName = dinosaurName;
        SpeciesName = speciesName;
    }
}

/// <summary>
/// Use case for adding a new dinosaur to a zone
/// </summary>
public class AddDinosaurToZoneUseCase : IUseCase<AddDinosaurToZoneRequest, Dinosaur>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _eventDispatcher;
    
    public AddDinosaurToZoneUseCase(
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher eventDispatcher)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
    }
    
    public async Task<Dinosaur> ExecuteAsync(AddDinosaurToZoneRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
            
        // Check if dinosaur with the same name already exists
        if (await _unitOfWork.DinosaurRepository.ExistsAsync(request.DinosaurName))
            throw new DinosaurDomainException($"Dinosaur with name '{request.DinosaurName}' already exists");
            
        // Get zone
        var zone = await _unitOfWork.ZoneRepository.GetByNameAsync(request.ZoneName);
        
        if (zone == null)
            throw new ZoneDomainException($"Zone '{request.ZoneName}' not found");
            
        // Create new dinosaur
        var speciesType = SpeciesType.FromName(request.SpeciesName);
        var dinosaur = new Dinosaur(request.DinosaurName, speciesType);
        
        // Add dinosaur to zone
        zone.AddDinosaur(dinosaur);
        
        // Save changes
        await _unitOfWork.DinosaurRepository.AddAsync(dinosaur);
        await _unitOfWork.ZoneRepository.UpdateAsync(zone);
        await _unitOfWork.SaveChangesAsync();
        
        // Dispatch domain events from both dinosaur and zone
        var allEvents = dinosaur.DomainEvents.Concat(zone.DomainEvents);
        await _eventDispatcher.DispatchEventsAsync(allEvents);
        
        dinosaur.ClearDomainEvents();
        zone.ClearDomainEvents();
        
        return dinosaur;
    }
}
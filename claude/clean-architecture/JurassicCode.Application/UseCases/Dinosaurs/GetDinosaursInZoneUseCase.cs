using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Common;
using JurassicCode.Domain.Entities;
using JurassicCode.Domain.Exceptions;

namespace JurassicCode.Application.UseCases.Dinosaurs;

/// <summary>
/// Use case for retrieving all dinosaurs in a specific zone
/// </summary>
public class GetDinosaursInZoneUseCase : IUseCase<string, IEnumerable<Dinosaur>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetDinosaursInZoneUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    
    public async Task<IEnumerable<Dinosaur>> ExecuteAsync(string zoneName)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
            
        var zone = await _unitOfWork.ZoneRepository.GetByNameAsync(zoneName);
        
        if (zone == null)
            throw new ZoneDomainException($"Zone '{zoneName}' not found");
            
        return zone.Dinosaurs;
    }
}
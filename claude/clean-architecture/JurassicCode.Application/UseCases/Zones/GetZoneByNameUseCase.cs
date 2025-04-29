using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Common;
using JurassicCode.Domain.Entities;
using JurassicCode.Domain.Exceptions;

namespace JurassicCode.Application.UseCases.Zones;

/// <summary>
/// Use case for retrieving a specific zone by name
/// </summary>
public class GetZoneByNameUseCase : IUseCase<string, Zone>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetZoneByNameUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    
    public async Task<Zone> ExecuteAsync(string zoneName)
    {
        if (string.IsNullOrWhiteSpace(zoneName))
            throw new ArgumentException("Zone name cannot be empty", nameof(zoneName));
            
        var zone = await _unitOfWork.ZoneRepository.GetByNameAsync(zoneName);
        
        if (zone == null)
            throw new ZoneDomainException($"Zone '{zoneName}' not found");
            
        return zone;
    }
}
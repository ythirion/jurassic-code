using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Common;
using JurassicCode.Domain.Entities;

namespace JurassicCode.Application.UseCases.Zones;

/// <summary>
/// Use case for retrieving all zones in the park
/// </summary>
public class GetAllZonesUseCase : IUseCase<IEnumerable<Zone>>
{
    private readonly IUnitOfWork _unitOfWork;
    
    public GetAllZonesUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    
    public async Task<IEnumerable<Zone>> ExecuteAsync()
    {
        return await _unitOfWork.ZoneRepository.GetAllAsync();
    }
}
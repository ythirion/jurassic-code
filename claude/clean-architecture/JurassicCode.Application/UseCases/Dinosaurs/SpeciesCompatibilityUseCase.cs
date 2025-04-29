using JurassicCode.Application.UseCases.Common;
using JurassicCode.Domain.ValueObjects;

namespace JurassicCode.Application.UseCases.Dinosaurs;

public class SpeciesCompatibilityRequest
{
    public string Species1Name { get; }
    public string Species2Name { get; }
    
    public SpeciesCompatibilityRequest(string species1Name, string species2Name)
    {
        if (string.IsNullOrWhiteSpace(species1Name))
            throw new ArgumentException("First species name cannot be empty", nameof(species1Name));
            
        if (string.IsNullOrWhiteSpace(species2Name))
            throw new ArgumentException("Second species name cannot be empty", nameof(species2Name));
            
        Species1Name = species1Name;
        Species2Name = species2Name;
    }
}

/// <summary>
/// Use case for checking compatibility between different dinosaur species
/// </summary>
public class SpeciesCompatibilityUseCase : IUseCase<SpeciesCompatibilityRequest, bool>
{
    public Task<bool> ExecuteAsync(SpeciesCompatibilityRequest request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));
            
        return Task.FromResult(SpeciesType.AreCompatible(request.Species1Name, request.Species2Name));
    }
}
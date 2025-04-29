using JurassicCode.Domain.Entities;

namespace JurassicCode.Application.Interfaces;

/// <summary>
/// Repository interface for Dinosaur aggregate
/// </summary>
public interface IDinosaurRepository
{
    Task<Dinosaur?> GetByNameAsync(string dinosaurName);
    Task<IEnumerable<Dinosaur>> GetAllAsync();
    Task<bool> ExistsAsync(string dinosaurName);
    Task AddAsync(Dinosaur dinosaur);
    Task UpdateAsync(Dinosaur dinosaur);
    Task RemoveAsync(string dinosaurName);
}
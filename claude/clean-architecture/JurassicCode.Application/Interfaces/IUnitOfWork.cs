namespace JurassicCode.Application.Interfaces;

/// <summary>
/// Unit of work interface to manage transactions across repositories
/// </summary>
public interface IUnitOfWork
{
    IZoneRepository ZoneRepository { get; }
    IDinosaurRepository DinosaurRepository { get; }
    
    Task<bool> SaveChangesAsync();
}
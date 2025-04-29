using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Dinosaurs;
using JurassicCode.Application.UseCases.Zones;
using JurassicCode.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace JurassicCode.Infrastructure.Persistence;

/// <summary>
/// Seeds the database with initial data
/// </summary>
public class DatabaseSeeder
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _eventDispatcher;
    private readonly ILogger<DatabaseSeeder> _logger;
    
    public DatabaseSeeder(
        IUnitOfWork unitOfWork,
        IDomainEventDispatcher eventDispatcher,
        ILogger<DatabaseSeeder> logger)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task SeedAsync()
    {
        _logger.LogInformation("Seeding database with initial data");
        
        // Create use cases
        var createZoneUseCase = new CreateZoneUseCase(_unitOfWork, _eventDispatcher);
        var addDinosaurUseCase = new AddDinosaurToZoneUseCase(_unitOfWork, _eventDispatcher);
        
        // Create initial zones
        await createZoneUseCase.ExecuteAsync(new CreateZoneRequest("Ismaloya Mountains", true));
        await createZoneUseCase.ExecuteAsync(new CreateZoneRequest("Western Ridge", true));
        await createZoneUseCase.ExecuteAsync(new CreateZoneRequest("Eastern Ridge", true));
        
        // Add dinosaurs to zones
        await AddIsmaloyaMountainsDinosaurs(addDinosaurUseCase);
        await AddWesternRidgeDinosaurs(addDinosaurUseCase);
        await AddEasternRidgeDinosaurs(addDinosaurUseCase);
        
        _logger.LogInformation("Database seeding completed");
    }
    
    private async Task AddIsmaloyaMountainsDinosaurs(AddDinosaurToZoneUseCase useCase)
    {
        var zoneName = "Ismaloya Mountains";
        
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Rexy", "T-Rex"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Bucky", "Triceratops"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Echo", "Velociraptor"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Brachio", "Brachiosaurus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Dilo", "Dilophosaurus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Para", "Parasaurolophus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Galli", "Gallimimus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Stego", "Stegosaurus"));
        
        // Make a dinosaur sick for demo purposes
        var zone = await _unitOfWork.ZoneRepository.GetByNameAsync(zoneName);
        var dinosaur = zone?.FindDinosaur("Dilo");
        if (dinosaur != null)
        {
            dinosaur.MarkAsSick();
            await _unitOfWork.DinosaurRepository.UpdateAsync(dinosaur);
            await _unitOfWork.SaveChangesAsync();
        }
    }
    
    private async Task AddWesternRidgeDinosaurs(AddDinosaurToZoneUseCase useCase)
    {
        var zoneName = "Western Ridge";
        
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Blue", "Velociraptor"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Charlie", "Velociraptor"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Anky", "Ankylosaurus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Spino", "Spinosaurus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Cory", "Corythosaurus"));
        
        // Make a dinosaur sick for demo purposes
        var zone = await _unitOfWork.ZoneRepository.GetByNameAsync(zoneName);
        var dinosaur = zone?.FindDinosaur("Charlie");
        if (dinosaur != null)
        {
            dinosaur.MarkAsSick();
            await _unitOfWork.DinosaurRepository.UpdateAsync(dinosaur);
            await _unitOfWork.SaveChangesAsync();
        }
    }
    
    private async Task AddEasternRidgeDinosaurs(AddDinosaurToZoneUseCase useCase)
    {
        var zoneName = "Eastern Ridge";
        
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Ducky", "Triceratops"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Apatos", "Brachiosaurus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Allo", "Spinosaurus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Diplo", "Brachiosaurus"));
        await useCase.ExecuteAsync(new AddDinosaurToZoneRequest(zoneName, "Pachy", "Ankylosaurus"));
    }
}
using JurassicCode.Application.Interfaces;
using JurassicCode.Application.UseCases.Dinosaurs;
using JurassicCode.Application.UseCases.Zones;
using JurassicCode.Infrastructure.Persistence;
using JurassicCode.Infrastructure.Persistence.Repositories;
using JurassicCode.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JurassicCode.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register in-memory database
        services.AddSingleton<InMemoryDatabase>();
        
        // Register repositories
        services.AddScoped<IZoneRepository, ZoneRepository>();
        services.AddScoped<IDinosaurRepository, DinosaurRepository>();
        
        // Register unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Register domain event dispatcher
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        
        // Register database seeder
        services.AddScoped<DatabaseSeeder>();
        
        // Register use cases for zones
        services.AddScoped<GetAllZonesUseCase>();
        services.AddScoped<GetZoneByNameUseCase>();
        services.AddScoped<CreateZoneUseCase>();
        services.AddScoped<ToggleZoneStatusUseCase>();
        
        // Register use cases for dinosaurs
        services.AddScoped<GetDinosaursInZoneUseCase>();
        services.AddScoped<AddDinosaurToZoneUseCase>();
        services.AddScoped<MoveDinosaurUseCase>();
        services.AddScoped<SpeciesCompatibilityUseCase>();
        
        return services;
    }
}
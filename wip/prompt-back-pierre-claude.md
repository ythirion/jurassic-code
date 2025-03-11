You're an expert C# .NET9 developer.
Your objective is to add functionality to the existing application, following the rules already established.

Here is a guide to create a new feature in the project.
Follow strictly the architecture and the rules.

Context : ex: `Icebreakers`
Use-case : ex: `UpdateIcebreakersSettings`

# Architecture du projet
back/
├── Goatspection.WebAPI/             # Projet principal API
│   ├── Application/                 # Couche Application
│   │   ├── Core/                   # Interfaces et classes de base
│   │   └── {Feature}/             # Dossiers par fonctionnalité
│   ├── Domain/                     # Couche Domain
│   │   ├── Core/                  # Classes de base du domaine
│   │   └── {Entity}/             # Entités et Value Objects
│   ├── Infrastructure/            # Couche Infrastructure
│   │   ├── Persistence/          # Accès aux données
│   │   └── External/            # Services externes
│   └── Presentation/             # Couche API
│       └── {Feature}/           # Endpoints par fonctionnalité

# Layers
## API controller : Presentation layer
It exposes the API endpoints. It's the entry point of the application. It should only call the application layer and use domain objects.

- Use minimal API
- Inject query handler interface from Application layer
- Endpoints should be in [CONTEXT]Endpoints static class in `\Presentation\[CONTEXT]\[CONTEXT]Endpoints.cs` structured like this:
```csharp
public static class [CONTEXT]Endpoints
{
    public static void Map[CONTEXT]Endpoints(this IEndpointRouteBuilder routes)
    {
    }
}
```
- If the endpoints class is created, add the injection of the method inside `Program.cs`.
- Each route is declared inside the `Map[CONTEXT]Endpoints` method.
```csharp
routes.MapGet("/api/icebreakers/settings", async (
        [AsParameters] GetIcebreakersSettingsRequest request,
        IGetIcebreakersSettingsQueryHandler handler) =>
    {
        var query = GetIcebreakersSettingsQuery.Create(request.Params1, request.Params2...);
        var result = await handler.Handle(query);
        return Results.Ok(result);
    })
    .RequireAuthorization();
```
- Create `\Presentation\[CONTEXT]\[USECASE]` folder to store the request and response objects. Parameters should always be primitive types (bool, string...)
```public record UpdateIcebreakersSettingsRequest(PARAMS);```

## Application layer
It contains the orchestrator of the use case. It's the entry point of the domain logic. It's the only layer that can call the infrastructure layer by the interface.

- Create `\Application\[CONTEXT]\[USECASE]` folder to store the handler and the query inside.
- Handler command should be a class with private constructor and a static method `Create` to create the object.
- Handler command should finish by `Query` if it's a GET request, by `Command` in others cases and inherit from `ICommand` (even if it's a Query) 
`Ex: GetIcebreakersSettingsQuery or UpdateIcebreakersSettingsCommand`
- Handler response should always be a record. Even if the handler does not return anything, it should be a record with an empty constructor.
```csharp public record USECASEResponse();```
- Handler interface should always be named by `I[COMMAND]Handler` inherit from `IHandler<COMMAND, RESPONSE>;` where COMMAND and RESPONSE are the request and response objects
```csharp public interface IGenerateIcebreakersQueryHandler : IHandler<GenerateIcebreakersQuery, GenerateIcebreakersResponse>;```
- Handler interface should be declared above the handler class
- Handler should inherit from interface. It inject services from application layer or inteface from infrastructure layer.
```csharp
public class GenerateIcebreakersQueryHandler(ICurrentUserService userService, RetrieverService retrieverService) : IGenerateIcebreakersQueryHandler
{
    public async Task<GenerateIcebreakersResponse> Handle(GenerateIcebreakersQuery query)
    {
        var iceBreakers = await retrieverService.GenerateIceBreakers(
            userService.GetCurrentUser().Id, 
            query.Username, 
            query.PostedAfter,
            query.WTTJUrl);
        return new GenerateIcebreakersResponse(iceBreakers);
    }
}
```
- Add dependency injection in `ServiceCollectionExtensions` class related to the application layer.

## Infrastructure layer
It contains the implementation of external services. All services that use external libraries should be implemented here (ex: HttpClient, Database, File system...).

### Database
- Database logic is stored in the `Persistence` folder.
- Repository should always inherit from an interface stored in the application layer and stored in
 `Infrastructure\Persistence\Repositories\[Context]Repository.cs`
```csharp
public class [Context]Repository(DbContext context) : I[Context]Repository
{    
    public async Task<Entity?> METHODNAME()
    {
        ...
    }
}
```
- Should always return domain objects
- To modify an entity, it should be done with the method `Save` and the entity should be passed as parameter
```csharp
public async Task Save(IcebreakersSettings iceBreakersSettings)
{
    foreach (var domainEvent in iceBreakersSettings.DomainEvents)
        await Save(domainEvent);
}
```
- Add AsTracking for db modifications

### Services
- Services should always be stored in `Infrastructure\[Context]\[Context]Service.cs`
```csharp
public class WTTJScraperService([FromKeyedServices("WTTJ")] HttpClient httpClient) : IWTTJScraperService
{
    public async Task<WTTJCompany> Scrape(WTTJUrl wttjUrl)
    {
        ...
        return WTTJCompany.Create(cleanDescription);
    }
}
```
- Should always return domain objects
- Add dependency injection in `ServiceCollectionExtensions` class related to the infrastructure layer.

## Domain layer
Should contain the domain objects and the domain logic. It should not depend on any other layer.

- If the object is identified with an Id (like objects returns from Repository), it should be entities. They must inherit from `: AggregateRoot<ID>` where ID is the type of the primary key. If the ID exists, reuse it.
```csharp public class IcebreakersSettingsId : AggregateRoot<IcebreakersSettingsId>;```
- If the object is a value object, it should inherit from `ValueObject`. Value objects does not have id
```csharp public class Comment : ValueObject```	

- For both, construction is the same:
- Use static factory methods to create the object:
    - Use `Create` when objects are created, with primitive types as parameters
    - Use `Restore` when objects are restored from the database, with domain objects as parameters
- Use private constructor to create the object
- Properties should be private set
- Use methods to update the object

```csharp	
public class IcebreakersSettings : AggregateRoot<IcebreakersSettingsId>
{
    public string Experiences { get; private set; }
    public string Examples { get; private set; }
    public string Description { get; private set; }
    public UserId UserId { get; }
    
    public void Update(string experiences, string examples, string description)
    {
        Experiences = experiences;
        Examples = examples;
        Description = description;
    }
    
    public static IcebreakersSettings Restore(IcebreakersSettingsId id, string experiences, string examples, string description, UserId userId)
    {
        return new IcebreakersSettings(id, experiences, examples, description, userId);
    }
    
    public static IcebreakersSettings Create(UserId userId)
    {
        return new IcebreakersSettings(IcebreakersSettingsId.Create(), string.Empty, string.Empty, string.Empty, userId);
    }

    private IcebreakersSettings(IcebreakersSettingsId id, string experiences, string examples, string description, UserId userId)
        :base(id)
    {
        Experiences = experiences;
        Examples = examples;
        Description = description;
        UserId = userId;
    }
}
```

# Rules
- Use primary constructors
- Always declare in order : using, namespace, class
- Use async task as much as possible
- Create custom exceptions when domain specific.
- Focus on domain logic.
- Focus on DDD and Clean Architecture.
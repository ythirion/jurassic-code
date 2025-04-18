# Jurassic Code Development Guidelines

## Build Commands
- Build solution: `dotnet build JurassicCode.sln`
- Build specific project: `dotnet build JurassicCode/JurassicCode.csproj`

## Test Commands
- Run all tests: `dotnet test JurassicCode.Tests/JurassicCode.Tests.csproj`
- Run specific test: `dotnet test --filter "FullyQualifiedName=JurassicCode.Tests.ParkServiceTests.TestAddAndMoveDinosaursWithZoneToggle"`
- Mutation testing: `dotnet tool run dotnet-stryker`

## Code Style Guidelines
- Naming: PascalCase for classes/methods/properties, camelCase for variables/parameters, underscore prefix for private fields
- Braces: Opening braces on same line as statement
- Indentation: 4 spaces (no tabs)
- Types: Use explicit types for public APIs, var for local variables when type is evident
- Nullables: Use null checks consistently, avoid nullable warnings
- Imports: System namespaces first, then external packages, then project namespaces
- Error handling: Use exceptions with descriptive messages, try/catch in controllers
- Documentation: XML comments for public APIs (missing in many places currently)
- Testing: Use xUnit with FluentAssertions for readable assertions

## Code Smells to Avoid

```csharp
// 1. Mutable domain models with no validation
public class Dinosaur
{
    public string Name { get; set; }
    public string Species { get; set; }
    public bool IsCarnivorous { get; set; }
    // ...
}
```

```csharp
// 2. Static data access layer with global state
public static class DataAccessLayer
{
    public static Database _db;
    
    public static void SaveZone(Zone zone)
    {
        _db.Zones[zone.Name] = new Entities.ZoneEntity { /*...*/ };
    }
}
```

```csharp
// 3. Using reflection to access private fields
public static Dictionary<string, Entities.DinosaurEntity> Dinosaurs(Database db)
{
    return (Dictionary<string, Entities.DinosaurEntity>)ReflectionHelper.GetPrivateField(db, "_dinosaurs");
}
```

```csharp
// 4. No dependency injection
public class ParkController : ControllerBase
{
    private readonly ParkService _parkService = new();
    // ...
}
```

```csharp
// 5. Poor exception handling in controllers
[HttpPost("AddZone")]
public IActionResult AddZone([FromBody] ZoneRequest request)
{
    try
    {
        _parkService.AddZone(request.Name, request.IsOpen);
        return Ok("Zone added successfully.");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error adding zone: {ex.Message}");
    }
}
```

```csharp
// 6. Inconsistent entity mapping
public static List<Entities.DinosaurEntity> GetDinosaurs(string zoneCode)
{
    if (_db.Zones.TryGetValue(zoneCode, out var zone))
    {
        return zone.DinosaurCodes.Select(code => new Entities.DinosaurEntity { CodeName = code }).ToList();
    }
    return new List<Entities.DinosaurEntity>();
}
```

```csharp
// 7. Manual loops instead of LINQ
for (int i = 0; i < DataAccessLayer._db.Zones.Count; i++)
{
    if (DataAccessLayer._db.Zones.ElementAt(i).Value.ZoneCode == zoneName)
    {
        // ...
    }
}
```

```csharp
// 8. Non-English comments
// Logique complexe et inutile pour déterminer la coexistence
```


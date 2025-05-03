namespace JurassicCode.Domain.ValueObjects;

/// <summary>
/// SpeciesType is a value object representing dinosaur species with diet type and compatibility score
/// </summary>
public class SpeciesType
{
    private static readonly Dictionary<string, SpeciesType> _speciesCatalog = new()
    {
        { "T-Rex", new SpeciesType("T-Rex", DietType.Carnivore, -10) },
        { "Velociraptor", new SpeciesType("Velociraptor", DietType.Carnivore, -5) },
        { "Triceratops", new SpeciesType("Triceratops", DietType.Herbivore, 5) },
        { "Brachiosaurus", new SpeciesType("Brachiosaurus", DietType.Herbivore, 2) },
        { "Stegosaurus", new SpeciesType("Stegosaurus", DietType.Herbivore, 3) },
        { "Parasaurolophus", new SpeciesType("Parasaurolophus", DietType.Herbivore, 4) },
        { "Dilophosaurus", new SpeciesType("Dilophosaurus", DietType.Carnivore, -3) },
        { "Gallimimus", new SpeciesType("Gallimimus", DietType.Herbivore, 1) },
        { "Pteranodon", new SpeciesType("Pteranodon", DietType.Carnivore, -2) },
        { "Compsognathus", new SpeciesType("Compsognathus", DietType.Carnivore, -1) },
        { "Ankylosaurus", new SpeciesType("Ankylosaurus", DietType.Herbivore, 3) },
        { "Spinosaurus", new SpeciesType("Spinosaurus", DietType.Carnivore, -7) },
        { "Carnotaurus", new SpeciesType("Carnotaurus", DietType.Carnivore, -4) }
    };
    
    public string Name { get; }
    public DietType Diet { get; }
    public int CompatibilityScore { get; }
    
    private SpeciesType(string name, DietType diet, int compatibilityScore)
    {
        Name = name;
        Diet = diet;
        CompatibilityScore = compatibilityScore;
    }
    
    public static SpeciesType FromName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Species name cannot be empty", nameof(name));
            
        if (_speciesCatalog.TryGetValue(name, out var species))
            return species;
            
        throw new ArgumentException($"Unknown species: {name}", nameof(name));
    }
    
    public static bool AreCompatible(SpeciesType species1, SpeciesType species2)
    {
        if (species1 == null || species2 == null)
            return false;
            
        return species1.CompatibilityScore + species2.CompatibilityScore >= 0;
    }
    
    public static bool AreCompatible(string species1Name, string species2Name)
    {
        try
        {
            var speciesType1 = FromName(species1Name);
            var speciesType2 = FromName(species2Name);
            return AreCompatible(speciesType1, speciesType2);
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
    
    // Make sure value equality works properly
    public override bool Equals(object? obj)
    {
        if (obj is not SpeciesType other)
            return false;
            
        return Name == other.Name;
    }
    
    public override int GetHashCode() => Name.GetHashCode();
    
    public static bool operator ==(SpeciesType? left, SpeciesType? right)
    {
        if (left is null && right is null)
            return true;
        if (left is null || right is null)
            return false;
            
        return left.Equals(right);
    }
    
    public static bool operator !=(SpeciesType? left, SpeciesType? right) => !(left == right);
}
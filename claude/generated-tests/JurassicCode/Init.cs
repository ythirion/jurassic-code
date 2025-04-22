namespace JurassicCode;

public partial class ParkService
{
    private void Init()
    {
        AddZone("Ismaloya Mountains", true);
        AddZone("Western Ridge", true);
        AddZone("Eastern Ridge", true);

        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Rexy",
            Species = "T-Rex",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Bucky",
            Species = "Triceratops",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Echo",
            Species = "Velociraptor",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Brachio",
            Species = "Brachiosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Dilo",
            Species = "Dilophosaurus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Para",
            Species = "Parasaurolophus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Galli",
            Species = "Gallimimus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Ptera",
            Species = "Pteranodon",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Compys",
            Species = "Compsognathus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Bary",
            Species = "Baryonyx",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Ismaloya Mountains", new Dinosaur
        {
            Name = "Stego",
            Species = "Stegosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });

        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Blue",
            Species = "Velociraptor",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Charlie",
            Species = "Velociraptor",
            IsCarnivorous = true,
            IsSick = true,
            LastFed = DateTime.Now.AddDays(-1)
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Anky",
            Species = "Ankylosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Spino",
            Species = "Spinosaurus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Cory",
            Species = "Corythosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Carno",
            Species = "Carnotaurus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Iggy",
            Species = "Iguanodon",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Mamenchi",
            Species = "Mamenchisaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Mosa",
            Species = "Mosasaurus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Edmonto",
            Species = "Edmontosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Western Ridge", new Dinosaur
        {
            Name = "Troody",
            Species = "Troodon",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });

        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Ducky",
            Species = "Triceratops",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Apatos",
            Species = "Apatosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Allo",
            Species = "Allosaurus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Diplo",
            Species = "Diplodocus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Pachy",
            Species = "Pachycephalosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Styra",
            Species = "Styracosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Tylo",
            Species = "Tylosaurus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Cerato",
            Species = "Ceratosaurus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Hadro",
            Species = "Hadrosaurus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Megalo",
            Species = "Megalosaurus",
            IsCarnivorous = true,
            IsSick = false,
            LastFed = DateTime.Now
        });
        AddDinosaurToZone("Eastern Ridge", new Dinosaur
        {
            Name = "Ornitho",
            Species = "Ornithomimus",
            IsCarnivorous = false,
            IsSick = false,
            LastFed = DateTime.Now
        });
    }
}
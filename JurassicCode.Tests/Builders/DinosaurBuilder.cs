using System;
using Bogus;
using JurassicCode;

namespace JurassicCode.Tests.Builders
{
    /// <summary>
    /// Builder for creating <see cref="Dinosaur"/> instances with randomized data for tests
    /// </summary>
    public class DinosaurBuilder
    {
        private static readonly string[] _commonSpecies = new[]
        {
            "T-Rex", "Velociraptor", "Triceratops", "Brachiosaurus", "Stegosaurus",
            "Ankylosaurus", "Pteranodon", "Dilophosaurus", "Parasaurolophus", "Carnotaurus"
        };

        private readonly Faker _faker;
        private string _name;
        private string _species;
        private bool? _isCarnivorous;
        private bool? _isSick;
        private DateTime? _lastFed;

        public DinosaurBuilder()
        {
            _faker = new Faker();
        }

        /// <summary>
        /// Sets the name of the dinosaur
        /// </summary>
        public DinosaurBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Sets a random name for the dinosaur
        /// </summary>
        public DinosaurBuilder WithRandomName()
        {
            _name = _faker.Name.FirstName();
            return this;
        }

        /// <summary>
        /// Sets the species of the dinosaur
        /// </summary>
        public DinosaurBuilder WithSpecies(string species)
        {
            _species = species;
            return this;
        }

        /// <summary>
        /// Sets the species to T-Rex (which is carnivorous)
        /// </summary>
        public DinosaurBuilder AsTRex()
        {
            _species = "T-Rex";
            _isCarnivorous = true;
            return this;
        }

        /// <summary>
        /// Sets the species to Velociraptor (which is carnivorous)
        /// </summary>
        public DinosaurBuilder AsVelociraptor()
        {
            _species = "Velociraptor";
            _isCarnivorous = true;
            return this;
        }

        /// <summary>
        /// Sets the species to Triceratops (which is herbivorous)
        /// </summary>
        public DinosaurBuilder AsTriceratops()
        {
            _species = "Triceratops";
            _isCarnivorous = false;
            return this;
        }

        /// <summary>
        /// Sets a random species from the common species list
        /// </summary>
        public DinosaurBuilder WithRandomSpecies()
        {
            _species = _faker.PickRandom(_commonSpecies);
            
            // Set carnivorous based on species
            if (_species == "T-Rex" || _species == "Velociraptor" || _species == "Carnotaurus" || _species == "Dilophosaurus")
            {
                _isCarnivorous = true;
            }
            else
            {
                _isCarnivorous = false;
            }
            
            return this;
        }

        /// <summary>
        /// Sets whether the dinosaur is carnivorous
        /// </summary>
        public DinosaurBuilder IsCarnivorous(bool isCarnivorous)
        {
            _isCarnivorous = isCarnivorous;
            return this;
        }

        /// <summary>
        /// Sets whether the dinosaur is sick
        /// </summary>
        public DinosaurBuilder IsSick(bool isSick)
        {
            _isSick = isSick;
            return this;
        }

        /// <summary>
        /// Sets a random health status for the dinosaur
        /// </summary>
        public DinosaurBuilder WithRandomHealthStatus()
        {
            _isSick = _faker.Random.Bool(0.2f); // 20% chance of being sick
            return this;
        }

        /// <summary>
        /// Sets when the dinosaur was last fed
        /// </summary>
        public DinosaurBuilder LastFed(DateTime lastFed)
        {
            _lastFed = lastFed;
            return this;
        }

        /// <summary>
        /// Sets a random last fed time for the dinosaur (within the last week)
        /// </summary>
        public DinosaurBuilder WithRandomFeedingTime()
        {
            _lastFed = _faker.Date.Recent(7); // Within the last week
            return this;
        }

        /// <summary>
        /// Creates a completely randomized dinosaur with all properties having random values
        /// </summary>
        public static Dinosaur CreateRandom()
        {
            return new DinosaurBuilder()
                .WithRandomName()
                .WithRandomSpecies()
                .WithRandomHealthStatus()
                .WithRandomFeedingTime()
                .Build();
        }

        /// <summary>
        /// Creates a batch of random dinosaurs
        /// </summary>
        public static List<Dinosaur> CreateRandomBatch(int count)
        {
            var dinosaurs = new List<Dinosaur>();
            for (int i = 0; i < count; i++)
            {
                dinosaurs.Add(CreateRandom());
            }
            return dinosaurs;
        }

        /// <summary>
        /// Creates a herbivorous dinosaur (non-carnivorous)
        /// </summary>
        public static Dinosaur CreateHerbivore()
        {
            var faker = new Faker();
            string species = faker.PickRandom("Triceratops", "Brachiosaurus", "Stegosaurus", "Parasaurolophus", "Ankylosaurus");
            
            return new DinosaurBuilder()
                .WithRandomName()
                .WithSpecies(species)
                .IsCarnivorous(false)
                .WithRandomHealthStatus()
                .WithRandomFeedingTime()
                .Build();
        }

        /// <summary>
        /// Creates a carnivorous dinosaur
        /// </summary>
        public static Dinosaur CreateCarnivore()
        {
            var faker = new Faker();
            string species = faker.PickRandom("T-Rex", "Velociraptor", "Carnotaurus", "Dilophosaurus");
            
            return new DinosaurBuilder()
                .WithRandomName()
                .WithSpecies(species)
                .IsCarnivorous(true)
                .WithRandomHealthStatus()
                .WithRandomFeedingTime()
                .Build();
        }

        /// <summary>
        /// Builds a Dinosaur instance with the specified or default values
        /// </summary>
        public Dinosaur Build()
        {
            return new Dinosaur
            {
                Name = _name ?? _faker.Name.FirstName(),
                Species = _species ?? _faker.PickRandom(_commonSpecies),
                IsCarnivorous = _isCarnivorous ?? _faker.Random.Bool(),
                IsSick = _isSick ?? _faker.Random.Bool(0.2f),  // 20% chance of being sick
                LastFed = _lastFed ?? _faker.Date.Recent(7)    // Within the last week
            };
        }
    }
}
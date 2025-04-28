using System;
using Bogus;
using JurassicCode.DataAccess.Entities;

namespace JurassicCode.Tests.Builders
{
    /// <summary>
    /// Builder for creating <see cref="DinosaurEntity"/> instances with randomized data for tests
    /// </summary>
    public class DinosaurEntityBuilder
    {
        private static readonly string[] _commonSpecies = new[]
        {
            "T-Rex", "Velociraptor", "Triceratops", "Brachiosaurus", "Stegosaurus",
            "Ankylosaurus", "Pteranodon", "Dilophosaurus", "Parasaurolophus", "Carnotaurus"
        };

        private readonly Faker _faker;
        private string _codeName;
        private string _species;
        private bool? _isCarnivorous;
        private bool? _isSick;
        private DateTime? _lastFed;

        public DinosaurEntityBuilder()
        {
            _faker = new Faker();
        }

        /// <summary>
        /// Sets the code name of the dinosaur entity
        /// </summary>
        public DinosaurEntityBuilder WithCodeName(string codeName)
        {
            _codeName = codeName;
            return this;
        }

        /// <summary>
        /// Sets a random code name for the dinosaur entity
        /// </summary>
        public DinosaurEntityBuilder WithRandomCodeName()
        {
            _codeName = _faker.Name.FirstName();
            return this;
        }

        /// <summary>
        /// Sets the species of the dinosaur entity
        /// </summary>
        public DinosaurEntityBuilder WithSpecies(string species)
        {
            _species = species;
            return this;
        }

        /// <summary>
        /// Sets the species to T-Rex (which is carnivorous)
        /// </summary>
        public DinosaurEntityBuilder AsTRex()
        {
            _species = "T-Rex";
            _isCarnivorous = true;
            return this;
        }

        /// <summary>
        /// Sets the species to Velociraptor (which is carnivorous)
        /// </summary>
        public DinosaurEntityBuilder AsVelociraptor()
        {
            _species = "Velociraptor";
            _isCarnivorous = true;
            return this;
        }

        /// <summary>
        /// Sets the species to Triceratops (which is herbivorous)
        /// </summary>
        public DinosaurEntityBuilder AsTriceratops()
        {
            _species = "Triceratops";
            _isCarnivorous = false;
            return this;
        }

        /// <summary>
        /// Sets a random species from the common species list
        /// </summary>
        public DinosaurEntityBuilder WithRandomSpecies()
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
        /// Sets whether the dinosaur entity is carnivorous
        /// </summary>
        public DinosaurEntityBuilder IsCarnivorous(bool isCarnivorous)
        {
            _isCarnivorous = isCarnivorous;
            return this;
        }

        /// <summary>
        /// Sets whether the dinosaur entity is sick
        /// </summary>
        public DinosaurEntityBuilder IsSick(bool isSick)
        {
            _isSick = isSick;
            return this;
        }

        /// <summary>
        /// Sets a random health status for the dinosaur entity
        /// </summary>
        public DinosaurEntityBuilder WithRandomHealthStatus()
        {
            _isSick = _faker.Random.Bool(0.2f); // 20% chance of being sick
            return this;
        }

        /// <summary>
        /// Sets when the dinosaur entity was last fed
        /// </summary>
        public DinosaurEntityBuilder LastFed(DateTime lastFed)
        {
            _lastFed = lastFed;
            return this;
        }

        /// <summary>
        /// Sets a random last fed time for the dinosaur entity (within the last week)
        /// </summary>
        public DinosaurEntityBuilder WithRandomFeedingTime()
        {
            _lastFed = _faker.Date.Recent(7); // Within the last week
            return this;
        }

        /// <summary>
        /// Creates a completely randomized dinosaur entity with all properties having random values
        /// </summary>
        public static DinosaurEntity CreateRandom()
        {
            return new DinosaurEntityBuilder()
                .WithRandomCodeName()
                .WithRandomSpecies()
                .WithRandomHealthStatus()
                .WithRandomFeedingTime()
                .Build();
        }

        /// <summary>
        /// Creates a batch of random dinosaur entities
        /// </summary>
        public static List<DinosaurEntity> CreateRandomBatch(int count)
        {
            var dinosaurEntities = new List<DinosaurEntity>();
            for (int i = 0; i < count; i++)
            {
                dinosaurEntities.Add(CreateRandom());
            }
            return dinosaurEntities;
        }

        /// <summary>
        /// Creates a herbivorous dinosaur entity (non-carnivorous)
        /// </summary>
        public static DinosaurEntity CreateHerbivore()
        {
            var faker = new Faker();
            string species = faker.PickRandom("Triceratops", "Brachiosaurus", "Stegosaurus", "Parasaurolophus", "Ankylosaurus");
            
            return new DinosaurEntityBuilder()
                .WithRandomCodeName()
                .WithSpecies(species)
                .IsCarnivorous(false)
                .WithRandomHealthStatus()
                .WithRandomFeedingTime()
                .Build();
        }

        /// <summary>
        /// Creates a carnivorous dinosaur entity
        /// </summary>
        public static DinosaurEntity CreateCarnivore()
        {
            var faker = new Faker();
            string species = faker.PickRandom("T-Rex", "Velociraptor", "Carnotaurus", "Dilophosaurus");
            
            return new DinosaurEntityBuilder()
                .WithRandomCodeName()
                .WithSpecies(species)
                .IsCarnivorous(true)
                .WithRandomHealthStatus()
                .WithRandomFeedingTime()
                .Build();
        }

        /// <summary>
        /// Builds a DinosaurEntity instance with the specified or default values
        /// </summary>
        public DinosaurEntity Build()
        {
            return new DinosaurEntity
            {
                CodeName = _codeName ?? _faker.Name.FirstName(),
                Species = _species ?? _faker.PickRandom(_commonSpecies),
                IsCarnivorous = _isCarnivorous ?? _faker.Random.Bool(),
                IsSick = _isSick ?? _faker.Random.Bool(0.2f),  // 20% chance of being sick
                LastFed = _lastFed ?? _faker.Date.Recent(7)    // Within the last week
            };
        }
    }
}
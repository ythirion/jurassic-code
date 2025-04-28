using System;
using System.Collections.Generic;
using Bogus;
using JurassicCode;

namespace JurassicCode.Tests.Builders
{
    /// <summary>
    /// Builder for creating <see cref="Zone"/> instances with randomized data for tests
    /// </summary>
    public class ZoneBuilder
    {
        private static readonly string[] _zoneNames = new[]
        {
            "North Sector", "South Sector", "East Sector", "West Sector",
            "Carnivore Valley", "Herbivore Plains", "Jungle Zone", "River Area", 
            "Mountain Ridge", "Predator Enclosure", "Visitor Center", "Feeding Area"
        };

        private readonly Faker _faker;
        private string _name;
        private bool? _isOpen;
        private readonly List<Dinosaur> _dinosaurs = new List<Dinosaur>();

        public ZoneBuilder()
        {
            _faker = new Faker();
        }

        /// <summary>
        /// Sets the name of the zone
        /// </summary>
        public ZoneBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        /// <summary>
        /// Sets a random name for the zone
        /// </summary>
        public ZoneBuilder WithRandomName()
        {
            _name = _faker.PickRandom(_zoneNames) + " " + _faker.Random.Number(1, 9);
            return this;
        }

        /// <summary>
        /// Sets whether the zone is open or closed
        /// </summary>
        public ZoneBuilder IsOpen(bool isOpen)
        {
            _isOpen = isOpen;
            return this;
        }

        /// <summary>
        /// Sets the zone as open
        /// </summary>
        public ZoneBuilder AsOpen()
        {
            _isOpen = true;
            return this;
        }

        /// <summary>
        /// Sets the zone as closed
        /// </summary>
        public ZoneBuilder AsClosed()
        {
            _isOpen = false;
            return this;
        }

        /// <summary>
        /// Sets a random open/closed status for the zone
        /// </summary>
        public ZoneBuilder WithRandomStatus()
        {
            _isOpen = _faker.Random.Bool(0.8f);  // 80% chance of being open
            return this;
        }

        /// <summary>
        /// Adds a dinosaur to the zone
        /// </summary>
        public ZoneBuilder WithDinosaur(Dinosaur dinosaur)
        {
            _dinosaurs.Add(dinosaur);
            return this;
        }

        /// <summary>
        /// Adds multiple dinosaurs to the zone
        /// </summary>
        public ZoneBuilder WithDinosaurs(IEnumerable<Dinosaur> dinosaurs)
        {
            _dinosaurs.AddRange(dinosaurs);
            return this;
        }

        /// <summary>
        /// Adds a random dinosaur to the zone
        /// </summary>
        public ZoneBuilder WithRandomDinosaur()
        {
            _dinosaurs.Add(DinosaurBuilder.CreateRandom());
            return this;
        }

        /// <summary>
        /// Adds multiple random dinosaurs to the zone
        /// </summary>
        public ZoneBuilder WithRandomDinosaurs(int count)
        {
            _dinosaurs.AddRange(DinosaurBuilder.CreateRandomBatch(count));
            return this;
        }

        /// <summary>
        /// Adds dinosaurs according to compatibility rules
        /// </summary>
        /// <remarks>
        /// Follows the rules:
        /// - T-Rex cannot coexist with any other dinosaurs
        /// - Velociraptor can coexist with herbivores
        /// - Herbivores can coexist with each other
        /// </remarks>
        public ZoneBuilder WithCompatibleDinosaurs(int count)
        {
            // Select a strategy for populating the zone
            var strategy = _faker.Random.Int(0, 3);
            
            switch (strategy)
            {
                case 0: // Single T-Rex zone
                    if (count > 0)
                    {
                        _dinosaurs.Add(new DinosaurBuilder().AsTRex().WithRandomName().Build());
                    }
                    break;
                    
                case 1: // Velociraptor(s) with herbivores
                    if (count > 0)
                    {
                        // Add 1-2 Velociraptors
                        int velociraptorCount = Math.Min(_faker.Random.Int(1, 2), count);
                        for (int i = 0; i < velociraptorCount; i++)
                        {
                            _dinosaurs.Add(new DinosaurBuilder().AsVelociraptor().WithRandomName().Build());
                        }
                        
                        // Fill the rest with herbivores
                        int herbivoreCount = count - velociraptorCount;
                        for (int i = 0; i < herbivoreCount; i++)
                        {
                            _dinosaurs.Add(DinosaurBuilder.CreateHerbivore());
                        }
                    }
                    break;
                    
                case 2: // All herbivores
                    for (int i = 0; i < count; i++)
                    {
                        _dinosaurs.Add(DinosaurBuilder.CreateHerbivore());
                    }
                    break;
                    
                case 3: // Mix of carnivores (except T-Rex)
                    if (count > 0)
                    {
                        for (int i = 0; i < count; i++)
                        {
                            var dino = DinosaurBuilder.CreateCarnivore();
                            if (dino.Species == "T-Rex" && i > 0)
                            {
                                // Replace with Velociraptor
                                dino = new DinosaurBuilder().AsVelociraptor().WithRandomName().Build();
                            }
                            _dinosaurs.Add(dino);
                        }
                    }
                    break;
            }
            
            return this;
        }

        /// <summary>
        /// Creates a completely randomized zone with all properties having random values
        /// </summary>
        public static Zone CreateRandom(int dinosaurCount = 0)
        {
            return new ZoneBuilder()
                .WithRandomName()
                .WithRandomStatus()
                .WithRandomDinosaurs(dinosaurCount)
                .Build();
        }

        /// <summary>
        /// Creates a batch of random zones
        /// </summary>
        public static List<Zone> CreateRandomBatch(int count, int dinosaursPerZone = 0)
        {
            var zones = new List<Zone>();
            for (int i = 0; i < count; i++)
            {
                zones.Add(CreateRandom(dinosaursPerZone));
            }
            return zones;
        }

        /// <summary>
        /// Builds a Zone instance with the specified or default values
        /// </summary>
        public Zone Build()
        {
            var zone = new Zone
            {
                Name = _name ?? _faker.PickRandom(_zoneNames) + " " + _faker.Random.Number(1, 9),
                IsOpen = _isOpen ?? _faker.Random.Bool(0.8f),  // 80% chance of being open
            };

            foreach (var dinosaur in _dinosaurs)
            {
                zone.Dinosaurs.Add(dinosaur);
            }

            return zone;
        }
    }
}
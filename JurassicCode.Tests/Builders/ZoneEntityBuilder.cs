using System.Collections.Generic;
using Bogus;
using JurassicCode.DataAccess.Entities;

namespace JurassicCode.Tests.Builders
{
    /// <summary>
    /// Builder for creating <see cref="ZoneEntity"/> instances with randomized data for tests
    /// </summary>
    public class ZoneEntityBuilder
    {
        private static readonly string[] _zoneNames = new[]
        {
            "North Sector", "South Sector", "East Sector", "West Sector",
            "Carnivore Valley", "Herbivore Plains", "Jungle Zone", "River Area", 
            "Mountain Ridge", "Predator Enclosure", "Visitor Center", "Feeding Area"
        };

        private readonly Faker _faker;
        private string _zoneCode;
        private bool? _accessStatus;
        private List<string> _dinosaurCodes;

        public ZoneEntityBuilder()
        {
            _faker = new Faker();
            _dinosaurCodes = new List<string>();
        }

        /// <summary>
        /// Sets the zone code of the zone entity
        /// </summary>
        public ZoneEntityBuilder WithZoneCode(string zoneCode)
        {
            _zoneCode = zoneCode;
            return this;
        }

        /// <summary>
        /// Sets a random zone code for the zone entity
        /// </summary>
        public ZoneEntityBuilder WithRandomZoneCode()
        {
            _zoneCode = _faker.PickRandom(_zoneNames) + " " + _faker.Random.Number(1, 9);
            return this;
        }

        /// <summary>
        /// Sets whether the zone is accessible
        /// </summary>
        public ZoneEntityBuilder WithAccessStatus(bool accessStatus)
        {
            _accessStatus = accessStatus;
            return this;
        }

        /// <summary>
        /// Sets the zone as accessible (open)
        /// </summary>
        public ZoneEntityBuilder AsOpen()
        {
            _accessStatus = true;
            return this;
        }

        /// <summary>
        /// Sets the zone as closed
        /// </summary>
        public ZoneEntityBuilder AsClosed()
        {
            _accessStatus = false;
            return this;
        }

        /// <summary>
        /// Sets a random access status for the zone
        /// </summary>
        public ZoneEntityBuilder WithRandomAccessStatus()
        {
            _accessStatus = _faker.Random.Bool(0.8f);  // 80% chance of being open
            return this;
        }

        /// <summary>
        /// Sets the dinosaur codes for the zone entity
        /// </summary>
        public ZoneEntityBuilder WithDinosaurCodes(IEnumerable<string> dinosaurCodes)
        {
            _dinosaurCodes = new List<string>(dinosaurCodes);
            return this;
        }

        /// <summary>
        /// Adds a dinosaur code to the zone entity
        /// </summary>
        public ZoneEntityBuilder WithDinosaurCode(string dinosaurCode)
        {
            _dinosaurCodes.Add(dinosaurCode);
            return this;
        }

        /// <summary>
        /// Adds random dinosaur codes to the zone entity
        /// </summary>
        public ZoneEntityBuilder WithRandomDinosaurCodes(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _dinosaurCodes.Add(_faker.Name.FirstName() + "_" + _faker.Random.AlphaNumeric(4));
            }
            return this;
        }

        /// <summary>
        /// Creates a completely randomized zone entity with all properties having random values
        /// </summary>
        public static ZoneEntity CreateRandom(int dinosaurCodeCount = 0)
        {
            return new ZoneEntityBuilder()
                .WithRandomZoneCode()
                .WithRandomAccessStatus()
                .WithRandomDinosaurCodes(dinosaurCodeCount)
                .Build();
        }

        /// <summary>
        /// Creates a batch of random zone entities
        /// </summary>
        public static List<ZoneEntity> CreateRandomBatch(int count, int dinosaurCodesPerZone = 0)
        {
            var zoneEntities = new List<ZoneEntity>();
            for (int i = 0; i < count; i++)
            {
                zoneEntities.Add(CreateRandom(dinosaurCodesPerZone));
            }
            return zoneEntities;
        }

        /// <summary>
        /// Builds a ZoneEntity instance with the specified or default values
        /// </summary>
        public ZoneEntity Build()
        {
            return new ZoneEntity
            {
                ZoneCode = _zoneCode ?? _faker.PickRandom(_zoneNames) + " " + _faker.Random.Number(1, 9),
                AccessStatus = _accessStatus ?? _faker.Random.Bool(0.8f),  // 80% chance of being open
                DinosaurCodes = _dinosaurCodes ?? new List<string>()
            };
        }
    }
}
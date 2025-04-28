using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using JurassicCode.DataAccess.Entities;
using JurassicCode.DataAccess.Interfaces;
using NSubstitute;
using Xunit;
using Bogus;

namespace JurassicCode.Tests.MockTests
{
    public class ParkServiceMockTests
    {
        private readonly JurassicCode.ParkService _parkService;
        private readonly IDataAccessLayer _mockDataAccess;
        private readonly Faker _faker;
        
        public ParkServiceMockTests()
        {
            // Create a mock of IDataAccessLayer
            _mockDataAccess = Substitute.For<IDataAccessLayer>();
            
            // Create ParkService with the mock and disable initialization
            _parkService = new JurassicCode.ParkService(_mockDataAccess, false);

            // Initialize Bogus faker
            _faker = new Faker();
        }

        [Fact]
        public void AddZone_WhenZoneDoesNotExist_ShouldAddZoneSuccessfully()
        {
            // Arrange
            string zoneName = _faker.Address.County();
            bool isOpen = true;
            
            // Configure mock to return empty zones collection
            _mockDataAccess.GetAllZones().Returns(new List<KeyValuePair<string, ZoneEntity>>());
            
            // Act
            _parkService.AddZone(zoneName, isOpen);
            
            // Assert
            _mockDataAccess.Received(1).SaveZone(
                Arg.Is<string>(z => z == zoneName),
                Arg.Is<bool>(o => o == isOpen),
                Arg.Any<IEnumerable<string>>()
            );
        }

        [Fact]
        public void AddZone_WhenZoneAlreadyExists_ShouldThrowException()
        {
            // Arrange
            string existingZoneName = _faker.Address.County();
            
            var existingZones = new List<KeyValuePair<string, ZoneEntity>>
            {
                new KeyValuePair<string, ZoneEntity>(
                    existingZoneName, 
                    new ZoneEntity { ZoneCode = existingZoneName, AccessStatus = true, DinosaurCodes = new List<string>() }
                )
            };
            
            _mockDataAccess.GetAllZones().Returns(existingZones);
            
            // Act
            Action action = () => _parkService.AddZone(existingZoneName, true);
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zone already exists.");
        }
        
        [Fact]
        public void AddDinosaurToZone_WhenZoneExistsAndIsOpen_ShouldAddDinosaurSuccessfully()
        {
            // Arrange
            string zoneName = _faker.Address.County();
            bool isOpen = true;
            
            var zoneEntity = new ZoneEntity
            {
                ZoneCode = zoneName,
                AccessStatus = isOpen,
                DinosaurCodes = new List<string>()
            };
            
            var mockZoneEntry = new KeyValuePair<string, ZoneEntity>(zoneName, zoneEntity);
            
            _mockDataAccess.GetZoneCount().Returns(1);
            _mockDataAccess.GetZoneAtIndex(0).Returns(mockZoneEntry);
            
            var dinosaur = new Dinosaur
            {
                Name = _faker.Name.FirstName(),
                Species = _faker.Commerce.ProductName(),
                IsCarnivorous = _faker.Random.Bool(),
                IsSick = _faker.Random.Bool(),
                LastFed = _faker.Date.Recent()
            };
            
            // Act
            _parkService.AddDinosaurToZone(zoneName, dinosaur);
            
            // Assert
            _mockDataAccess.Received(1).SaveDinosaur(
                Arg.Is<string>(z => z == zoneName),
                Arg.Is<DinosaurEntity>(d => 
                    d.CodeName == dinosaur.Name && 
                    d.Species == dinosaur.Species && 
                    d.IsCarnivorous == dinosaur.IsCarnivorous &&
                    d.IsSick == dinosaur.IsSick
                )
            );
        }
        
        [Fact]
        public void AddDinosaurToZone_WhenZoneIsClosed_ShouldThrowException()
        {
            // Arrange
            string zoneName = _faker.Address.County();
            bool isOpen = false;
            
            var zoneEntity = new ZoneEntity
            {
                ZoneCode = zoneName,
                AccessStatus = isOpen,
                DinosaurCodes = new List<string>()
            };
            
            var mockZoneEntry = new KeyValuePair<string, ZoneEntity>(zoneName, zoneEntity);
            
            _mockDataAccess.GetZoneCount().Returns(1);
            _mockDataAccess.GetZoneAtIndex(0).Returns(mockZoneEntry);
            
            var dinosaur = new Dinosaur
            {
                Name = _faker.Name.FirstName(),
                Species = _faker.Commerce.ProductName(),
                IsCarnivorous = _faker.Random.Bool()
            };
            
            // Act
            Action action = () => _parkService.AddDinosaurToZone(zoneName, dinosaur);
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zone is closed or does not exist.");
        }
        
        [Fact]
        public void ToggleZone_ShouldCallDataAccessLayerToggleZoneStatus()
        {
            // Arrange
            string zoneName = _faker.Address.County();
            
            // Act
            _parkService.ToggleZone(zoneName);
            
            // Assert
            _mockDataAccess.Received(1).ToggleZoneStatus(Arg.Is<string>(z => z == zoneName));
        }
        
        [Fact]
        public void GetDinosaursInZone_WhenZoneExists_ShouldReturnDinosaurs()
        {
            // Arrange
            string zoneName = _faker.Address.County();
            
            var zoneEntity = new ZoneEntity
            {
                ZoneCode = zoneName,
                AccessStatus = true,
                DinosaurCodes = new List<string> { "Dino1", "Dino2" }
            };
            
            var mockZones = new List<KeyValuePair<string, ZoneEntity>>
            {
                new KeyValuePair<string, ZoneEntity>(zoneName, zoneEntity)
            };
            
            var mockDinos = new List<DinosaurEntity>
            {
                new DinosaurEntity
                {
                    CodeName = "Dino1",
                    Species = "T-Rex",
                    IsCarnivorous = true,
                    IsSick = false,
                    LastFed = DateTime.Now.AddDays(-1)
                },
                new DinosaurEntity
                {
                    CodeName = "Dino2",
                    Species = "Triceratops",
                    IsCarnivorous = false,
                    IsSick = true,
                    LastFed = DateTime.Now
                }
            };
            
            _mockDataAccess.GetAllZones().Returns(mockZones);
            _mockDataAccess.GetDinosaurs(zoneName).Returns(mockDinos);
            
            // Act
            var result = _parkService.GetDinosaursInZone(zoneName).ToList();
            
            // Assert
            result.Should().HaveCount(2);
            result[0].Name.Should().Be("Dino1");
            result[0].Species.Should().Be("T-Rex");
            result[0].IsCarnivorous.Should().BeTrue();
            result[1].Name.Should().Be("Dino2");
            result[1].Species.Should().Be("Triceratops");
            result[1].IsCarnivorous.Should().BeFalse();
        }
        
        [Fact]
        public void GetDinosaursInZone_WhenZoneDoesNotExist_ShouldThrowException()
        {
            // Arrange
            string nonExistentZone = _faker.Address.County();
            _mockDataAccess.GetAllZones().Returns(new List<KeyValuePair<string, ZoneEntity>>());
            
            // Act
            Action action = () => _parkService.GetDinosaursInZone(nonExistentZone).ToList();
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zone does not exist.");
        }
        
        [Fact]
        public void MoveDinosaur_WhenBothZonesExistAndTargetZoneIsOpen_ShouldMoveDinosaur()
        {
            // Arrange
            string sourceZoneName = _faker.Address.County();
            string targetZoneName = _faker.Address.County();
            string dinosaurName = _faker.Name.FirstName();
            
            var sourceZoneEntity = new ZoneEntity
            {
                ZoneCode = sourceZoneName,
                AccessStatus = true,
                DinosaurCodes = new List<string> { dinosaurName }
            };
            
            var targetZoneEntity = new ZoneEntity
            {
                ZoneCode = targetZoneName,
                AccessStatus = true,
                DinosaurCodes = new List<string>()
            };
            
            _mockDataAccess.GetZoneCount().Returns(2);
            _mockDataAccess.GetZoneAtIndex(0).Returns(new KeyValuePair<string, ZoneEntity>(sourceZoneName, sourceZoneEntity));
            _mockDataAccess.GetZoneAtIndex(1).Returns(new KeyValuePair<string, ZoneEntity>(targetZoneName, targetZoneEntity));
            
            // Act
            _parkService.MoveDinosaur(sourceZoneName, targetZoneName, dinosaurName);
            
            // Assert
            sourceZoneEntity.DinosaurCodes.Should().NotContain(dinosaurName);
            targetZoneEntity.DinosaurCodes.Should().Contain(dinosaurName);
        }
        
        [Fact]
        public void MoveDinosaur_WhenTargetZoneIsClosed_ShouldThrowException()
        {
            // Arrange
            string sourceZoneName = _faker.Address.County();
            string targetZoneName = _faker.Address.County();
            string dinosaurName = _faker.Name.FirstName();
            
            var sourceZoneEntity = new ZoneEntity
            {
                ZoneCode = sourceZoneName,
                AccessStatus = true,
                DinosaurCodes = new List<string> { dinosaurName }
            };
            
            var targetZoneEntity = new ZoneEntity
            {
                ZoneCode = targetZoneName,
                AccessStatus = false, // Closed zone
                DinosaurCodes = new List<string>()
            };
            
            // The key issue was with these mock setups
            // Need to ensure the keys match the zone names in GetZoneAtIndex
            _mockDataAccess.GetZoneCount().Returns(2);
            _mockDataAccess.GetZoneAtIndex(0).Returns(
                new KeyValuePair<string, ZoneEntity>(sourceZoneName, sourceZoneEntity));
            _mockDataAccess.GetZoneAtIndex(1).Returns(
                new KeyValuePair<string, ZoneEntity>(targetZoneName, targetZoneEntity));
            
            // Act
            Action action = () => _parkService.MoveDinosaur(sourceZoneName, targetZoneName, dinosaurName);
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");
        }
    }
}
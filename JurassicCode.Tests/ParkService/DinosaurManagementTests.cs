using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using JurassicCode.DataAccess.Entities;
using NSubstitute;
using Xunit;

namespace JurassicCode.Tests.ParkService
{
    public class DinosaurManagementTests : ParkServiceTestBase
    {
        [Fact]
        public void AddDinosaurToZone_WhenZoneExistsAndIsOpen_ShouldAddDinosaurSuccessfully()
        {
            // Arrange
            string zoneName = Faker.Address.County();
            bool isOpen = true;
            
            var zoneEntity = new ZoneEntity
            {
                ZoneCode = zoneName,
                AccessStatus = isOpen,
                DinosaurCodes = new List<string>()
            };
            
            var mockZoneEntry = new KeyValuePair<string, ZoneEntity>(zoneName, zoneEntity);
            
            MockDataAccess.GetZoneCount().Returns(1);
            MockDataAccess.GetZoneAtIndex(0).Returns(mockZoneEntry);
            
            var dinosaur = new Dinosaur
            {
                Name = Faker.Name.FirstName(),
                Species = Faker.Commerce.ProductName(),
                IsCarnivorous = Faker.Random.Bool(),
                IsSick = Faker.Random.Bool(),
                LastFed = Faker.Date.Recent()
            };
            
            // Act
            ParkService.AddDinosaurToZone(zoneName, dinosaur);
            
            // Assert
            MockDataAccess.Received(1).SaveDinosaur(
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
            string zoneName = Faker.Address.County();
            bool isOpen = false;
            
            var zoneEntity = new ZoneEntity
            {
                ZoneCode = zoneName,
                AccessStatus = isOpen,
                DinosaurCodes = new List<string>()
            };
            
            var mockZoneEntry = new KeyValuePair<string, ZoneEntity>(zoneName, zoneEntity);
            
            MockDataAccess.GetZoneCount().Returns(1);
            MockDataAccess.GetZoneAtIndex(0).Returns(mockZoneEntry);
            
            var dinosaur = new Dinosaur
            {
                Name = Faker.Name.FirstName(),
                Species = Faker.Commerce.ProductName(),
                IsCarnivorous = Faker.Random.Bool()
            };
            
            // Act
            Action action = () => ParkService.AddDinosaurToZone(zoneName, dinosaur);
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zone is closed or does not exist.");
        }
        
        [Fact]
        public void GetDinosaursInZone_WhenZoneExists_ShouldReturnDinosaurs()
        {
            // Arrange
            string zoneName = Faker.Address.County();
            
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
            
            MockDataAccess.GetAllZones().Returns(mockZones);
            MockDataAccess.GetDinosaurs(zoneName).Returns(mockDinos);
            
            // Act
            var result = ParkService.GetDinosaursInZone(zoneName).ToList();
            
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
            string nonExistentZone = Faker.Address.County();
            MockDataAccess.GetAllZones().Returns(new List<KeyValuePair<string, ZoneEntity>>());
            
            // Act
            Action action = () => ParkService.GetDinosaursInZone(nonExistentZone).ToList();
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zone does not exist.");
        }
    }
}
using System;
using System.Collections.Generic;
using FluentAssertions;
using JurassicCode.DataAccess.Entities;
using JurassicCode.Tests.Builders;
using NSubstitute;
using Xunit;

namespace JurassicCode.Tests.ParkService
{
    public class ZoneManagementTests : ParkServiceTestBase
    {
        [Fact]
        public void AddZone_WhenZoneDoesNotExist_ShouldAddZoneSuccessfully()
        {
            // Arrange
            string zoneName = Faker.Address.County();
            bool isOpen = true;
            
            // Configure mock to return empty zones collection
            MockDataAccess.GetAllZones().Returns(new List<KeyValuePair<string, ZoneEntity>>());
            
            // Act
            ParkService.AddZone(zoneName, isOpen);
            
            // Assert
            MockDataAccess.Received(1).SaveZone(
                Arg.Is<string>(z => z == zoneName),
                Arg.Is<bool>(o => o == isOpen),
                Arg.Any<IEnumerable<string>>()
            );
        }

        [Fact]
        public void AddZone_WhenZoneAlreadyExists_ShouldThrowException()
        {
            // Arrange
            string existingZoneName = Faker.Address.County();
            
            var existingZoneEntity = new ZoneEntityBuilder()
                .WithZoneCode(existingZoneName)
                .AsOpen()
                .Build();
            
            var existingZones = new List<KeyValuePair<string, ZoneEntity>>
            {
                new KeyValuePair<string, ZoneEntity>(existingZoneName, existingZoneEntity)
            };
            
            MockDataAccess.GetAllZones().Returns(existingZones);
            
            // Act
            Action action = () => ParkService.AddZone(existingZoneName, true);
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zone already exists.");
        }
        
        [Fact]
        public void ToggleZone_ShouldCallDataAccessLayerToggleZoneStatus()
        {
            // Arrange
            string zoneName = Faker.Address.County();
            
            // Act
            ParkService.ToggleZone(zoneName);
            
            // Assert
            MockDataAccess.Received(1).ToggleZoneStatus(Arg.Is<string>(z => z == zoneName));
        }
        
        [Fact]
        public void GetAllZones_ShouldMapZoneEntitiesToZones()
        {
            // Arrange
            var zoneEntity1 = new ZoneEntityBuilder()
                .WithRandomZoneCode()
                .AsOpen()
                .WithRandomDinosaurCodes(2)
                .Build();
            
            var zoneEntity2 = new ZoneEntityBuilder()
                .WithRandomZoneCode()
                .AsClosed()
                .WithRandomDinosaurCodes(1)
                .Build();
            
            var mockZones = new List<KeyValuePair<string, ZoneEntity>>
            {
                new KeyValuePair<string, ZoneEntity>(zoneEntity1.ZoneCode, zoneEntity1),
                new KeyValuePair<string, ZoneEntity>(zoneEntity2.ZoneCode, zoneEntity2)
            };
            
            MockDataAccess.GetAllZones().Returns(mockZones);
            
            // Mock dinosaur retrievals
            foreach (var code in zoneEntity1.DinosaurCodes)
            {
                MockDataAccess.GetDinosaurByName(code).Returns(DinosaurEntityBuilder.CreateRandom());
            }
            
            foreach (var code in zoneEntity2.DinosaurCodes)
            {
                MockDataAccess.GetDinosaurByName(code).Returns(DinosaurEntityBuilder.CreateRandom());
            }
            
            // Act
            var result = ParkService.GetAllZones();
            
            // Assert
            result.Should().HaveCount(2);
            
            var zones = new List<Zone>(result);
            zones[0].Name.Should().Be(zoneEntity1.ZoneCode);
            zones[0].IsOpen.Should().Be(zoneEntity1.AccessStatus);
            zones[0].Dinosaurs.Should().HaveCount(zoneEntity1.DinosaurCodes.Count);
            
            zones[1].Name.Should().Be(zoneEntity2.ZoneCode);
            zones[1].IsOpen.Should().Be(zoneEntity2.AccessStatus);
            zones[1].Dinosaurs.Should().HaveCount(zoneEntity2.DinosaurCodes.Count);
        }
    }
}
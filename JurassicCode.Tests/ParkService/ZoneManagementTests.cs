using System;
using System.Collections.Generic;
using FluentAssertions;
using JurassicCode.DataAccess.Entities;
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
            
            var existingZones = new List<KeyValuePair<string, ZoneEntity>>
            {
                new KeyValuePair<string, ZoneEntity>(
                    existingZoneName, 
                    new ZoneEntity { ZoneCode = existingZoneName, AccessStatus = true, DinosaurCodes = new List<string>() }
                )
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
    }
}
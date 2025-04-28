using System;
using System.Collections.Generic;
using FluentAssertions;
using JurassicCode.DataAccess.Entities;
using NSubstitute;
using Xunit;

namespace JurassicCode.Tests.ParkService
{
    public class MoveDinosaurTests : ParkServiceTestBase
    {
        [Fact]
        public void MoveDinosaur_WhenBothZonesExistAndTargetZoneIsOpen_ShouldMoveDinosaur()
        {
            // Arrange
            string sourceZoneName = Faker.Address.County();
            string targetZoneName = Faker.Address.County();
            string dinosaurName = Faker.Name.FirstName();
            
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
            
            MockDataAccess.GetZoneCount().Returns(2);
            MockDataAccess.GetZoneAtIndex(0).Returns(new KeyValuePair<string, ZoneEntity>(sourceZoneName, sourceZoneEntity));
            MockDataAccess.GetZoneAtIndex(1).Returns(new KeyValuePair<string, ZoneEntity>(targetZoneName, targetZoneEntity));
            
            // Act
            ParkService.MoveDinosaur(sourceZoneName, targetZoneName, dinosaurName);
            
            // Assert
            sourceZoneEntity.DinosaurCodes.Should().NotContain(dinosaurName);
            targetZoneEntity.DinosaurCodes.Should().Contain(dinosaurName);
        }
        
        [Fact]
        public void MoveDinosaur_WhenTargetZoneIsClosed_ShouldThrowException()
        {
            // Arrange
            string sourceZoneName = Faker.Address.County();
            string targetZoneName = Faker.Address.County();
            string dinosaurName = Faker.Name.FirstName();
            
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
            
            MockDataAccess.GetZoneCount().Returns(2);
            MockDataAccess.GetZoneAtIndex(0).Returns(
                new KeyValuePair<string, ZoneEntity>(sourceZoneName, sourceZoneEntity));
            MockDataAccess.GetZoneAtIndex(1).Returns(
                new KeyValuePair<string, ZoneEntity>(targetZoneName, targetZoneEntity));
            
            // Act
            Action action = () => ParkService.MoveDinosaur(sourceZoneName, targetZoneName, dinosaurName);
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");
        }
    }
}
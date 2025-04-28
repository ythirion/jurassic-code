using System;
using System.Collections.Generic;
using FluentAssertions;
using JurassicCode.DataAccess.Entities;
using JurassicCode.Tests.Builders;
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
            
            // Create cloneable collections so we can verify they're changed after the operation
            var sourceDinoCodes = new List<string> { dinosaurName };
            var targetDinoCodes = new List<string>();
            
            var sourceZoneEntity = new ZoneEntityBuilder()
                .WithZoneCode(sourceZoneName)
                .AsOpen()
                .WithDinosaurCodes(sourceDinoCodes)
                .Build();
            
            var targetZoneEntity = new ZoneEntityBuilder()
                .WithZoneCode(targetZoneName)
                .AsOpen()
                .WithDinosaurCodes(targetDinoCodes)
                .Build();
            
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
            
            var sourceZoneEntity = new ZoneEntityBuilder()
                .WithZoneCode(sourceZoneName)
                .AsOpen()
                .WithDinosaurCode(dinosaurName)
                .Build();
            
            var targetZoneEntity = new ZoneEntityBuilder()
                .WithZoneCode(targetZoneName)
                .AsClosed() // Zone is closed
                .Build();
            
            // This was the issue: we need to make sure the key matches exactly
            MockDataAccess.GetZoneCount().Returns(2);
            MockDataAccess.GetZoneAtIndex(0).Returns(new KeyValuePair<string, ZoneEntity>(sourceZoneName, sourceZoneEntity));
            MockDataAccess.GetZoneAtIndex(1).Returns(new KeyValuePair<string, ZoneEntity>(targetZoneName, targetZoneEntity));
            
            // Act & Assert
            Action action = () => ParkService.MoveDinosaur(sourceZoneName, targetZoneName, dinosaurName);
            action.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");
        }

        [Fact]
        public void MoveDinosaur_WhenSourceZoneDoesNotExist_ShouldThrowException()
        {
            // Arrange
            string sourceZoneName = Faker.Address.County();
            string targetZoneName = Faker.Address.County();
            string dinosaurName = Faker.Name.FirstName();
            
            var targetZoneEntity = new ZoneEntityBuilder()
                .WithZoneCode(targetZoneName)
                .AsOpen()
                .Build();
            
            MockDataAccess.GetZoneCount().Returns(1);
            MockDataAccess.GetZoneAtIndex(0).Returns(
                new KeyValuePair<string, ZoneEntity>(targetZoneName, targetZoneEntity));
            
            // Act
            Action action = () => ParkService.MoveDinosaur(sourceZoneName, targetZoneName, dinosaurName);
            
            // Assert
            action.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");
        }
        
        [Fact]
        public void MoveDinosaur_WhenDinosaurDoesNotExist_ShouldNotModifyZones()
        {
            // Arrange
            string sourceZoneName = Faker.Address.County();
            string targetZoneName = Faker.Address.County();
            string existingDinosaurName = Faker.Name.FirstName();
            string nonExistentDinosaurName = Faker.Name.FirstName();
            
            var sourceZoneEntity = new ZoneEntityBuilder()
                .WithZoneCode(sourceZoneName)
                .AsOpen()
                .WithDinosaurCode(existingDinosaurName)
                .Build();
            
            var targetZoneEntity = new ZoneEntityBuilder()
                .WithZoneCode(targetZoneName)
                .AsOpen()
                .Build();
            
            MockDataAccess.GetZoneCount().Returns(2);
            MockDataAccess.GetZoneAtIndex(0).Returns(new KeyValuePair<string, ZoneEntity>(sourceZoneName, sourceZoneEntity));
            MockDataAccess.GetZoneAtIndex(1).Returns(new KeyValuePair<string, ZoneEntity>(targetZoneName, targetZoneEntity));
            
            // Copy the dinosaur codes for comparison after the operation
            var sourceDinosaurCodesBefore = new List<string>(sourceZoneEntity.DinosaurCodes);
            var targetDinosaurCodesBefore = new List<string>(targetZoneEntity.DinosaurCodes);
            
            // Act
            ParkService.MoveDinosaur(sourceZoneName, targetZoneName, nonExistentDinosaurName);
            
            // Assert
            sourceZoneEntity.DinosaurCodes.Should().BeEquivalentTo(sourceDinosaurCodesBefore);
            targetZoneEntity.DinosaurCodes.Should().BeEquivalentTo(targetDinosaurCodesBefore);
        }
    }
}
using System;
using FluentAssertions;
using JurassicCode.Tests.TestDataBuilders;
using Xunit;
using static JurassicCode.Tests.TestDataBuilders.DinosaurBuilder;

namespace JurassicCode.Tests.UseCases
{
    public class Move_A_Dinosaur : TestBase
    {
        [Fact]
        public void Should_Move_Dinosaur_Between_Open_Zones()
        {
            // Arrange
            var sourceZone = "Source Zone";
            var targetZone = "Target Zone";
            ParkService.AddZone(sourceZone, true);
            ParkService.AddZone(targetZone, true);
            
            var dinosaur = ATRex().Build();
            ParkService.AddDinosaurToZone(sourceZone, dinosaur);
            
            // Act
            ParkService.MoveDinosaur(sourceZone, targetZone, dinosaur.Name);
            
            // Assert
            var sourceZoneDinosaurs = ParkService.GetDinosaursInZone(sourceZone);
            var targetZoneDinosaurs = ParkService.GetDinosaursInZone(targetZone);
            
            sourceZoneDinosaurs.MustNotContain(dinosaur.Name);
            targetZoneDinosaurs.MustContain(dinosaur.Name);
        }

        [Fact]
        public void Should_Move_Dinosaur_From_Closed_Zone_To_Open_Zone()
        {
            // Arrange
            var sourceZone = "Initially Open Zone";
            var targetZone = "Target Open Zone";
            ParkService.AddZone(sourceZone, true);
            ParkService.AddZone(targetZone, true);
            
            var dinosaur = ATriceratops().Build();
            ParkService.AddDinosaurToZone(sourceZone, dinosaur);
            
            // Close the source zone
            ParkService.ToggleZone(sourceZone);
            
            // Act
            ParkService.MoveDinosaur(sourceZone, targetZone, dinosaur.Name);
            
            // Assert
            var sourceZoneDinosaurs = ParkService.GetDinosaursInZone(sourceZone);
            var targetZoneDinosaurs = ParkService.GetDinosaursInZone(targetZone);
            
            sourceZoneDinosaurs.MustNotContain(dinosaur.Name);
            targetZoneDinosaurs.MustContain(dinosaur.Name);
        }

        public class Fail : TestBase
        {
            [Fact]
            public void When_Target_Zone_Is_Closed()
            {
                // Arrange
                var sourceZone = "Source Open Zone";
                var targetZone = "Target Closed Zone";
                ParkService.AddZone(sourceZone, true);
                ParkService.AddZone(targetZone, false);
                
                var dinosaur = AVelociraptor().Build();
                ParkService.AddDinosaurToZone(sourceZone, dinosaur);
                
                // Act
                Action moveToClosedZone = () => ParkService.MoveDinosaur(sourceZone, targetZone, dinosaur.Name);
                
                // Assert
                moveToClosedZone.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");
            }

            [Fact]
            public void When_Source_Zone_Does_Not_Exist()
            {
                // Arrange
                var nonExistentZone = "Non-Existent Zone";
                var targetZone = "Target Zone";
                ParkService.AddZone(targetZone, true);
                
                // Act
                Action moveFromNonExistentZone = () => ParkService.MoveDinosaur(nonExistentZone, targetZone, "AnyDino");
                
                // Assert
                moveFromNonExistentZone.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");
            }

            [Fact]
            public void When_Target_Zone_Does_Not_Exist()
            {
                // Arrange
                var sourceZone = "Source Zone";
                var nonExistentZone = "Non-Existent Zone";
                ParkService.AddZone(sourceZone, true);
                
                var dinosaur = ABrachiosaurus().Build();
                ParkService.AddDinosaurToZone(sourceZone, dinosaur);
                
                // Act
                Action moveToNonExistentZone = () => ParkService.MoveDinosaur(sourceZone, nonExistentZone, dinosaur.Name);
                
                // Assert
                moveToNonExistentZone.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");
            }

            [Fact]
            public void When_Dinosaur_Does_Not_Exist()
            {
                // Arrange
                var sourceZone = "Source Zone";
                var targetZone = "Target Zone";
                ParkService.AddZone(sourceZone, true);
                ParkService.AddZone(targetZone, true);
                
                // Act
                Action moveNonExistentDino = () => ParkService.MoveDinosaur(sourceZone, targetZone, "NonExistentDino");
                
                // Assert
                moveNonExistentDino.Should().Throw<Exception>().WithMessage("Dinosaur with name NonExistentDino does not exist in zone Source Zone.");
            }
        }
    }
}
using System;
using FluentAssertions;
using JurassicCode.Tests.TestDataBuilders;
using Xunit;
using static JurassicCode.Tests.TestDataBuilders.DinosaurBuilder;

namespace JurassicCode.Tests.UseCases
{
    public class Remove_A_Dinosaur : TestBase
    {
        [Fact]
        public void Should_Remove_Dinosaur_From_Zone()
        {
            // Arrange
            var zoneName = "Dinosaur Zone";
            ParkService.AddZone(zoneName, true);
            var dinosaur = ATRex().Build();
            ParkService.AddDinosaurToZone(zoneName, dinosaur);
            
            // Verify dinosaur is in zone
            var dinosaursBeforeRemoval = ParkService.GetDinosaursInZone(zoneName);
            dinosaursBeforeRemoval.MustContain(dinosaur.Name);
            
            // Act
            ParkService.RemoveDinosaurFromZone(zoneName, dinosaur.Name);
            
            // Assert
            var dinosaursAfterRemoval = ParkService.GetDinosaursInZone(zoneName);
            dinosaursAfterRemoval.MustNotContain(dinosaur.Name);
        }

        [Fact]
        public void Should_Remove_Dinosaur_From_Closed_Zone()
        {
            // Arrange
            var zoneName = "Initially Open Zone";
            ParkService.AddZone(zoneName, true);
            var dinosaur = ATRex().Build();
            ParkService.AddDinosaurToZone(zoneName, dinosaur);
            
            // Close the zone
            ParkService.ToggleZone(zoneName);
            
            // Act
            ParkService.RemoveDinosaurFromZone(zoneName, dinosaur.Name);
            
            // Assert
            var dinosaursAfterRemoval = ParkService.GetDinosaursInZone(zoneName);
            dinosaursAfterRemoval.MustNotContain(dinosaur.Name);
        }

        public class Fail : TestBase
        {
            [Fact]
            public void When_No_Dinosaur_Found()
            {
                // Arrange
                var zoneName = "Empty Zone";
                ParkService.AddZone(zoneName, true);
                
                // Act
                Action removeNonExistentDino = () => ParkService.RemoveDinosaurFromZone(zoneName, "NonExistentDino");
                
                // Assert
                removeNonExistentDino.Should().Throw<Exception>().WithMessage("Dinosaur not found in zone.");
            }

            [Fact]
            public void When_Zone_Does_Not_Exist()
            {
                // Arrange
                
                // Act
                Action removeFromNonExistentZone = () => ParkService.RemoveDinosaurFromZone("NonExistentZone", "AnyDino");
                
                // Assert
                removeFromNonExistentZone.Should().Throw<Exception>().WithMessage("Zone does not exist.");
            }
        }
    }
}
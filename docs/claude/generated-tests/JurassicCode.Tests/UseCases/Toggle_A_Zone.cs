using System;
using FluentAssertions;
using JurassicCode.Tests.TestDataBuilders;
using Xunit;
using static JurassicCode.Tests.TestDataBuilders.ZoneBuilder;

namespace JurassicCode.Tests.UseCases
{
    public class Toggle_A_Zone : TestBase
    {
        [Fact]
        public void Should_Close_An_Open_Zone()
        {
            // Arrange
            var zoneName = "Open Zone";
            ParkService.AddZone(zoneName, true);
            var zones = ParkService.GetAllZones();
            zones.HasAnOpenZone(zoneName);

            // Act
            ParkService.ToggleZone(zoneName);

            // Assert
            zones = ParkService.GetAllZones();
            zones.HasAClosedZone(zoneName);
        }

        [Fact]
        public void Should_Open_A_Closed_Zone()
        {
            // Arrange
            var zoneName = "Closed Zone";
            ParkService.AddZone(zoneName, false);
            var zones = ParkService.GetAllZones();
            zones.HasAClosedZone(zoneName);

            // Act
            ParkService.ToggleZone(zoneName);

            // Assert
            zones = ParkService.GetAllZones();
            zones.HasAnOpenZone(zoneName);
        }

        [Fact]
        public void Should_Toggle_Zone_Multiple_Times()
        {
            // Arrange
            var zoneName = "Multi-Toggle Zone";
            ParkService.AddZone(zoneName, true);

            // Act 1 - Close
            ParkService.ToggleZone(zoneName);

            // Assert 1
            var zones = ParkService.GetAllZones();
            zones.HasAClosedZone(zoneName);

            // Act 2 - Reopen
            ParkService.ToggleZone(zoneName);

            // Assert 2
            zones = ParkService.GetAllZones();
            zones.HasAnOpenZone(zoneName);

            // Act 3 - Close again
            ParkService.ToggleZone(zoneName);

            // Assert 3
            zones = ParkService.GetAllZones();
            zones.HasAClosedZone(zoneName);
        }

        public class Fail : TestBase
        {
            [Fact]
            public void When_Zone_Does_Not_Exist()
            {
                // Arrange
                var nonExistentZone = "Non-Existent Zone";

                // Act
                Action toggleNonExistentZone = () => ParkService.ToggleZone(nonExistentZone);

                // Assert
                toggleNonExistentZone.Should().Throw<Exception>().WithMessage($"Zone {nonExistentZone} does not exist.");
            }
        }
    }
}
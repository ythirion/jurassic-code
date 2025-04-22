using System;
using FluentAssertions;
using JurassicCode.Tests.TestDataBuilders;
using Xunit;
using static JurassicCode.Tests.TestDataBuilders.ZoneBuilder;

namespace JurassicCode.Tests.UseCases
{
    public class Add_A_Zone : TestBase
    {
        [Fact]
        public void Should_Add_Zone_When_Zone_Does_Not_Exist()
        {
            // Arrange
            var zoneName = "Safari Zone";
            var isOpen = true;

            // Act
            ParkService.AddZone(zoneName, isOpen);

            // Assert
            var zones = ParkService.GetAllZones();
            zones.HasAnOpenZone(zoneName);
        }
        
        [Fact]
        public void Should_Add_Closed_Zone()
        {
            // Arrange
            var zoneName = "Restricted Zone";
            var isOpen = false;

            // Act
            ParkService.AddZone(zoneName, isOpen);

            // Assert
            var zones = ParkService.GetAllZones();
            zones.HasAClosedZone(zoneName);
        }

        public class Fail : TestBase
        {
            [Fact]
            public void When_Zone_Already_Exists()
            {
                // Arrange
                var zoneName = "Duplicate Zone";
                ParkService.AddZone(zoneName, true);

                // Act
                Action addDuplicateZone = () => ParkService.AddZone(zoneName, false);

                // Assert
                addDuplicateZone.Should().Throw<Exception>().WithMessage("Zone already exists.");
            }
        }
    }
}
using System;
using System.Linq;
using FluentAssertions;
using JurassicCode.Tests.TestDataBuilders;
using Xunit;
using static JurassicCode.Tests.TestDataBuilders.DinosaurBuilder;

namespace JurassicCode.Tests.UseCases
{
    public class Add_A_Dinosaur : TestBase
    {
        [Fact]
        public void Should_Add_Dinosaur_To_Open_Zone()
        {
            // Arrange
            var zoneName = "Open Zone";
            ParkService.AddZone(zoneName, true);
            var dinosaur = ATRex().Build();

            // Act
            ParkService.AddDinosaurToZone(zoneName, dinosaur);

            // Assert
            var zoneDinosaurs = ParkService.GetDinosaursInZone(zoneName);
            zoneDinosaurs.MustContain(dinosaur.Name);
        }

        [Fact]
        public void Should_Add_Multiple_Dinosaurs_To_Zone()
        {
            // Arrange
            var zoneName = "Multi-Dino Zone";
            ParkService.AddZone(zoneName, true);
            var dino1 = ATriceratops().Build();
            var dino2 = ABrachiosaurus().Build();

            // Act
            ParkService.AddDinosaurToZone(zoneName, dino1);
            ParkService.AddDinosaurToZone(zoneName, dino2);

            // Assert
            var zoneDinosaurs = ParkService.GetDinosaursInZone(zoneName).ToList();
            zoneDinosaurs.MustHaveCount(2);
            zoneDinosaurs.MustContain(dino1.Name);
            zoneDinosaurs.MustContain(dino2.Name);
        }

        public class Fail : TestBase
        {
            [Fact]
            public void When_Zone_Is_Closed()
            {
                // Arrange
                var zoneName = "Closed Zone";
                ParkService.AddZone(zoneName, false);
                var dinosaur = ATRex().Build();

                // Act
                Action addToClosedZone = () => ParkService.AddDinosaurToZone(zoneName, dinosaur);

                // Assert
                addToClosedZone.Should().Throw<Exception>().WithMessage("Zone is closed or does not exist.");
            }

            [Fact]
            public void When_Zone_Does_Not_Exist()
            {
                // Arrange
                var nonExistentZone = "Non-Existent Zone";
                var dinosaur = ATRex().Build();

                // Act
                Action addToNonExistentZone = () => ParkService.AddDinosaurToZone(nonExistentZone, dinosaur);

                // Assert
                addToNonExistentZone.Should().Throw<Exception>().WithMessage("Zone is closed or does not exist.");
            }
        }
    }
}
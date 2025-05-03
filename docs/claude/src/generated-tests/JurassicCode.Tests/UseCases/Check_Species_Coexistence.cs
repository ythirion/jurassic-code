using System;
using FluentAssertions;
using JurassicCode.Tests.TestDataBuilders;
using Xunit;
using static JurassicCode.Tests.TestDataBuilders.DinosaurBuilder;

namespace JurassicCode.Tests.UseCases
{
    public class Check_Species_Coexistence : TestBase
    {
        [Fact]
        public void Should_Allow_Herbivore_Species_To_Coexist()
        {
            // Arrange
            var species1 = "Triceratops";
            var species2 = "Brachiosaurus";

            // Act
            var canCoexist = ParkService.CanSpeciesCoexist(species1, species2);

            // Assert
            canCoexist.Should().BeTrue();
        }

        [Fact]
        public void Should_Allow_Compatible_Carnivores_And_Herbivores()
        {
            // Arrange
            var carnivorousSpecies = "Pteranodon";
            var herbivorousSpecies = "Stegosaurus";

            // Act
            var canCoexist = ParkService.CanSpeciesCoexist(carnivorousSpecies, herbivorousSpecies);

            // Assert
            canCoexist.Should().BeTrue();
        }
        
        [Fact]
        public void Should_Increase_Compatibility_For_Triceratops()
        {
            // Arrange
            var triceratops = "Triceratops";
            var velociraptor = "Velociraptor";

            // Act
            var canCoexist = ParkService.CanSpeciesCoexist(triceratops, velociraptor);

            // Assert
            canCoexist.Should().BeTrue();
        }

        public class Fail : TestBase
        {
            [Fact]
            public void When_TRex_And_Velociraptor_Together()
            {
                // Arrange
                var tRex = "T-Rex";
                var velociraptor = "Velociraptor";

                // Act
                var canCoexist = ParkService.CanSpeciesCoexist(tRex, velociraptor);

                // Assert
                canCoexist.Should().BeFalse();
            }

            [Fact]
            public void When_Multiple_Aggressive_Carnivores_Together()
            {
                // Arrange
                var tRex = "T-Rex";
                var otherCarnivore = "Allosaurus";

                // Act
                var canCoexist = ParkService.CanSpeciesCoexist(tRex, otherCarnivore);

                // Assert
                canCoexist.Should().BeFalse();
            }
        }
    }
}
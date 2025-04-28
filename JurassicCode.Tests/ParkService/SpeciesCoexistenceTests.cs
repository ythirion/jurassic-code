using FluentAssertions;
using Xunit;

namespace JurassicCode.Tests.ParkService
{
    public class SpeciesCoexistenceTests : ParkServiceTestBase
    {
        /// <summary>
        /// Tests the compatibility rules for T-Rex with other species
        /// </summary>
        [Theory]
        [InlineData("T-Rex", "Velociraptor", false)]
        [InlineData("T-Rex", "Triceratops", false)] // T-Rex(-10) + Triceratops(+5) = -5, which is less than 0
        [InlineData("T-Rex", "Brachiosaurus", false)]
        public void CanSpeciesCoexist_WithTRex_ShouldReturnFalse(string species1, string species2, bool expectedResult)
        {
            // Act
            bool result = ParkService.CanSpeciesCoexist(species1, species2);
            
            // Assert
            result.Should().Be(expectedResult);
        }
        
        /// <summary>
        /// Tests the compatibility rules for Velociraptor with Triceratops
        /// </summary>
        [Theory]
        [InlineData("Velociraptor", "Triceratops", true)] // Velociraptor(-5) + Triceratops(+5) = 0, which is equal to 0
        public void CanSpeciesCoexist_VelociraptorWithTriceratops_ShouldReturnTrue(string species1, string species2, bool expectedResult)
        {
            // Act
            bool result = ParkService.CanSpeciesCoexist(species1, species2);
            
            // Assert
            result.Should().Be(expectedResult);
        }
        
        /// <summary>
        /// Tests the compatibility rules for Velociraptor with other herbivores
        /// </summary>
        [Theory]
        [InlineData("Velociraptor", "Brachiosaurus", false)] // Velociraptor(-5) + default(0) = -5, which is less than 0
        [InlineData("Stegosaurus", "Velociraptor", false)] // Velociraptor(-5) + default(0) = -5, which is less than 0
        public void CanSpeciesCoexist_VelociraptorWithOtherHerbivores_ShouldReturnFalse(string species1, string species2, bool expectedResult)
        {
            // Act
            bool result = ParkService.CanSpeciesCoexist(species1, species2);
            
            // Assert
            result.Should().Be(expectedResult);
        }
        
        /// <summary>
        /// Tests the compatibility rules between carnivores
        /// </summary>
        [Theory]
        [InlineData("Velociraptor", "Velociraptor", false)] // Velociraptor(-5) + Velociraptor(-5) = -10, which is less than 0
        [InlineData("Velociraptor", "Carnotaurus", false)] // Velociraptor(-5) + default(0) = -5, which is less than 0
        public void CanSpeciesCoexist_BetweenCarnivores_ShouldReturnFalse(string species1, string species2, bool expectedResult)
        {
            // Act
            bool result = ParkService.CanSpeciesCoexist(species1, species2);
            
            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
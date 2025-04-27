using System;
using System.Collections.Generic;
using FluentAssertions;
using JurassicCode.Db2;
using Xunit;

namespace JurassicCode.Tests
{
    public class ParkServiceTests
    {
        private readonly ParkService _parkService;

        public ParkServiceTests()
        {
            _parkService = new ParkService();
        }

        [Fact]
        public void TestAddAndMoveDinosaursWithZoneToggle()
        {
            DataAccessLayer.Init(new Database());
            _parkService.AddZone("Test Zone 1", true);
            _parkService.AddZone("Test Zone 2", false);
            
            // Add dinosaurs to zones and test moving them around
            _parkService.AddDinosaurToZone("Test Zone 1", new Dinosaur { Name = "TestDino1", Species = "T-Rex", IsCarnivorous = true });
            _parkService.AddDinosaurToZone("Test Zone 1", new Dinosaur { Name = "TestDino2", Species = "Velociraptor", IsCarnivorous = true });

            // Test moving a dinosaur to a closed zone (should fail)
            System.Action moveToClosedZone = () => _parkService.MoveDinosaur("Test Zone 1", "Test Zone 3", "TestDino1");
            moveToClosedZone.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");

            // Open the second zone and move the dinosaur
            _parkService.ToggleZone("Test Zone 2");
            _parkService.MoveDinosaur("Test Zone 1", "Test Zone 2", "TestDino1");

            // Verify the dinosaur was moved
            var zone2Dinosaurs = _parkService.GetDinosaursInZone("Test Zone 2");
            zone2Dinosaurs.Should().Contain(d => d.Name == "TestDino1");

            // Verify the dinosaur was removed from the first zone
            var zone1Dinosaurs = _parkService.GetDinosaursInZone("Test Zone 1");
            zone1Dinosaurs.Should().NotContain(d => d.Name == "TestDino1");

            // Test coexistence logic (should fail for T-Rex and Velociraptor)
            bool canCoexist = _parkService.CanSpeciesCoexist("T-Rex", "Velociraptor");
            canCoexist.Should().BeFalse();

            // Test coexistence logic (should pass for Triceratops and Velociraptor)
            canCoexist = _parkService.CanSpeciesCoexist("Triceratops", "Velociraptor");
            canCoexist.Should().BeTrue();

            // Test adding a dinosaur to a closed zone (should fail)
            _parkService.ToggleZone("Test Zone 1");
            System.Action addToClosedZone = () => _parkService.AddDinosaurToZone("Test Zone 1", new Dinosaur { Name = "TestDino3", Species = "Triceratops", IsCarnivorous = false });
            addToClosedZone.Should().Throw<Exception>().WithMessage("Zone is closed or does not exist.");
        }

        [Fact]
        public void TestZoneToggleAndDinosaurCount()
        {
            DataAccessLayer.Init(new Database());
            _parkService.AddZone("Test Zone 1", true);
            _parkService.AddZone("Test Zone 2", false);

            // Toggle zones and check dinosaur counts
            _parkService.ToggleZone("Test Zone 2");

            // Add dinosaurs to both zones
            _parkService.AddDinosaurToZone("Test Zone 1", new Dinosaur { Name = "DinoA", Species = "Brachiosaurus", IsCarnivorous = false });
            _parkService.AddDinosaurToZone("Test Zone 2", new Dinosaur { Name = "DinoB", Species = "Stegosaurus", IsCarnivorous = false });

            // Verify dinosaur counts
            var zone1Dinosaurs = _parkService.GetDinosaursInZone("Test Zone 1");
            zone1Dinosaurs.Should().HaveCount(1);

            var zone2Dinosaurs = _parkService.GetDinosaursInZone("Test Zone 2");
            zone2Dinosaurs.Should().HaveCount(1); 
        }

        [Fact]
        public void TestComplexDinosaurMovements()
        {
            DataAccessLayer.Init(new Database());
            _parkService.AddZone("Test Zone 1", true);
            _parkService.AddZone("Test Zone 2", false);

            // Add more zones for complex movements
            _parkService.AddZone("Test Zone 3", true);
            _parkService.AddZone("Test Zone 4", false);

            // Add dinosaurs to the new zones
            _parkService.AddDinosaurToZone("Test Zone 3", new Dinosaur { Name = "DinoC", Species = "Pteranodon", IsCarnivorous = true });

            // Move dinosaurs around and test the results
            _parkService.MoveDinosaur("Test Zone 3", "Test Zone 1", "DinoC");
            _parkService.ToggleZone("Test Zone 4");
            _parkService.MoveDinosaur("Test Zone 1", "Test Zone 4", "DinoC");

            // Verify the final positions of the dinosaurs
            var zone4Dinosaurs = _parkService.GetDinosaursInZone("Test Zone 4");
            zone4Dinosaurs.Should().Contain(d => d.Name == "DinoC");

            var zone1Dinosaurs = _parkService.GetDinosaursInZone("Test Zone 1");
            zone1Dinosaurs.Should().NotContain(d => d.Name == "DinoC");

            // Test moving a dinosaur to a non-existent zone (should fail)
            System.Action moveToNonExistentZone = () => _parkService.MoveDinosaur("Test Zone 4", "Non-Existent Zone", "DinoC");
            moveToNonExistentZone.Should().Throw<Exception>().WithMessage("Zones are closed or do not exist.");
        }
    }
}

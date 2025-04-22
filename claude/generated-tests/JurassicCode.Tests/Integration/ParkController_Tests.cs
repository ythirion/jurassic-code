using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JurassicCode.Requests;
using Xunit;
using FluentAssertions;
using static JurassicCode.Tests.TestDataBuilders.DinosaurBuilder;

// Helper class for creating JSON content for HTTP requests
internal static class JsonContent
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true
    };

    public static StringContent Create<T>(T content)
    {
        return new StringContent(
            JsonSerializer.Serialize(content, _jsonOptions),
            Encoding.UTF8,
            "application/json");
    }
}

namespace JurassicCode.Tests.Integration
{
    public class ParkController_Tests
    {
        private readonly TestWebApplicationFactory _factory;
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };

        public ParkController_Tests()
        {
            _factory = new TestWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Should_Add_Zone_When_Zone_Does_Not_Exist()
        {
            // Arrange
            var zoneRequest = new ZoneRequest
            {
                Name = "Test Zone",
                IsOpen = true
            };
            var content = JsonContent.Create(zoneRequest);

            // Act
            var response = await _client.PostAsync("/Park/AddZone", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("Zone added successfully");
        }

        [Fact]
        public async Task Should_Add_Dinosaur_To_Zone()
        {
            // Arrange
            // First create a zone
            var createZoneContent = JsonContent.Create(new ZoneRequest
                {
                    Name = "Dino Zone",
                    IsOpen = true
                });
            await _client.PostAsync("/Park/AddZone", createZoneContent);

            // Then add a dinosaur
            var dinosaur = ATRex().Build();
            var dinosaurRequest = new AddDinosaurRequest
            {
                ZoneName = "Dino Zone",
                Dinosaur = dinosaur
            };
            var content = JsonContent.Create(dinosaurRequest);

            // Act
            var response = await _client.PostAsync("/Park/AddDinosaurToZone", content);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            responseString.Should().Contain("Dinosaur added successfully");

            // Verify the dinosaur is in the zone by calling GetDinosaursInZone
            var zoneContent = JsonContent.Create(new ZoneRequest { Name = "Dino Zone" });
            var getDinosResponse = await _client.PostAsync("/Park/GetDinosaursInZone", zoneContent);
            getDinosResponse.EnsureSuccessStatusCode();
            var dinosInZone = await getDinosResponse.Content.ReadAsStringAsync();
            dinosInZone.Should().Contain(dinosaur.Name);
            dinosInZone.Should().Contain(dinosaur.Species);
        }

        [Fact]
        public async Task Should_Toggle_Zone_Status()
        {
            // Arrange
            // First create a zone
            var createZoneContent = JsonContent.Create(new ZoneRequest
                {
                    Name = "Toggle Zone",
                    IsOpen = true
                });
            await _client.PostAsync("/Park/AddZone", createZoneContent);

            var toggleRequest = new ZoneToggleRequest
            {
                ZoneName = "Toggle Zone"
            };
            var content = JsonContent.Create(toggleRequest);

            // Act
            var response = await _client.PostAsync("/Park/ToggleZone", content);

            // Assert
            response.EnsureSuccessStatusCode();
            
            // Verify the zone status by calling GetAllZones
            var getAllZonesResponse = await _client.GetAsync("/Park/GetAllZones");
            getAllZonesResponse.EnsureSuccessStatusCode();
            var zones = await getAllZonesResponse.Content.ReadAsStringAsync();
            zones.Should().Contain("Toggle Zone");
            zones.Should().Contain("\"isOpen\":false"); // The zone should be toggled to closed
        }

        public class Fail
        {
            private readonly TestWebApplicationFactory _factory;
            private readonly HttpClient _client;

            public Fail()
            {
                _factory = new TestWebApplicationFactory();
                _client = _factory.CreateClient();
            }

            [Fact]
            public async Task When_Adding_Zone_That_Already_Exists()
            {
                // Arrange
                var zoneRequest = new ZoneRequest
                {
                    Name = "Duplicate Zone",
                    IsOpen = true
                };
                var content = JsonContent.Create(zoneRequest);

                // Add the zone first time
                await _client.PostAsync("/Park/AddZone", content);

                // Act - Try to add the same zone again
                var response = await _client.PostAsync("/Park/AddZone", content);

                // Assert
                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                responseString.Should().Contain("Zone Duplicate Zone already exists");
            }

            [Fact]
            public async Task When_Adding_Dinosaur_To_Closed_Zone()
            {
                // Arrange
                // Create a closed zone
                var createZoneContent = JsonContent.Create(new ZoneRequest
                    {
                        Name = "Closed Zone",
                        IsOpen = false
                    });
                await _client.PostAsync("/Park/AddZone", createZoneContent);

                // Try to add a dinosaur to the closed zone
                var dinosaurRequest = new AddDinosaurRequest
                {
                    ZoneName = "Closed Zone",
                    Dinosaur = ATRex().Build()
                };
                var content = JsonContent.Create(dinosaurRequest);

                // Act
                var response = await _client.PostAsync("/Park/AddDinosaurToZone", content);

                // Assert
                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                responseString.Should().Contain("Zone Closed Zone is closed");
            }

            [Fact]
            public async Task When_Moving_Dinosaur_To_Non_Existent_Zone()
            {
                // Arrange
                // Create a source zone and add a dinosaur
                var createZoneContent = JsonContent.Create(new ZoneRequest
                    {
                        Name = "Source Zone",
                        IsOpen = true
                    });
                await _client.PostAsync("/Park/AddZone", createZoneContent);

                var dinosaur = ATRex().Build();
                var addDinoContent = JsonContent.Create(new AddDinosaurRequest
                    {
                        ZoneName = "Source Zone",
                        Dinosaur = dinosaur
                    });
                await _client.PostAsync("/Park/AddDinosaurToZone", addDinoContent);

                // Try to move the dinosaur to a non-existent zone
                var moveRequest = new MoveDinosaurRequest
                {
                    FromZoneName = "Source Zone",
                    ToZoneName = "Non-Existent Zone",
                    DinosaurName = dinosaur.Name
                };
                var content = JsonContent.Create(moveRequest);

                // Act
                var response = await _client.PostAsync("/Park/MoveDinosaur", content);

                // Assert
                Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
                var responseString = await response.Content.ReadAsStringAsync();
                responseString.Should().Contain("Zones are closed or do not exist");
            }
        }
    }
}
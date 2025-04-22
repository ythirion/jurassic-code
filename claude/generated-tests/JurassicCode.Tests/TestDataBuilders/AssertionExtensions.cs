using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace JurassicCode.Tests.TestDataBuilders
{
    public static class AssertionExtensions
    {
        public static void MustContain(this IEnumerable<Dinosaur> dinosaurs, string dinosaurName)
        {
            dinosaurs.Should().Contain(d => d.Name == dinosaurName);
        }

        public static void MustNotContain(this IEnumerable<Dinosaur> dinosaurs, string dinosaurName)
        {
            dinosaurs.Should().NotContain(d => d.Name == dinosaurName);
        }

        public static void MustContainSpecies(this IEnumerable<Dinosaur> dinosaurs, string species)
        {
            dinosaurs.Should().Contain(d => d.Species == species);
        }

        public static void MustBeEmpty(this IEnumerable<Dinosaur> dinosaurs)
        {
            dinosaurs.Should().BeEmpty();
        }

        public static void MustHaveCount(this IEnumerable<Dinosaur> dinosaurs, int count)
        {
            dinosaurs.Should().HaveCount(count);
        }

        public static void HasAnOpenZone(this IEnumerable<Zone> zones, string zoneName)
        {
            zones.Should().Contain(z => z.Name == zoneName && z.IsOpen);
        }

        public static void HasAClosedZone(this IEnumerable<Zone> zones, string zoneName)
        {
            zones.Should().Contain(z => z.Name == zoneName && !z.IsOpen);
        }

        public static void MustContainZone(this IEnumerable<Zone> zones, string zoneName)
        {
            zones.Should().Contain(z => z.Name == zoneName);
        }

        public static void MustNotContainZone(this IEnumerable<Zone> zones, string zoneName)
        {
            zones.Should().NotContain(z => z.Name == zoneName);
        }

        public static void MustHaveZoneCount(this IEnumerable<Zone> zones, int count)
        {
            zones.Should().HaveCount(count);
        }
    }
}
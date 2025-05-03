using System;
using System.Collections.Generic;

namespace JurassicCode.Tests.TestDataBuilders
{
    public class ZoneBuilder
    {
        private string _name = $"Zone-{Guid.NewGuid().ToString("N").Substring(0, 5)}";
        private bool _isOpen = true;
        private List<Dinosaur> _dinosaurs = new List<Dinosaur>();

        public static ZoneBuilder AZone() => new ZoneBuilder();
        public static ZoneBuilder AnOpenZone() => new ZoneBuilder().ThatIsOpen();
        public static ZoneBuilder AClosedZone() => new ZoneBuilder().ThatIsClosed();

        public ZoneBuilder Named(string name)
        {
            _name = name;
            return this;
        }

        public ZoneBuilder ThatIsOpen()
        {
            _isOpen = true;
            return this;
        }

        public ZoneBuilder ThatIsClosed()
        {
            _isOpen = false;
            return this;
        }

        public ZoneBuilder WithDinosaur(Dinosaur dinosaur)
        {
            _dinosaurs.Add(dinosaur);
            return this;
        }

        public ZoneBuilder WithDinosaurs(params Dinosaur[] dinosaurs)
        {
            _dinosaurs.AddRange(dinosaurs);
            return this;
        }

        public Zone Build()
        {
            var zone = new Zone
            {
                Name = _name,
                IsOpen = _isOpen,
                Dinosaurs = new List<Dinosaur>()
            };

            foreach (var dinosaur in _dinosaurs)
            {
                zone.Dinosaurs.Add(dinosaur);
            }

            return zone;
        }
    }
}
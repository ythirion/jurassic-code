using System;

namespace JurassicCode.Tests.TestDataBuilders
{
    public class DinosaurBuilder
    {
        private string _name = Guid.NewGuid().ToString("N").Substring(0, 8);
        private string _species = "Generic";
        private bool _isCarnivorous = false;
        private bool _isSick = false;
        private DateTime _lastFed = DateTime.Now.AddDays(-1);

        public static DinosaurBuilder ADinosaur() => new DinosaurBuilder();
        public static DinosaurBuilder ATRex() => new DinosaurBuilder().OfSpecies("T-Rex").ThatIsCarnivorous();
        public static DinosaurBuilder AVelociraptor() => new DinosaurBuilder().OfSpecies("Velociraptor").ThatIsCarnivorous();
        public static DinosaurBuilder ATriceratops() => new DinosaurBuilder().OfSpecies("Triceratops").ThatIsHerbivore();
        public static DinosaurBuilder ABrachiosaurus() => new DinosaurBuilder().OfSpecies("Brachiosaurus").ThatIsHerbivore();
        public static DinosaurBuilder AStegosaurus() => new DinosaurBuilder().OfSpecies("Stegosaurus").ThatIsHerbivore();
        public static DinosaurBuilder APteranodon() => new DinosaurBuilder().OfSpecies("Pteranodon").ThatIsCarnivorous();

        public DinosaurBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public DinosaurBuilder OfSpecies(string species)
        {
            _species = species;
            return this;
        }

        public DinosaurBuilder ThatIsCarnivorous()
        {
            _isCarnivorous = true;
            return this;
        }

        public DinosaurBuilder ThatIsHerbivore()
        {
            _isCarnivorous = false;
            return this;
        }

        public DinosaurBuilder ThatIsSick()
        {
            _isSick = true;
            return this;
        }

        public DinosaurBuilder ThatIsHealthy()
        {
            _isSick = false;
            return this;
        }

        public DinosaurBuilder FedRecently()
        {
            _lastFed = DateTime.Now.AddHours(-1);
            return this;
        }

        public DinosaurBuilder NotFedRecently()
        {
            _lastFed = DateTime.Now.AddDays(-3);
            return this;
        }

        public Dinosaur Build()
        {
            return new Dinosaur
            {
                Name = _name,
                Species = _species,
                IsCarnivorous = _isCarnivorous,
                IsSick = _isSick,
                LastFed = _lastFed
            };
        }
    }
}
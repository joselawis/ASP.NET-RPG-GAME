using Heroes.Domain.Common;

namespace Heroes.Domain.ValueObjects
{
    public sealed class HeroClass : ValueObject
    {
        public string Name { get; }
        public Stats BaseStats { get; }

        private HeroClass(string name, Stats baseStats)
        {
            Name = name;
            BaseStats = baseStats;
        }

        public static HeroClass Warrior => new("Warrior", new Stats(150, 25, 20, 10));
        public static HeroClass Mage => new("Mage", new Stats(80, 40, 10, 15));
        public static HeroClass Rogue => new("Rogue", new Stats(100, 20, 15, 25));

        public static HeroClass FromString(string className) => className.ToLower() switch
        {
            "warrior" => Warrior,
            "mage" => Mage,
            "rogue" => Rogue,
            _ => throw new ArgumentException($"Unknown hero class: {className}")
        };

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }

        public override string ToString() => Name;
    }
}
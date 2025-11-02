using Heroes.Domain.Common;
using Heroes.Domain.Events;
using Heroes.Domain.Exceptions;

namespace Heroes.Domain.ValueObjects
{
    public sealed class Hero : Entity<HeroId>
    {
        public HeroName Name { get; private set; }
        public HeroClass Class { get; private set; }
        public Level Level { get; private set; }
        public Experience Experience { get; private set; }
        public Stats CurrentStats { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastBattleAt { get; private set; }

        private Hero(HeroId id, HeroName name, HeroClass heroClass) : base(id)
        {
            Name = name;
            Class = heroClass;
            Level = Level.Create(1);
            Experience = Experience.Create(0);
            CurrentStats = heroClass.BaseStats;
            CreatedAt = DateTime.UtcNow;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        // Need for Entity Framework Core
        private Hero() : base()
        {
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public static Hero Create(string name, string className)
        {
            var heroId = HeroId.CreateUnique();
            var heroName = HeroName.Create(name);
            var heroClass = HeroClass.FromString(className);

            var hero = new Hero(heroId, heroName, heroClass);
            hero.AddDomainEvent(new HeroCreatedEvent(heroId.Value, heroName.Value, heroClass.Name));

            return hero;
        }

        public void GainExperience(int amount)
        {
            if (amount <= 0)
                throw new DomainException("Experience amount must be positive");

            Experience = Experience.Add(amount);

            while (Experience.IsReadyToLevelUp(Level))
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level = Level.Increase();
            Experience = Experience.ConsumeForLevelUp(Level);
            CurrentStats = CurrentStats.IncreaseForLevel(Level.Value);

            AddDomainEvent(new HeroLeveledUpEvent(Id.Value, Level.Value));
        }

        public void RecordBattle()
        {
            LastBattleAt = DateTime.UtcNow;
        }

        public bool CanBattle()
        {
            return CurrentStats.Health > 0;
        }
    }
}
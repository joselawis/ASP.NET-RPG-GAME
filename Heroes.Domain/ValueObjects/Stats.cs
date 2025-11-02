using Heroes.Domain.Common;

namespace Heroes.Domain.ValueObjects
{
    public sealed class Stats(int health, int attack, int defense, int speed) : ValueObject
    {
        public int Health { get; } = health;
        public int Attack { get; } = attack;
        public int Defense { get; } = defense;
        public int Speed { get; } = speed;

        public Stats IncreaseForLevel(int level)
        {
            return new Stats(
                Health + (level * 10),
                Attack + (level * 2),
                Defense + (level * 2),
                Speed + level
            );
        }

        public Stats ApplyItemBonus(Stats itemStats)
        {
            return new Stats(
                Health + itemStats.Health,
                Attack + itemStats.Attack,
                Defense + itemStats.Defense,
                Speed + itemStats.Speed
            );
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Health;
            yield return Attack;
            yield return Defense;
            yield return Speed;
        }
    }
}
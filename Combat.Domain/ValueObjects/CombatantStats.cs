using Combat.Domain.Common;

namespace Combat.Domain.ValueObjects
{
    public sealed class CombatantStats : ValueObject
    {
        public int Health { get; }
        public int Attack { get; }
        public int Defense { get; }
        public int Speed { get; }

        public CombatantStats(int health, int attack, int defense, int speed)
        {
            Health = health;
            Attack = attack;
            Defense = defense;
            Speed = speed;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        // Need for Entity Framework Core
        private CombatantStats() : base()
        {
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public int CalculateDamage(CombatantStats target)
        {
            int damage = Attack - (target.Defense / 2);
            return Math.Max(1, damage);
        }

        public CombatantStats TakeDamage(int damage)
        {
            var newHealth = Math.Max(0, Health - damage);
            return new CombatantStats(newHealth, Attack, Defense, Speed);
        }

        public bool IsAlive() => Health > 0;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Health;
            yield return Attack;
            yield return Defense;
            yield return Speed;
        }
    }
}
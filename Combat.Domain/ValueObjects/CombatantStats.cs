using Combat.Domain.Common;

namespace Combat.Domain.ValueObjects
{
    public sealed class CombatantStats(int health, int attack, int defense, int speed) : ValueObject
    {
        public int Health { get; } = health;
        public int Attack { get; } = attack;
        public int Defense { get; } = defense;
        public int Speed { get; } = speed;

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
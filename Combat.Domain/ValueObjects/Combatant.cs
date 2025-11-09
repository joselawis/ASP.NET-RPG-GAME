using Combat.Domain.Common;

namespace Combat.Domain.ValueObjects
{
    public sealed class Combatant : ValueObject
    {
        public Guid EntityId { get; }
        public string Name { get; }
        public CombatantType Type { get; }
        public CombatantStats Stats { get; }

        public Combatant(Guid entityId, string name, CombatantType type, CombatantStats stats)
        {
            EntityId = entityId;
            Name = name;
            Type = type;
            Stats = stats;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        // Need for Entity Framework Core
        private Combatant() : base()
        {
            Name = string.Empty;
            Stats = null!;
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public Combatant WithStats(CombatantStats newStats)
        {
            return new Combatant(EntityId, Name, Type, newStats);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return EntityId;
            yield return Name;
            yield return Type;
        }
    }
}
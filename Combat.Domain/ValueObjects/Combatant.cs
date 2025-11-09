using Combat.Domain.Common;

namespace Combat.Domain.ValueObjects
{
    public sealed class Combatant(Guid entityId, string name, CombatantType type, CombatantStats stats) : ValueObject
    {
        public Guid EntityId { get; } = entityId;
        public string Name { get; } = name;
        public CombatantType Type { get; } = type;
        public CombatantStats Stats { get; } = stats;

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
using Combat.Domain.Common;

namespace Combat.Domain.ValueObjects
{
    public sealed class CombatTurn(int turnNumber, string attackerName, string defenderName, int damageDealt, bool wasCritical) : ValueObject
    {
        public int TurnNumber { get; } = turnNumber;
        public string AttackerName { get; } = attackerName;
        public string DefenderName { get; } = defenderName;
        public int DamageDealt { get; } = damageDealt;
        public bool WasCritical { get; } = wasCritical;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TurnNumber;
            yield return AttackerName;
            yield return DefenderName;
            yield return DamageDealt;
        }

        public override string ToString()
        {
            var criticalText = WasCritical ? " (Critical Hit!)" : string.Empty;
            return $"Turn {TurnNumber}: {AttackerName} attacked {DefenderName} for {DamageDealt} damage{criticalText}.";
        }
    }
}
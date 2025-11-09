using Combat.Domain.Common;

namespace Combat.Domain.ValueObjects
{
    public sealed class CombatTurn : ValueObject
    {
        public int TurnNumber { get; }
        public string AttackerName { get; }
        public string DefenderName { get; }
        public int DamageDealt { get; }
        public bool WasCritical { get; }

        public CombatTurn(int turnNumber, string attackerName, string defenderName, int damageDealt, bool wasCritical)
        {
            TurnNumber = turnNumber;
            AttackerName = attackerName;
            DefenderName = defenderName;
            DamageDealt = damageDealt;
            WasCritical = wasCritical;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        // Need for Entity Framework Core
        private CombatTurn() : base()
        {
            AttackerName = string.Empty;
            DefenderName = string.Empty;
        }

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
using Combat.Domain.Common;

namespace Combat.Domain.ValueObjects
{
    public sealed class CombatId : ValueObject
    {
        public Guid Value { get; }

        private CombatId(Guid value) => Value = value;

        public static CombatId CreateUnique() => new(Guid.NewGuid());

        public static CombatId Create(Guid value) => new(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
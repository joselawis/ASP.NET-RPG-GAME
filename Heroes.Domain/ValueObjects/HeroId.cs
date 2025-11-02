using Heroes.Domain.Common;

namespace Heroes.Domain.ValueObjects
{
    public sealed class HeroId(Guid value) : ValueObject
    {
        public Guid Value { get; } = value;

        public static HeroId CreateUnique() => new(Guid.NewGuid());

        public static HeroId Create(Guid value) => new(value);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
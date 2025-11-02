using Heroes.Domain.Common;
using Heroes.Domain.Exceptions;

namespace Heroes.Domain.ValueObjects
{
    public sealed class HeroName : ValueObject
    {
        public string Value { get; }

        private HeroName(string value)
        {
            Value = value;
        }

        public static HeroName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Hero name cannot be null or empty.");

            if (value.Length < 3 || value.Length > 50)
                throw new DomainException("Hero name must be between 3 and 50 characters long.");

            return new HeroName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}
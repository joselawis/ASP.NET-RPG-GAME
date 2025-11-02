using Heroes.Domain.Common;
using Heroes.Domain.Exceptions;

namespace Heroes.Domain.ValueObjects
{
    public sealed class Level : ValueObject
    {
        public int Value { get; }

        private Level(int value)
        {
            Value = value;
        }

        public static Level Create(int value = 1)
        {
            if (value < 1 || value > 100)
                throw new DomainException("Level must be between 1 and 100");

            return new Level(value);
        }

        public Level Increase()
        {
            if (Value >= 100)
                throw new DomainException("Maximum level reached");

            return new Level(Value + 1);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
using Heroes.Domain.Common;

namespace Heroes.Domain.ValueObjects
{
    public sealed class Experience : ValueObject
    {
        public int Value { get; }

        private Experience(int value)
        {
            Value = value;
        }

        public static Experience Create(int value = 0) => new(value);

        public Experience Add(int amount) => new(Value + amount);

        public bool IsReadyToLevelUp(Level currentLevel)
        {
            int requiredExp = currentLevel.Value * 100;
            return Value >= requiredExp;
        }

        public Experience ConsumeForLevelUp(Level currentLevel)
        {
            int requiredExp = currentLevel.Value * 100;
            return new Experience(Value - requiredExp);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
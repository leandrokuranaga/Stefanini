using Abp.Domain.Values;

namespace Stefanini.Domain.CityAggregate.ValueObjects
{
    public class Age : ValueObject
    {
        public int Value { get; }
        public int PersonId { get; set; }

        public Age(int value)
        {
            if (value < 0)
                throw new ArgumentException("Age must be non-negative.");
            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}

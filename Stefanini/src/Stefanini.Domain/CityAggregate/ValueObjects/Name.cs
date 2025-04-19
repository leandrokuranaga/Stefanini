using Abp.Domain.Values;

namespace Stefanini.Domain.CityAggregate.ValueObjects
{
    public class Name : ValueObject
    {
        public string Value { get; }

        public Name(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Name cannot be null or empty.");

            value = value.Trim();

            if (value.Length < 2)
                throw new ArgumentException("Name must have at least 2 characters.");

            if (value.Length > 100)
                throw new ArgumentException("Name must have at most 100 characters.");

            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}

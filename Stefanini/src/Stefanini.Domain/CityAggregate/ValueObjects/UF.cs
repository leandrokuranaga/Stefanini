using Abp.Domain.Values;
using System.Text.RegularExpressions;

namespace Stefanini.Domain.CityAggregate.ValueObjects
{
    public class UF : ValueObject
    {
        public string Value { get; }

        private static readonly HashSet<string> ValidUFs = new()
        {
            "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO",
            "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
            "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
        };

        public UF(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("UF cannot be null or empty.");

            value = value.Trim().ToUpperInvariant();

            if (value.Length != 2 || !Regex.IsMatch(value, "^[A-Z]{2}$"))
                throw new ArgumentException("UF must be exactly 2 uppercase letters.");

            if (!ValidUFs.Contains(value))
                throw new ArgumentException("UF must be a valid Brazilian state abbreviation.");

            Value = value;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString() => Value;
    }
}

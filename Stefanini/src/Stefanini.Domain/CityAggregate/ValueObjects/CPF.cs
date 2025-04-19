using Abp.Domain.Values;

public class CPF : ValueObject
{
    public string Value { get; }

    public int PersonId { get; set; }

    public CPF(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length != 11)
            throw new ArgumentException("Invalid CPF");
        Value = value;
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}

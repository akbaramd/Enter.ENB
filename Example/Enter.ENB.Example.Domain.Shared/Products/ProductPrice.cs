using Enter.ENB.Domain.Values;

namespace Enter.ENB.Example.Domain.Shared.Products;

public class ProductPrice : EntValueObject
{
    public decimal Amount { get; set; }
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Amount;
    }
}
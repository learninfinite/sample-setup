using Domain.Primitives;
using Domain.Shared;

namespace Domain.ValueObjects;

public sealed class ChangeUnit : ValueObject
{
    private ChangeUnit(string label, int qty, decimal amount)
    {
        Label = label;
        Qty = qty;
        Amount = amount;
    }

    public string Label { get; }
    public int Qty { get; }

    public decimal Amount { get; }

    public static Result<ChangeUnit> Create(string label, int qty, decimal amount)
    {
        if (string.IsNullOrWhiteSpace(label))
        {
            return Result.Failure<ChangeUnit>(new Error("ChangeUnit.EmptyLabel","Label is required"));
        }

        return new ChangeUnit(label, qty, amount);
    }

    public override IEnumerable<object> GetAtomicValues()
    {
        yield return Label;
        yield return Qty;
        yield return Amount;
    }
}

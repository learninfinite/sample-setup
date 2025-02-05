using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public sealed class Transaction : Entity
{
    public Transaction(Guid id, 
        CurrencyType currencyType,
        decimal total,
        decimal paymentAmount) : base(id)
    {
        Currency = currencyType;
        Total = total;
        PaymentAmount = paymentAmount;
    }

    public CurrencyType Currency { get; set; } = CurrencyType.USD;

    public decimal Total { get; set; }
    public decimal PaymentAmount { get; set; }
}

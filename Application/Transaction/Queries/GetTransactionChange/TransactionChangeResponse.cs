using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Transaction.Queries.GetTransactionChange;

public sealed record TransactionChangeResponse(Guid Id, 
    CurrencyType Currency,
    decimal ChangeAmount,
    ICollection<ChangeUnit> Change);
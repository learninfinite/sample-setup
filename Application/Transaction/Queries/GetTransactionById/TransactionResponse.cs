using Domain.Enums;

namespace Application.Transaction.Queries.GetTransactionById;

public sealed record TransactionResponse(Guid Id, 
    CurrencyType Currency,
    decimal Total,
    decimal PaymentAmount);
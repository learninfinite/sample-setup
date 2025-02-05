using Application.Abstractions.Messaging;

namespace Application.Transaction.Queries.GetTransactionChange;

public sealed record GetTransactionChangeQuery(Guid tranId) : IQuery<TransactionChangeResponse>;

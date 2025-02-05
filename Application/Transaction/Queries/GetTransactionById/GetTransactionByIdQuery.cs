using Application.Abstractions.Messaging;

namespace Application.Transaction.Queries.GetTransactionById;

public sealed record GetTransactionByIdQuery(Guid tranId) : IQuery<TransactionResponse>;

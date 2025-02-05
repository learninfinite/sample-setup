using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.Transaction.Queries.GetTransactionById;

internal sealed class GetTransactionChangeQueryHandler
    : IQueryHandler<GetTransactionByIdQuery, TransactionResponse>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionChangeQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<Result<TransactionResponse>> Handle(GetTransactionByIdQuery query, CancellationToken cancellationToken = default)
    {
        var tran = await _transactionRepository.GetByIdAsync(
             query.tranId,
             cancellationToken);

        if (tran is null)
        {
            return Result.Failure<TransactionResponse>(new Error(
                "Transaction.NotFound",
                $"The transaction with Id {query.tranId} was not found"));
        }

        var response = new TransactionResponse(tran.Id, 
            tran.Currency,
            tran.Total,
            tran.PaymentAmount);

        return response;
    }
}
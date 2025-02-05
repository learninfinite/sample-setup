using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.Transaction.Queries.GetTransactionChange;

internal sealed class GetTransactionChangeQueryHandler
    : IQueryHandler<GetTransactionChangeQuery, TransactionChangeResponse>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEnumerable<ICurrencyChangeService> _currencyChangeServices;

    public GetTransactionChangeQueryHandler(ITransactionRepository transactionRepository,
        IEnumerable<ICurrencyChangeService> currencyChangeServices)
    {
        _transactionRepository = transactionRepository;
        _currencyChangeServices = currencyChangeServices;
    }

    public async Task<Result<TransactionChangeResponse>> Handle(GetTransactionChangeQuery query, CancellationToken cancellationToken)
    {
        var tran = await _transactionRepository.GetByIdAsync(
             query.tranId,
             cancellationToken);

        if (tran is null)
        {
            return Result.Failure<TransactionChangeResponse>(new Error(
                "Transaction.NotFound",
                $"The transaction with Id {query.tranId} was not found"));
        }

        var changeService = _currencyChangeServices.FirstOrDefault(f => f.CurrencyType == tran.Currency);

        if (changeService is null)
        {
            return Result.Failure<TransactionChangeResponse>(new Error(
                "ChangeService.NotFound",
                $"No change service found for currency: {tran.Currency}"));
        }

        var changeResult = changeService.MakeChange(tran);

        var response = new TransactionChangeResponse(tran.Id,
            tran.Currency,
            changeResult.Value.Sum(s=> s.Amount),
            changeResult.Value);

        return response;
    }
}
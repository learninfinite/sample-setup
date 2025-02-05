using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.Sales.Commands.CreateTransaction;

public sealed class CreateTransactionCommandHandler : ICommandHandler<CreateTransactionCommand, Guid>
{
    private readonly ITransactionRepository _transactionRepository; 
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;   
    }

    public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var transaction = new Domain.Entities.Transaction(
            Guid.CreateVersion7(),
            request.currencyType,
            request.total,
            request.paymentAmount);

        _transactionRepository.Add(transaction);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(transaction.Id);
    }
}

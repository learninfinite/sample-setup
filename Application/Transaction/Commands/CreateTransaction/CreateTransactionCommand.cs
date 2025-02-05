using Application.Abstractions.Messaging;
using Domain.Enums;
using Domain.Shared;

namespace Application.Sales.Commands.CreateTransaction;

public record CreateTransactionCommand(CurrencyType currencyType,
    decimal total,
    decimal paymentAmount) : Application.Abstractions.Messaging.ICommand<Guid>;

using Application.Sales.Commands.CreateTransaction;
using Application.Transaction.Queries.GetTransactionById;
using Application.Transaction.Queries.GetTransactionChange;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[Route("api/transactions")]
public sealed class TransactionsController : ApiController
{
    public TransactionsController(ISender sender)
        : base(sender)
    {
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(command, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTransactionById(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTransactionByIdQuery(id);

        Result<TransactionResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }

    [HttpGet("{id}/change")]
    public async Task<IActionResult> GetTransactionChange(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetTransactionChangeQuery(id);

        Result<TransactionChangeResponse> response = await Sender.Send(query, cancellationToken);

        return response.IsSuccess ? Ok(response.Value) : NotFound(response.Error);
    }
}

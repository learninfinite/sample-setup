using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repository;

internal sealed class TransactionRepository : ITransactionRepository
{
    public void Add(Transaction transaction)
    {
        MemoryDb.Transactions.AddOrUpdate(transaction.Id, transaction, (key, old) => transaction);
    }

    public Task<Transaction?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Transaction? transaction = null;

        if (MemoryDb.Transactions.ContainsKey(id)) 
            transaction = MemoryDb.Transactions[id];

        return Task.FromResult<Transaction?>(transaction);
    }
}

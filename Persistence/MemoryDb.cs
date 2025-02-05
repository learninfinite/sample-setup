using Domain.Entities;
using System.Collections.Concurrent;

namespace Persistence;

internal static class MemoryDb
{
    public static ConcurrentDictionary<Guid, Transaction> Transactions { get; set; }
        = new ConcurrentDictionary<Guid, Transaction>();
}

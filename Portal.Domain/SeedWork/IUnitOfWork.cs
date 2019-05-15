using System;
using System.Threading;
using System.Threading.Tasks;

namespace Portal.Domain.SeedWork
{
    // Design Patterns comment
    // Unit of Work design pattern does two important things: 
    // first it maintains in-memory updates 
    // second it sends these in-memory updates as one transaction to the database.
    public interface IUnitOfWork: IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

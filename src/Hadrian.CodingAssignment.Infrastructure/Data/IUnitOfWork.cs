using Microsoft.EntityFrameworkCore.Storage;

namespace Hadrian.CodingAssignment.Infrastructure.Data;

public interface IUnitOfWork
{
    /// <summary>
    /// Saves database changes
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Number of changes saved in database</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Asynchronously starts a new transaction on the database.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A task that represents the asynchronous transaction initialization. The task
    /// result contains a <see cref="IDbContextTransaction"/>
    /// that represents the started transaction.
    /// </returns>
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hadrian.CodingAssignment.Infrastructure.Data.EntityFrameworkCore;

public class DbContextUnitOfWork<T> : IUnitOfWork where T : DbContext
{
    private readonly T _context;

    public DbContextUnitOfWork(T context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return _context.Database.BeginTransactionAsync(cancellationToken);
    }
}
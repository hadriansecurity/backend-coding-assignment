using Hadrian.CodingAssignment.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Hadrian.CodingAssignment.Infrastructure.Data.Repository;

public class IntegrationsRepository
{
    private readonly DbSet<Integration> _dbSet;

    public IntegrationsRepository(DbContext context)
    {
        _dbSet = context.Set<Integration>(); 
    }

    public IQueryable<Integration> BuildQuery()
    {
        return _dbSet;
    }

    public void Add(Integration item)
    {
        _dbSet.Add(item);
    }

    public void Remove(Integration item)
    {
        _dbSet.Remove(item);
    }
}

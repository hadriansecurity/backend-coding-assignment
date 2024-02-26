using Hadrian.CodingAssignment.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;

namespace Hadrian.CodingAssignment.Infrastructure.Data.EntityFrameworkCore;

public class HadrianDbContext : DbContext
{
    public DbSet<Organization> Organizations => Set<Organization>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Integration> Integrations => Set<Integration>();

    public HadrianDbContext()
    {
    }
    
    public HadrianDbContext(DbContextOptions<HadrianDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql()
            .UseSnakeCaseNamingConvention();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
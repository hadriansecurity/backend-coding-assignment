using Hadrian.CodingAssignment.Infrastructure.Data;
using Hadrian.CodingAssignment.Infrastructure.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hadrian.CodingAssignment.Infrastructure.DI;

public static class EfConfigurationExtensions
{
    public static IServiceCollection AddHadrianDbStorage(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HadrianDbContext>(builder =>
        {
            builder
                .UseNpgsql(configuration.GetConnectionString("Postgres"))
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<DbContext, HadrianDbContext>(sp => sp.GetRequiredService<HadrianDbContext>());

        services.AddScoped<IUnitOfWork, DbContextUnitOfWork<HadrianDbContext>>();

        services.Scan(scan => scan
            .FromApplicationDependencies(x => x.FullName?.Contains("Hadrian") ?? false)
            .AddClasses(classes => classes.Where(x => x.Name.ToLower().EndsWith("repository")))
            .AsSelf()
            .WithScopedLifetime());

        return services;
    }
}
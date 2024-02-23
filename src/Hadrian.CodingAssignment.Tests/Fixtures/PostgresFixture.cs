using Hadrian.CodingAssignment.Infrastructure.Data.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Testcontainers.PostgreSql;

namespace Hadrian.CodingAssignment.Tests.Fixtures;

[CollectionDefinition(nameof(PostgresCollection))]
public sealed class PostgresCollection : ICollectionFixture<PostgresFixture>
{
}

public sealed class PostgresFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;
    private bool _isInitialized = false;

    public PostgresFixture()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:14-bullseye")
            .WithPortBinding(5432, PostgreSqlBuilder.PostgreSqlPort)
            .Build();
    }

    public string ConnectionString =>
        new NpgsqlConnectionStringBuilder(_container.GetConnectionString())
        {
            IncludeErrorDetail = true
        }.ToString();

    public async Task InitializeAsync()
    {
        if (!_isInitialized)
        {
            await _container.StartAsync();

            await using var ctx = CreateContext();
            await ctx.Database.EnsureCreatedAsync();

            _isInitialized = true;
        }
    }

    public async Task DisposeAsync()
    {
        await _container.DisposeAsync();
        _isInitialized = false;
    }

    public DbContext CreateContext()
    {
        return new HadrianDbContext(
            new DbContextOptionsBuilder<HadrianDbContext>()
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseNpgsql(ConnectionString)
                .Options);
    }

    public async Task ResetAsync()
    {
        await using var connection = new NpgsqlConnection(ConnectionString);
        await connection.OpenAsync();

        var tablesToReset = new[]
        {
            "organizations",
            "users",
            "integrations"
        };

        foreach (var table in tablesToReset)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = $"DELETE FROM {table};";
            await command.ExecuteNonQueryAsync();
        }
    }
}

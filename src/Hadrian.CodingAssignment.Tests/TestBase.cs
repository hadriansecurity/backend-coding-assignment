using Hadrian.CodingAssignment.Api;
using Hadrian.CodingAssignment.Tests.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace Hadrian.CodingAssignment.Tests;

[Collection(nameof(PostgresCollection))]
public abstract class TestBase :
    IClassFixture<WebApplicationFactory<Program>>,
    IAsyncLifetime
{
    private readonly PostgresFixture _postgresFixture;
    private readonly WebhookFixture _msteamsFixture;
    private readonly WebApplicationFactory<Program> _webAppFactory;

    protected PostgresFixture Database => _postgresFixture;
    protected WebhookFixture MsTeams => _msteamsFixture;

    protected TestBase(
        PostgresFixture postgresFixture,
        WebApplicationFactory<Program> webAppFactory)
    {
        _postgresFixture = postgresFixture;
        _msteamsFixture = new WebhookFixture();

        var configurationBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(
                new Dictionary<string, string?>
                {
                    ["ConnectionStrings:Postgres"] = Database.ConnectionString,
                });

        var configuration = configurationBuilder.Build();

        _webAppFactory = webAppFactory.WithWebHostBuilder(builder =>
        {
            builder.UseConfiguration(configuration);
        });
    }

    protected HttpClient CreateHttpClient() => _webAppFactory.CreateClient();

    public virtual Task InitializeAsync() => Task.CompletedTask;

    public virtual async Task DisposeAsync()
    {
        await _webAppFactory.DisposeAsync();
        await _postgresFixture.ResetAsync();
        _msteamsFixture.Dispose();
    }
}
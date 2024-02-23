using System.Net.Http.Json;
using FluentAssertions;
using Hadrian.CodingAssignment.Api;
using Hadrian.CodingAssignment.Api.Models;
using Hadrian.CodingAssignment.Infrastructure.Model;
using Hadrian.CodingAssignment.Tests.Extensions;
using Hadrian.CodingAssignment.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Hadrian.CodingAssignment.Tests;

public class IntegrationsControllerTests : TestBase
{
    private static Organization _organization = new Organization("test org");
    private static Integration _integration1 = new MsTeamsIntegration(_organization.Id, "Test integration 1", new Uri("http://google.com"));

    public IntegrationsControllerTests(PostgresFixture postgresFixture, WebApplicationFactory<Program> webAppFactory) : base(postgresFixture, webAppFactory)
    {

    }

    public override async Task InitializeAsync()
    {
        using var ctx = Database.CreateContext();

        ctx.Add(_organization);

        ctx.AddRange(
            _integration1,
            new MsTeamsIntegration(_organization.Id, "Test integration 2", new Uri("http://google.com"))
        );

        ctx.Add(_organization);

        await ctx.SaveChangesAsync();
    }

    [Fact]
    public async Task GetIntegrations()
    {
        var client = CreateHttpClient();

        var response = await client.GetAsync($"organizations/{_organization.Id}/integrations");

        var integrations = await response.AsAsync<MsTeamsIntegration[]>();

        integrations.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetIntegration()
    {
        var client = CreateHttpClient();

        var response = await client.GetAsync($"organizations/{_organization.Id}/integrations/{_integration1.Id}");

        var integration = await response.AsAsync<MsTeamsIntegration>();

        integration.Name.Should().Be(_integration1.Name);
    }

    [Fact]
    public async Task CreateIntegration()
    {
        var client = CreateHttpClient();

        var response = await client.PostAsJsonAsync(
            $"organizations/{_organization.Id}/integrations",
            new MsTeamsIntegrationData("test", new Uri("https://hadrian.io")));

        using var ctx = Database.CreateContext();
        ctx.Set<Integration>().Should().Contain(x => x.Name == "test");
    }

    [Fact]
    public async Task DeleteIntegration()
    {
        var client = CreateHttpClient();

        var response = await client.PostAsync($"organizations/{_organization.Id}/integrations/{_integration1.Id}/delete-msteams", null);

        response.Should().BeSuccessful();
    }
}
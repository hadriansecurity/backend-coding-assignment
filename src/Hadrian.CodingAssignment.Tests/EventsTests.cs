using System.Net.Http.Json;
using FluentAssertions;
using Hadrian.CodingAssignment.Api;
using Hadrian.CodingAssignment.Api.Models;
using Hadrian.CodingAssignment.Infrastructure.Model;
using Hadrian.CodingAssignment.Tests.Fixtures;
using Microsoft.AspNetCore.Mvc.Testing;
using WireMock.FluentAssertions;

namespace Hadrian.CodingAssignment.Tests;

public class EventsTests : TestBase
{
    private Organization _organization = new Organization("test org");
    private Integration _integration1;

    public EventsTests(PostgresFixture postgresFixture, WebApplicationFactory<Program> webAppFactory) : base(postgresFixture, webAppFactory)
    {

    }

    public override async Task InitializeAsync()
    {
        using var ctx = Database.CreateContext();

        ctx.Add(_organization);

        _integration1 =  new MsTeamsIntegration(_organization.Id, "Test integration 1", MsTeams.WebhookUrl)
        {
            NotifyWhenAssetIsFound = true,
            NotifyWhenRiskIsFound = true
        };

        ctx.Add(_integration1);

        await ctx.SaveChangesAsync();
    }

    [Fact]
    public async void NotificationEvent()
    {
        var client = CreateHttpClient();

        var response = await client.PostAsJsonAsync(
            "events",
            new NotificationEvent(
                _organization.Id,
                "This is a test message, a new risk has been found!",
                NotificationType.Risk));

        MsTeams.Server.Should()
            .HaveReceived(1).Calls()
            .UsingPost()
            .And.WithBodyAsJson(new { text =  "This is a test message, a new risk has been found!" });
    }
}
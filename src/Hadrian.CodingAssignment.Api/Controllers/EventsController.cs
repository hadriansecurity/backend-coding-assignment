using Microsoft.AspNetCore.Mvc;
using Hadrian.CodingAssignment.Api.Models;
using Hadrian.CodingAssignment.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Hadrian.CodingAssignment.Infrastructure.Model;

namespace Hadrian.CodingAssignment.Api.Controllers;

[ApiController]
[Route("events")]
public class EventsController : ControllerBase
{
    private readonly IntegrationsRepository _integrationsRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public EventsController(
        IntegrationsRepository integrationsRepository,
        IHttpClientFactory httpClientFactory)
    {
        _integrationsRepository = integrationsRepository;
        _httpClientFactory = httpClientFactory;
    }

    [HttpPost]
    public async Task<ActionResult> EventReceived(
        [FromBody] NotificationEvent eventData,
        CancellationToken cancellationToken = default)
    {
        var integrations = _integrationsRepository
            .BuildQuery()
            .Include(x => x.Organization)
            .Where(x => x.OrganizationId == eventData.OrganizationId)
            .Where(x => eventData.NotificationType == NotificationType.Asset ? x.NotifyWhenAssetIsFound : x.NotifyWhenRiskIsFound)
            .ToArray();
            
        var client = _httpClientFactory.CreateClient();
        foreach (var integration in integrations)
        {
            await client.PostAsJsonAsync(
                (integration as MsTeamsIntegration)?.Webhook,
                new {
                    text = eventData.Message
                });
        }

        return Ok();
    }
}
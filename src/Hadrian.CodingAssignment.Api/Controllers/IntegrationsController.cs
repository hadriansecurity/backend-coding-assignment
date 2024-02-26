using Microsoft.AspNetCore.Mvc;
using Hadrian.CodingAssignment.Api.Models;
using Hadrian.CodingAssignment.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Hadrian.CodingAssignment.Infrastructure.Model;
using Hadrian.CodingAssignment.Infrastructure.Data;

namespace Hadrian.CodingAssignment.Api.Controllers;

[ApiController]
[Route("organizations/{organizationId}/integrations")]
public class IntegrationsController : ControllerBase
{
    private readonly IntegrationsRepository _integrationsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public IntegrationsController(
        IntegrationsRepository integrationsRepository,
        IUnitOfWork unitOfWork)
    {
        _integrationsRepository = integrationsRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Integration[]> GetIntegrations(
        [FromRoute] Guid organizationId)
    {
        var integrations = _integrationsRepository
            .BuildQuery()
            .Include(x => x.Organization)
            .Where(x => x.OrganizationId == organizationId)
            .ToArray();

        return integrations;
    }

    [HttpGet("{integrationId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Integration> GetIntegration(
        [FromRoute] Guid organizationId,
        [FromRoute] Guid integrationId)
    {
        var integration = _integrationsRepository
            .BuildQuery()
            .Include(x => x.Organization)
            .Single(x => x.OrganizationId == organizationId && x.Id == integrationId);

        return integration;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<Integration>> CreateMsTeamsIntegration(
        [FromRoute] Guid organizationId,
        [FromBody] MsTeamsIntegrationData data,
        CancellationToken cancellationToken = default)
    {
        var integration = new MsTeamsIntegration(organizationId, data.Name, data.Webhook);
        _integrationsRepository.Add(integration);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(GetIntegration), integration);
    }

    [HttpPost("{integrationId}/delete-msteams")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public void DeleteMsTeamsIntegration(
        [FromRoute] Guid organizationId,
        [FromRoute] Guid integrationId,
        CancellationToken cancellationToken = default)
    {
        var integration = _integrationsRepository
            .BuildQuery()
            .Include(x => x.Organization)
            .Single(x => x.OrganizationId == organizationId && x.Id == integrationId);

        _integrationsRepository.Remove(integration);
    }
}
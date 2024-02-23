using System.ComponentModel.DataAnnotations;
namespace Hadrian.CodingAssignment.Api.Models;

/// <summary>
/// MsTeams Integration Data
/// </summary>
public record MsTeamsIntegrationData(
    [Required] string Name,
    [Required] Uri Webhook
);

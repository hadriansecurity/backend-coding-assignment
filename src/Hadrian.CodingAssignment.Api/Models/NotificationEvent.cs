using System.ComponentModel.DataAnnotations;
using Hadrian.CodingAssignment.Infrastructure.Model;

namespace Hadrian.CodingAssignment.Api.Models;

/// <summary>
/// Notification Event
/// </summary>
public record NotificationEvent(
    [Required] Guid OrganizationId,
    [Required] string Message,
    [Required] NotificationType NotificationType
);

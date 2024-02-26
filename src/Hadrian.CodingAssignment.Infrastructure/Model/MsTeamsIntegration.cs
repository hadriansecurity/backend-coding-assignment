namespace Hadrian.CodingAssignment.Infrastructure.Model;

public class MsTeamsIntegration : Integration
{
    public Uri Webhook { get; init; }

    public MsTeamsIntegration(Guid organizationId, string name, Uri webhook) : base(organizationId, IntegrationType.MsTeams, name)
    {
        Webhook = webhook;
    }
}

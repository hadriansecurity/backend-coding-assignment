using System.Text.Json.Serialization;

namespace Hadrian.CodingAssignment.Infrastructure.Model;

public abstract class Integration
{
    public Guid Id { get; }

    public IntegrationType IntegrationType { get; init; }

    public string Name { get; set; }

    public bool NotifyWhenAssetIsFound { get; set; }

    public bool NotifyWhenRiskIsFound { get; set; }

    public Guid OrganizationId { get; init; }

    [JsonIgnore]
    public Organization? Organization { get; }
    
    public Integration(Guid organizationId, IntegrationType integrationType, string name)
    {
        Id = Guid.NewGuid();
        IntegrationType = integrationType;
        Name = name;

        OrganizationId = organizationId;
    }
}

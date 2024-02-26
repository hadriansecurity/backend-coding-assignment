using System.Text.Json.Serialization;

namespace Hadrian.CodingAssignment.Infrastructure.Model;

public class User
{
    public Guid Id { get; }

    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public Guid OrganizationId { get; }

    [JsonIgnore]
    public Organization? Organization { get; }

    public User(Guid organizationId, string username, string passwordHash)
    {
        Id = Guid.NewGuid();
        Username = username;
        PasswordHash = passwordHash;

        OrganizationId = organizationId;
    }
}

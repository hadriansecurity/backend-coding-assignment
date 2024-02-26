using Hadrian.CodingAssignment.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hadrian.CodingAssignment.Infrastructure.Data.EntityFrameworkCore.Configurations;

internal class MsTeamsIntegrationConfiguration : IEntityTypeConfiguration<MsTeamsIntegration>
{
    public void Configure(EntityTypeBuilder<MsTeamsIntegration> builder)
    {

    }
}

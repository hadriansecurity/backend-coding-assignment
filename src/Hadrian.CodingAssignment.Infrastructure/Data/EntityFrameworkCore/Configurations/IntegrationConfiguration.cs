using Hadrian.CodingAssignment.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hadrian.CodingAssignment.Infrastructure.Data.EntityFrameworkCore.Configurations;

internal class IntegrationConfiguration : IEntityTypeConfiguration<Integration>
{
    public void Configure(EntityTypeBuilder<Integration> builder)
    {
        builder.UseTphMappingStrategy();

        builder
            .HasKey(x => x.Id);

        builder
            .Property(x => x.IntegrationType)
            .HasConversion<string>();
    }
}

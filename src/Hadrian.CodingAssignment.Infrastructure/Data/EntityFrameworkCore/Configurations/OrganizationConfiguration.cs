using System.ComponentModel;
using Hadrian.CodingAssignment.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hadrian.CodingAssignment.Infrastructure.Data.EntityFrameworkCore.Configurations;

internal class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasMany(x => x.Users)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId);

        builder
            .HasMany(x => x.Integrations)
            .WithOne(x => x.Organization)
            .HasForeignKey(x => x.OrganizationId);
    }
}

using System.ComponentModel;
using Hadrian.CodingAssignment.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Hadrian.CodingAssignment.Infrastructure.Data.EntityFrameworkCore.Configurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasKey(x => x.Id);
    }
}

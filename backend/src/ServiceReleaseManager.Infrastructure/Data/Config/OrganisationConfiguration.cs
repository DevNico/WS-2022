using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
{
  public void Configure(EntityTypeBuilder<Organisation> builder)
  {
    builder.Property(p => p.Name)
      .HasMaxLength(100)
      .IsRequired();
  }
}

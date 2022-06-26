using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
{
  public void Configure(EntityTypeBuilder<Organisation> builder)
  {
    builder
      .HasIndex(organisation => organisation.RouteName)
      .IsUnique();

    builder
      .HasMany(organisation => organisation.Roles)
      .WithOne()
      .HasForeignKey(role => role.OrganisationId)
      .IsRequired();

    builder
      .HasMany(organisation => organisation.Users)
      .WithOne()
      .HasForeignKey(user => user.OrganisationId)
      .IsRequired();
    
    builder
      .HasMany(organisation => organisation.ServiceTemplates)
      .WithOne(serviceTemplate => serviceTemplate.Organisation)
      .HasForeignKey(serviceTemplate => serviceTemplate.OrganisationId)
      .IsRequired();

    builder
      .HasMany(organisation => organisation.Services)
      .WithOne()
      .HasForeignKey(service => service.OrganisationId)
      .IsRequired();

    builder
      .HasMany(organisation => organisation.ServiceRoles)
      .WithOne()
      .HasForeignKey(serviceRole => serviceRole.OrganisationId)
      .IsRequired();
  }
}

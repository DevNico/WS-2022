using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class OrganisationUserConfiguration : IEntityTypeConfiguration<OrganisationUser>
{
  public void Configure(EntityTypeBuilder<OrganisationUser> builder)
  {
    builder
      .HasOne(organisationUser => organisationUser.Role)
      .WithMany(organisationRole => organisationRole.Users)
      .HasForeignKey(organisationUser => organisationUser.RoleId)
      .IsRequired();

    builder
      .HasMany(user => user.ServiceUser)
      .WithOne(serviceUser => serviceUser.OrganisationUser)
      .HasForeignKey(serviceUser => serviceUser.OrganisationUserId)
      .IsRequired();
  }
}

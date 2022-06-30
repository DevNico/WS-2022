using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class UserServiceConfiguration : IEntityTypeConfiguration<ServiceUser>
{
  public void Configure(EntityTypeBuilder<ServiceUser> builder)
  {
    builder
     .HasOne(serviceUser => serviceUser.OrganisationUser)
     .WithMany()
     .HasForeignKey(serviceUser => serviceUser.OrganisationUserId)
     .IsRequired();

    builder
     .HasOne(serviceUser => serviceUser.ServiceRole)
     .WithMany()
     .HasForeignKey(serviceUser => serviceUser.ServiceRoleId)
     .IsRequired();
  }
}

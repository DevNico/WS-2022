using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class UserServiceConfiguration : IEntityTypeConfiguration<UserSevice>
{
  public void Configure(EntityTypeBuilder<UserSevice> builder)
  {
    builder.HasOne(p => p.Service).WithMany().IsRequired();
    builder.HasOne(p => p.ServiceRole).WithMany().IsRequired();
  }
}

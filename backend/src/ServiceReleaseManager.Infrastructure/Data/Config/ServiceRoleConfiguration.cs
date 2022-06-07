using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class ServiceRoleConfiguration : IEntityTypeConfiguration<ServiceRole>
{
  public void Configure(EntityTypeBuilder<ServiceRole> builder)
  {
    builder.HasIndex(i => i.Name)
      .IsUnique();
  }
}

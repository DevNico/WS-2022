using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class ServiceTemplateConfiguration : IEntityTypeConfiguration<ServiceTemplate>
{
  public void Configure(EntityTypeBuilder<ServiceTemplate> builder)
  {
    builder
      .HasIndex(s => s.Name)
      .IsUnique();
  }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
  public void Configure(EntityTypeBuilder<Service> builder)
  {
    builder
     .HasIndex(service => service.RouteName)
     .IsUnique();

    builder
     .HasOne(service => service.Organisation)
     .WithMany(org => org.Services)
     .HasForeignKey(service => service.OrganisationId)
     .IsRequired();

    builder
     .HasOne(service => service.ServiceTemplate)
     .WithMany()
     .HasForeignKey(service => service.ServiceTemplateId)
     .IsRequired();

    builder
     .HasMany(service => service.Locales)
     .WithOne()
     .HasForeignKey(l => l.ServiceId)
     .IsRequired();

    builder
     .HasMany(service => service.Users)
     .WithOne()
     .HasForeignKey(l => l.ServiceId)
     .IsRequired();

    builder
     .HasMany(service => service.Releases)
     .WithOne()
     .HasForeignKey(release => release.ServiceId)
     .IsRequired();

    /*builder
      .HasMany(service => service.ReleaseTargets)
      .WithOne()
      .HasForeignKey(release => release.ServiceId)
      .IsRequired();*/
  }
}

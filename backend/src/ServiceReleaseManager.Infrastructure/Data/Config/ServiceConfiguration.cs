﻿using Microsoft.EntityFrameworkCore;
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

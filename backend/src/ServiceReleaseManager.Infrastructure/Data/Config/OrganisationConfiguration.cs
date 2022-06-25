﻿using Microsoft.EntityFrameworkCore;
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
      .WithOne(user => user.Organisation)
      .HasForeignKey(user => user.OrganisationId)
      .IsRequired();

    builder
      .HasMany(organisation => organisation.Services)
      .WithOne()
      .HasForeignKey(service => service.OrganisationId)
      .IsRequired();
  }
}

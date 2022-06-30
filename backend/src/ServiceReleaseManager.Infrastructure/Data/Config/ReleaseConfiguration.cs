using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class ReleaseConfiguration : IEntityTypeConfiguration<Release>
{
  public void Configure(EntityTypeBuilder<Release> builder)
  {
    builder
     .HasOne(release => release.ApprovedBy)
     .WithMany()
     .HasForeignKey(release => release.ApprovedById);

    builder
     .HasOne(release => release.PublishedBy)
     .WithMany()
     .HasForeignKey(release => release.PublishedById);

    builder
     .HasMany(release => release.LocalisedMetadataList)
     .WithOne(localisedMetadata => localisedMetadata.Release)
     .HasForeignKey(localisedMetadata => localisedMetadata.ReleaseId)
     .IsRequired();
  }
}

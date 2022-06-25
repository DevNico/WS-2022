using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class ReleaseConfiguration : IEntityTypeConfiguration<Release>
{
  public void Configure(EntityTypeBuilder<Release> builder)
  {
    builder
      .HasMany(release => release.LocalisedMetadataList)
      .WithOne(localisedMetadata => localisedMetadata.Release)
      .HasForeignKey(localisedMetadata => localisedMetadata.ReleaseId)
      .IsRequired();
  }
}

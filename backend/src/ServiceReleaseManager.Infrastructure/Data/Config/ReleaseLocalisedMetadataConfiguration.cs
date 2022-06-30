using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Infrastructure.Data.Config;

public class
  ReleaseLocalisedMetadataConfiguration : IEntityTypeConfiguration<ReleaseLocalisedMetadata>
{
  public void Configure(EntityTypeBuilder<ReleaseLocalisedMetadata> builder)
  {
    builder
     .HasOne(localisedMetadata => localisedMetadata.Locale)
     .WithMany()
     .HasForeignKey(localisedMetadata => localisedMetadata.LocaleId);
  }
}

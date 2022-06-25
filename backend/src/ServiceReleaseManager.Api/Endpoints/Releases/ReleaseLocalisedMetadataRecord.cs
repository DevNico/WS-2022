using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public record ReleaseLocalisedMetadataRecord(string Metadata, int LocaleId)
{
  public static ReleaseLocalisedMetadataRecord FromEntity(ReleaseLocalisedMetadata metadata)
  {
    return new ReleaseLocalisedMetadataRecord(
      metadata.Metadata,
      metadata.LocaleId
    );
  }
}

using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public record ReleaseRecord(
  int Id,
  string Version,
  int ServiceId,
  string Metadata,
  List<ReleaseLocalisedMetadataRecord> LocalisedMetadata,
  DateTime UpdatedAt,
  DateTime CreatedAt
)
{
  public static ReleaseRecord FromEntity(Release entity)
  {
    return new ReleaseRecord(
      entity.Id,
      entity.Version,
      entity.ServiceId,
      entity.Metadata,
      entity
       .LocalisedMetadataList
       .Select(ReleaseLocalisedMetadataRecord.FromEntity)
       .ToList(),
      entity.UpdatedAt,
      entity.CreatedAt
    );
  }
}

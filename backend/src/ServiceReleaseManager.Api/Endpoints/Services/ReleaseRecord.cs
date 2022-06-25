using Newtonsoft.Json;
using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public record ReleaseRecord(
  string Version,
  object? Metadata,
  int ServiceId,
  int? ApprovedBy,
  DateTime? ApprovedAt,
  int? PublishedBy,
  DateTime? PublishedAt
)
{
  public static ReleaseRecord FromEntity(Release r)
  {
    var metadata = JsonConvert.DeserializeObject(r.Metadata);
    return new ReleaseRecord(r.Version, metadata, r.ServiceId, r.ApprovedBy?.Id, r.ApprovedAt,
      r.PublishedBy?.Id, r.PublishedAt);
  }
}

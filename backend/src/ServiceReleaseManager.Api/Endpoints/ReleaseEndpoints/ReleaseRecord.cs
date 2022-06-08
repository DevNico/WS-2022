using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public record ReleaseRecord(int Id, string version, int builNumber, string metadata, DateTime UpdatedAt, DateTime CreatedAt)
{
  public static ReleaseRecord FromEntity(Release entity)
  {
    return new ReleaseRecord(entity.Id, entity.Version, entity.BuildNumber, entity.Metadata, entity.UpdatedAt, entity.CreatedAt);
  }
}

using ServiceReleaseManager.Api.Endpoints.Releases;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public record ServiceRecord(
  int Id,
  string Name,
  string RouteName,
  string Description,
  ReleaseRecord? LatestRelease,
  int organisationId,
  DateTime UpdatedAt,
  DateTime CreatedAt
)
{
  public static ServiceRecord FromEntity(Service service)
  {
    var latestSpec = new LatestReleaseSpec();
    var latestRelease = latestSpec.Evaluate(service.Releases).FirstOrDefault();

    return new ServiceRecord(
      service.Id,
      service.Name,
      service.RouteName,
      service.Description,
      latestRelease != null ? ReleaseRecord.FromEntity(latestRelease) : null,
      service.OrganisationId,
      service.UpdatedAt,
      service.CreatedAt
    );
  }
}

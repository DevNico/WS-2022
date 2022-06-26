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
      Id: service.Id,
      Name: service.Name,
      RouteName: service.RouteName,
      Description: service.Description,
      LatestRelease: latestRelease != null ? ReleaseRecord.FromEntity(latestRelease) : null,
      organisationId: service.OrganisationId,
      UpdatedAt: service.UpdatedAt,
      CreatedAt: service.CreatedAt
    );
  }
}

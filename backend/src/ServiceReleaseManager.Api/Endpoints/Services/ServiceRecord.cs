using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public record ServiceRecord(int ServiceId, string Name, string Description, List<Locale> Locales,
  List<ServiceUser> ServiceUsers, List<Release> Releases, List<ReleaseTarget> ReleaseTargets,
  int OrganisationId)
{
  public static ServiceRecord FromEntity(Service service)
  {
    return new ServiceRecord(service.Id, service.Name, service.Description, service.Locales,
      service.ServiceUsers, service.Releases, service.ReleaseTargets, service.OrganisationId);
  }
}

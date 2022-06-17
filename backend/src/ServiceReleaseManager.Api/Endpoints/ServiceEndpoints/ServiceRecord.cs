using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public record ServiceRecord(int ServiceId, string Name, List<Release> Releases, List<Locale> Locales, List<ReleaseTarget> ReleaseTargets)
{
  public static ServiceRecord FromEntity(Service service)
  {
    return new ServiceRecord(service.Id, service.Name, service.Releases, service.Locales, service.ReleaseTargets);
  }
}

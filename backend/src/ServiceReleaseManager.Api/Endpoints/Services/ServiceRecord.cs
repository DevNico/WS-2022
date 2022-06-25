using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public record ServiceRecord(int ServiceId, string Name, string Description)
{
  public static ServiceRecord FromEntity(Service service)
  {
    return new ServiceRecord(service.Id, service.Name, service.Description);
  }
}

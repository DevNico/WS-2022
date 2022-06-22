using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IServiceUserService
{
  public Task<ServiceUser?> getByOrganisationUserId(int organisationUserId, CancellationToken cancellationToken);

}

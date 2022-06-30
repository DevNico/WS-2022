using Ardalis.Result;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IServiceUserService
{
  public Task<Result<List<ServiceUser>>> GetByServiceId(
    int serviceId,
    CancellationToken cancellationToken
  );

  public Task<Result<ServiceUser>> GetById(int id, CancellationToken cancellationToken);

  public Task<Result<ServiceUser>> Create(
    int serviceId,
    int serviceRoleId,
    int organisationUserId,
    CancellationToken cancellationToken
  );

  public Task<Result> Deactivate(int id, CancellationToken cancellationToken);

  public Task<Result<ServiceUser>> GetOrganisationUserById(
    int organisationUserid,
    int serviceId,
    CancellationToken cancellationToken
  );
}

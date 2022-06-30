using Ardalis.Result;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IServiceRoleService
{
  public Task<Result<ServiceRole>> Create(
    int organisationId,
    ServiceRole serviceRole,
    CancellationToken cancellationToken
  );

  public Task<Result<List<ServiceRole>>> GetByOrganisationId(
    int organisationId,
    CancellationToken cancellationToken
  );

  public Task<Result<ServiceRole>> GetById(int id, CancellationToken cancellationToken);

  public Task<Result<List<ServiceRole>>> GetByName(
    string name,
    CancellationToken cancellationToken
  );

  public Task<Result> Deactivate(int id, CancellationToken cancellationToken);
}

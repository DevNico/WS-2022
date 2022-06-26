using Ardalis.Result;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IServiceService
{
  public Task<Result<Service>> Create(Service service, CancellationToken cancellationToken);

  public Task<Result<List<Service>>> GetByOrganisationUserIds(ICollection<int> organisationUserIds,
    CancellationToken cancellationToken);

  public Task<Result<Service>> GetById(int id, CancellationToken cancellationToken);

  public Task<Result<Service>> GetByRouteName(
    string serviceRouteName,
    CancellationToken cancellationToken
  );

  public Task<Result<Service>> GetByNameAndOrganisationId(string name, int organisationId,
    CancellationToken cancellationToken);

  public Task Update(Service service, CancellationToken cancellationToken);

  public Task<Result> Deactivate(int serviceId, CancellationToken cancellationToken);
}

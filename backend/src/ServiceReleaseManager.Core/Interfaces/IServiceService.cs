using Ardalis.Result;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IServiceService
{
  public Task<Result<Service>> Create(
    string name,
    string description,
    int serviceTemplateId,
    CancellationToken cancellationToken
  );

  public Task<Result<List<Service>>> GetByOrganisationUserId(
    int organisationUserId,
    CancellationToken cancellationToken
  );

  public Task<Result<Service>> GetById(int id, CancellationToken cancellationToken);

  public Task<Result<Service>> GetByRouteName(
    string serviceRouteName,
    CancellationToken cancellationToken
  );

  public Task<Result<Service>> GetByNameAndOrganisationId(
    string name,
    int organisationId,
    CancellationToken cancellationToken
  );

  public Task Update(Service service, CancellationToken cancellationToken);

  public Task<Result> Deactivate(int serviceId, CancellationToken cancellationToken);

  public Task<Result<ServiceTemplate>> GetServiceTemplate(int serviceTemplateId,
    CancellationToken cancellationToken);
}

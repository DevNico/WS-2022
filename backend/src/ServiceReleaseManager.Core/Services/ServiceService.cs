using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class ServiceService : IServiceService
{
  private readonly IRepository<Service> _repository;

  public ServiceService(IRepository<Service> repository)
  {
    _repository = repository;
  }

  public async Task<Result<Service>> Create(Service service, CancellationToken cancellationToken)
  {
    var created = await _repository.AddAsync(service, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success(created);
  }

  public async Task<Result<List<Service>>> GetByOrganisationUserIds(
    ICollection<int> organisationUserIds,
    CancellationToken cancellationToken)
  {
    var spec = new ServicesByOrganisationUserIdsSpec(organisationUserIds);
    var result = await _repository.ListAsync(spec, cancellationToken);

    return Result.Success(result);
  }

  public async Task<Result<Service>> GetById(int id, CancellationToken cancellationToken)
  {
    var spec = new ServiceByIdSpec(id);
    var service = await _repository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(service);
  }

  public async Task<Result<Service>> GetByNameAndOrganisationId(string name, int organisationId,
    CancellationToken cancellationToken)
  {
    var spec = new ServiceByNameAndOrganisationIdSpec(name, organisationId);
    var result = await _repository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(result);
  }

  public async Task Update(Service service, CancellationToken cancellationToken)
  {
    await _repository.UpdateAsync(service, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);
  }

  public async Task<Result> Deactivate(int serviceId, CancellationToken cancellationToken)
  {
    var service = await GetById(serviceId, cancellationToken);
    if (!service.IsSuccess)
    {
      return Result.NotFound();
    }

    service.Value.Deactivate();
    await Update(service, cancellationToken);

    return Result.Success();
  }
}

using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class ServiceService : IServiceService
{
  private readonly IRepository<Service> _serviceRepository;
  private readonly IRepository<Organisation> _organisationRepository;

  public ServiceService(IRepository<Service> serviceRepository,
    IRepository<Organisation> organisationRepository)
  {
    _serviceRepository = serviceRepository;
    _organisationRepository = organisationRepository;
  }

  public async Task<Result<Service>> Create(Service service, CancellationToken cancellationToken)
  {
    var created = await _serviceRepository.AddAsync(service, cancellationToken);
    await _serviceRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(created);
  }

  public async Task<Result<List<Service>>> GetByOrganisationUserIds(
    ICollection<int> organisationUserIds,
    CancellationToken cancellationToken)
  {
    var spec = new ServicesByOrganisationUserIdsSpec(organisationUserIds);
    var result = await _serviceRepository.ListAsync(spec, cancellationToken);

    return Result.Success(result);
  }

  public async Task<Result<Service>> GetById(int id, CancellationToken cancellationToken)
  {
    var spec = new ServiceByIdSpec(id);
    var service = await _serviceRepository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(service);
  }

  public async Task<Result<Service>> GetByRouteName(
    string serviceRouteName,
    CancellationToken cancellationToken)
  {
    var serviceSpec = new ServiceByRouteNameSpec(serviceRouteName);
    var service = await _serviceRepository.GetBySpecAsync(serviceSpec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(service);
  }

  public async Task<Result<Service>> GetByNameAndOrganisationId(string name, int organisationId,
    CancellationToken cancellationToken)
  {
    var spec = new ServiceByNameAndOrganisationIdSpec(name, organisationId);
    var result = await _serviceRepository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(result);
  }

  public async Task Update(Service service, CancellationToken cancellationToken)
  {
    await _serviceRepository.UpdateAsync(service, cancellationToken);
    await _serviceRepository.SaveChangesAsync(cancellationToken);
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

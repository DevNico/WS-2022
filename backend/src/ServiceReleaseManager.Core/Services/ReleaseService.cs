using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class ReleaseService : IReleaseService
{
  private readonly IRepository<Release> _repository;
  private readonly IRepository<Service> _serviceRepository;

  public ReleaseService(IRepository<Release> repository, IRepository<Service> serviceRepository)
  {
    _repository = repository;
    _serviceRepository = serviceRepository;
  }

  public async Task<Result<List<Release>>> GetByServiceId(int id, CancellationToken cancellationToken)
  {
    var serviceSpec = new ServiceByIdSpec(id);
    var services = await _serviceRepository.CountAsync(serviceSpec, cancellationToken);
    if (services == 0)
    {
      return Result<List<Release>>.NotFound();
    }

    var spec = new ReleaseByServiceIdSpec(id);
    var releases = await _repository.ListAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(releases);
  }
}

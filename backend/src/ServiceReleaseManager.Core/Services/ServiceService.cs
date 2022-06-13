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

  public async Task<Result<Service>> GetById(int id, CancellationToken cancellationToken)
  {
    var spec = new ServiceByIdSpec(id);
    var service = await _repository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(service);
  }
}

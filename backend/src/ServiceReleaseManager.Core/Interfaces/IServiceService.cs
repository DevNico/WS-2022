using Ardalis.Result;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IServiceService
{
  public Task<Result<Service>> GetById(int id, CancellationToken cancellationToken);
}

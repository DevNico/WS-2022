using Ardalis.Result;
using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IReleaseService
{
  public Task<Result<List<Release>>> GetByServiceId(int id, CancellationToken cancellationToken);
}

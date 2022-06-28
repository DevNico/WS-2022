using Ardalis.Result;
using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IReleaseTriggerService
{
  Task<Result<ReleaseTrigger>> GetById(int id, CancellationToken cancellationToken);
  Task<Result<ReleaseTrigger>> Create(ReleaseTrigger trigger, CancellationToken cancellationToken);
  Task<Result> Delete(int releaseTriggerId, CancellationToken cancellationToken);
}

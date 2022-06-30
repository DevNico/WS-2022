using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class ReleaseTriggerService : IReleaseTriggerService
{
  private readonly IRepository<ReleaseTrigger> _repository;

  public ReleaseTriggerService(IRepository<ReleaseTrigger> repository)
  {
    _repository = repository;
  }

  public async Task<Result<ReleaseTrigger>> GetById(int id, CancellationToken cancellationToken)
  {
    var spec = new ReleaseTriggerByIdSpec(id);
    var organisation = await _repository.GetBySpecAsync(spec, cancellationToken);

    return ResultHelper.NullableSuccessNotFound(organisation);
  }

  public async Task<Result<ReleaseTrigger>> Create(
    ReleaseTrigger trigger,
    CancellationToken cancellationToken
  )
  {
    var created = await _repository.AddAsync(trigger, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return ResultHelper.NullableSuccessNotFound(created);
  }

  public async Task<Result> Delete(int releaseTriggerId, CancellationToken cancellationToken)
  {
    var role = await GetById(releaseTriggerId, cancellationToken);
    if (!role.IsSuccess)
    {
      return Result.NotFound();
    }

    await _repository.DeleteAsync(role.Value, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}

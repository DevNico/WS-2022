using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class ServiceRoleService : IServiceRoleService
{
  private readonly IOrganisationService _organisationService;
  private readonly IRepository<ServiceRole> _repository;

  public ServiceRoleService(
    IRepository<ServiceRole> repository,
    IOrganisationService organisationService
  )
  {
    _repository = repository;
    _organisationService = organisationService;
  }

  public async Task<Result<ServiceRole>> Create(
    int organisationId,
    ServiceRole serviceRole,
    CancellationToken cancellationToken
  )
  {
    var organisation = await _organisationService.GetById(organisationId, cancellationToken);
    if (!organisation.IsSuccess)
    {
      return Result.NotFound();
    }

    var created = await _repository.AddAsync(serviceRole, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    organisation.Value.ServiceRoles.Add(created);
    await _organisationService.Update(organisation, cancellationToken);

    return ResultHelper.NullableSuccessNotFound(created);
  }

  public async Task<Result<List<ServiceRole>>> GetByOrganisationId(
    int organisationId,
    CancellationToken cancellationToken
  )
  {
    var spec = new ServiceRolesByOrganisationIdSpec(organisationId);
    var result = await _repository.ListAsync(spec, cancellationToken);

    return Result.Success(result);
  }

  public async Task<Result<ServiceRole>> GetById(int id, CancellationToken cancellationToken)
  {
    var spec = new ServiceRoleByIdSpec(id);
    var role = await _repository.GetBySpecAsync(spec, cancellationToken);

    return ResultHelper.NullableSuccessNotFound(role);
  }

  public async Task<Result<List<ServiceRole>>> GetByName(
    string name,
    CancellationToken cancellationToken
  )
  {
    var spec = new ServiceRoleByNameSpec(name);
    var role = await _repository.ListAsync(spec, cancellationToken);

    return ResultHelper.NullableSuccessNotFound(role);
  }

  public async Task<Result> Deactivate(int id, CancellationToken cancellationToken)
  {
    var role = await GetById(id, cancellationToken);
    if (!role.IsSuccess)
    {
      return Result.NotFound();
    }

    role.Value.Deactivate();
    await _repository.UpdateAsync(role.Value, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}

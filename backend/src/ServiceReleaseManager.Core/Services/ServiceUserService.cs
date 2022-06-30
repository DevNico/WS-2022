using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class ServiceUserService : IServiceUserService
{
  private readonly IOrganisationUserService _organisationUserService;
  private readonly IRepository<ServiceUser> _repository;
  private readonly IServiceRoleService _serviceRoleService;
  private readonly IServiceService _serviceService;

  public ServiceUserService(
    IRepository<ServiceUser> repository,
    IServiceService serviceService,
    IOrganisationUserService organisationUserService,
    IServiceRoleService serviceRoleService
  )
  {
    _repository = repository;
    _serviceService = serviceService;
    _organisationUserService = organisationUserService;
    _serviceRoleService = serviceRoleService;
  }

  public async Task<Result<List<ServiceUser>>> GetByServiceId(
    int serviceId,
    CancellationToken cancellationToken
  )
  {
    var service = await _serviceService.GetById(serviceId, cancellationToken);
    if (!service.IsSuccess)
    {
      return Result.NotFound();
    }

    var spec = new ServiceUsersByServiceIdSpec(serviceId);
    var result = await _repository.ListAsync(spec, cancellationToken);
    return result;
  }

  public async Task<Result<ServiceUser>> GetById(int id, CancellationToken cancellationToken)
  {
    var spec = new ServiceUserByIdSpec(id);
    var result = await _repository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(result);
  }

  public async Task<Result<ServiceUser>> Create(
    int serviceId,
    int serviceRoleId,
    int organisationUserId,
    CancellationToken cancellationToken
  )
  {
    var user = await _organisationUserService.GetById(organisationUserId, cancellationToken);
    if (!user.IsSuccess)
    {
      return Result.NotFound();
    }

    var role = await _serviceRoleService.GetById(serviceRoleId, cancellationToken);
    if (!role.IsSuccess)
    {
      return Result.NotFound();
    }

    var serviceResult = await _serviceService.GetById(serviceId, cancellationToken);
    if (!serviceResult.IsSuccess)
    {
      return Result.NotFound();
    }

    if (user.Value.OrganisationId != role.Value.OrganisationId ||
        role.Value.OrganisationId != serviceResult.Value.OrganisationId)
    {
      return Result<ServiceUser>.Invalid(
        new List<ValidationError>
        {
          new()
          {
            ErrorCode = "InvalidOrganisationId",
            ErrorMessage = "The user, role or service organisation did not match",
            Severity = ValidationSeverity.Error
          }
        }
      );
    }

    var serviceUser = new ServiceUser(serviceResult.Value.Id, role, user);
    var result = await _repository.AddAsync(serviceUser, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    var service = serviceResult.Value;
    service.Users.Add(result);
    await _serviceService.Update(service, cancellationToken);

    return result;
  }

  public async Task<Result> Deactivate(int id, CancellationToken cancellationToken)
  {
    var user = await GetById(id, cancellationToken);
    if (!user.IsSuccess)
    {
      return Result.NotFound();
    }

    var u = user.Value;
    u.Deactivate();
    await _repository.UpdateAsync(u, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }

  public async Task<Result<ServiceUser>> GetOrganisationUserById(
    int organisationUserid,
    int serviceId,
    CancellationToken cancellationToken
  )
  {
    var spec = new ServiceUserByOrganisationUserIdSpec(organisationUserid, serviceId);
    var result = await _repository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(result);
  }
}

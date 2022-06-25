using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class OrganisationUserService : IOrganisationUserService
{
  private readonly IOrganisationService _organisationService;

  private readonly IRepository<OrganisationUser> _organisationUserRepository;

  public OrganisationUserService(IRepository<OrganisationUser> organisationUserRepository,
    IOrganisationService organisationService)
  {
    _organisationUserRepository = organisationUserRepository;
    _organisationService = organisationService;
  }

  public async Task<Result<OrganisationUser>> Create(OrganisationUser organisationUser,
    CancellationToken cancellationToken)
  {
    var spec =
      new OrganisationUserByOrganisationIdAndEmailSpec(organisationUser.OrganisationId,
        organisationUser.Email);
    var existingUser = await _organisationUserRepository.GetBySpecAsync(spec, cancellationToken);

    if (existingUser != null)
    {
      return Result.Error();
    }

    var createdUser =
      await _organisationUserRepository.AddAsync(organisationUser, cancellationToken);
    await _organisationUserRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(createdUser);
  }

  public async Task<Result<List<OrganisationUser>>> ListByOrganisationRouteName(string
    organisationRouteName, CancellationToken cancellationToken)
  {
    var organisation = await
      _organisationService.GetByRouteName(organisationRouteName, cancellationToken);

    if (!organisation.IsSuccess)
    {
      return Result.NotFound();
    }

    var spec = new OrganisationUserByOrganisationIdSpec(organisation.Value.Id);
    var organisations = await _organisationUserRepository.ListAsync(spec, cancellationToken);

    return Result.Success(organisations.ToList());
  }

  public async Task<Result<OrganisationUser>> GetById(int userId,
    CancellationToken cancellationToken)
  {
    var user = await _organisationUserRepository.GetByIdAsync(userId, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(user);
  }

  public async Task<Result> Delete(int organisationUserId, CancellationToken cancellationToken)
  {
    var spec = new OrganisationUserByIdSpec(organisationUserId);
    var existingUser = await _organisationUserRepository.GetBySpecAsync(spec, cancellationToken);

    if (existingUser == null)
    {
      return Result.NotFound();
    }

    existingUser.Deactivate();

    await _organisationUserRepository.UpdateAsync(existingUser, cancellationToken);
    await _organisationUserRepository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}

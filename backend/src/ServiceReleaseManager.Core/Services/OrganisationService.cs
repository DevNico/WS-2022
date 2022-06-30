using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class OrganisationService : IOrganisationService
{
  private readonly IRepository<Organisation> _organisationRepository;

  public OrganisationService(IRepository<Organisation> organisationRepository)
  {
    _organisationRepository = organisationRepository;
  }

  public async Task<Result<Organisation>> GetById(int id, CancellationToken cancellationToken)
  {
    var spec = new OrganisationByIdSpec(id);
    var organisation = await _organisationRepository.GetBySpecAsync(spec, cancellationToken);

    return ResultHelper.NullableSuccessNotFound(organisation);
  }

  public async Task<Result<Organisation>> GetByRouteName(
    string routeName,
    CancellationToken cancellationToken
  )
  {
    var spec = new OrganisationByRouteNameSpec(routeName);
    var organisation = await _organisationRepository.GetBySpecAsync(spec, cancellationToken);

    return ResultHelper.NullableSuccessNotFound(organisation);
  }

  public async Task<Result<List<Organisation>>> List(
    bool includeDeactivated,
    CancellationToken cancellationToken
  )
  {
    var spec = new OrganisationsSearchSpec(includeDeactivated);
    var organisations = await _organisationRepository.ListAsync(spec, cancellationToken);
    return Result.Success(organisations);
  }

  public async Task<Result<List<Organisation>>> ListByUserEmail(
    string userEmail,
    CancellationToken cancellationToken
  )
  {
    var spec = new OrganisationsByUserEmailSpec(userEmail.ToLower());
    var organisations = await _organisationRepository.ListAsync(spec, cancellationToken);

    return Result.Success(organisations);
  }

  public async Task<Result<Organisation>> Create(
    string routeName,
    CancellationToken cancellationToken
  )
  {
    var spec = new OrganisationByRouteNameSpec(routeName);
    var org = await _organisationRepository.GetBySpecAsync(spec, cancellationToken);
    if (org != null)
    {
      return Result.Error();
    }

    var newOrganisation = new Organisation(routeName);
    var createdOrganisation =
      await _organisationRepository.AddAsync(newOrganisation, cancellationToken);

    newOrganisation.Roles = OrganisationRole.GetDefaultRoles(newOrganisation.Id);
    await _organisationRepository.UpdateAsync(newOrganisation, cancellationToken);
    await _organisationRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(createdOrganisation);
  }

  public async Task<Result> Update(Organisation organisation, CancellationToken cancellationToken)
  {
    await _organisationRepository.UpdateAsync(organisation, cancellationToken);
    await _organisationRepository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }

  public async Task<Result> Delete(
    string routeName,
    CancellationToken cancellationToken
  )
  {
    var spec = new OrganisationByRouteNameSpec(routeName);
    var org = await _organisationRepository.GetBySpecAsync(spec, cancellationToken);
    if (org == null)
    {
      return Result.NotFound();
    }

    org.Deactivate();
    await _organisationRepository.UpdateAsync(org, cancellationToken);
    await _organisationRepository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }
}

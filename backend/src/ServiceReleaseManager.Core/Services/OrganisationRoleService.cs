using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class OrganisationRoleService : IOrganisationRoleService
{
  public OrganisationRoleService(IRepository<Organisation> organisationRepository)
  {
    _organisationRepository = organisationRepository;
  }

  private readonly IRepository<Organisation> _organisationRepository;

  public async Task<Result<List<OrganisationRole>>> ListByOrganisationRouteName(
    string organisationRouteName,
    CancellationToken cancellationToken)
  {
    var spec = new OrganisationRolesByOrganisationRouteNameSpec(organisationRouteName);
    var roles =
      await _organisationRepository.GetBySpecAsync<IEnumerable<OrganisationRole>>(spec,
        cancellationToken);

    return Result.Success((roles ?? new List<OrganisationRole>()).ToList());
  }

  public Task<Result<OrganisationRole>> Create(OrganisationRole role,
    CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }

  public Task<Result> Delete(int organisationRoleId, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}

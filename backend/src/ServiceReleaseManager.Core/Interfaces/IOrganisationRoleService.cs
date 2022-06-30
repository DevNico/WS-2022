using Ardalis.Result;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IOrganisationRoleService
{
  public Task<Result<List<OrganisationRole>>> ListByOrganisationRouteName(
    string organisationRouteName,
    CancellationToken cancellationToken
  );

  public Task<Result<OrganisationRole>> Create(
    OrganisationRole role,
    CancellationToken cancellationToken
  );

  public Task<Result> Delete(int organisationRoleId, CancellationToken cancellationToken);
}

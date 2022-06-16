using Ardalis.Result;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IOrganisationUserService
{
  public Task<Result<OrganisationUser>> Create(OrganisationUser organisationUser,
    CancellationToken cancellationToken);

  public Task<Result<OrganisationUser>> GetById(int userId,
    CancellationToken cancellationToken);

  public Task<Result<List<OrganisationUser>>> ListByOrganisationRouteName(string
    organisationRouteName, CancellationToken cancellationToken);

  public Task<Result> Delete(int organisationUserId, CancellationToken cancellationToken);
}

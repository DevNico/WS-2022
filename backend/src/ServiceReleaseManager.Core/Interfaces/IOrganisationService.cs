using Ardalis.Result;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IOrganisationService
{
  public Task<Result<Organisation>> GetById(int id, CancellationToken cancellationToken);

  public Task<Result<Organisation>> GetByRouteName(string routeName,
    CancellationToken cancellationToken);

  public Task<Result<List<Organisation>>> List(bool includeDeactivated,
    CancellationToken cancellationToken);

  public Task<Result<Organisation>> Create(string routeName, CancellationToken cancellationToken);
  
  public Task<Result> Update(Organisation organisation, CancellationToken cancellationToken);

  public Task<Result> Delete(string routeName, CancellationToken cancellationToken);
}

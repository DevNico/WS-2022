using System.Security.Claims;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public interface IServiceManagerAuthorizationService
{
  Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken);

  Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal,
    ServiceAuthorizationRequirement requirement, CancellationToken cancellationToken);

}

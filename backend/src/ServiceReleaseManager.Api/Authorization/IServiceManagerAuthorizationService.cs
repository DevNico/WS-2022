using System.Security.Claims;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public interface IServiceManagerAuthorizationService
{
  Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal, string organisationRouteName,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken);

  Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal, int orhanisationId,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken);

  public Task<bool> EvaluateOrganisationAuthorizationServiceId(ClaimsPrincipal claimsPrincipal, int serviceId,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken);

  Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal, int serviceId,
    ServiceAuthorizationRequirement requirement, CancellationToken cancellationToken);

}

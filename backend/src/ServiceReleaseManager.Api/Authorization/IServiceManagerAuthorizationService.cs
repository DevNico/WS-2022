using System.Security.Claims;

namespace ServiceReleaseManager.Api.Authorization;

public interface IServiceManagerAuthorizationService
{
  public Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal, string organisationRouteName,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken);

  public Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal, int organisationId,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken);

  public Task<bool> EvaluateOrganisationAuthorizationServiceId(ClaimsPrincipal claimsPrincipal, int serviceId,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken);

  public Task<bool> EvaluateOrganisationAuthorizationServiceRouteName(ClaimsPrincipal claimsPrincipal, string serviceRouteName,
   OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken);

  public Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal, int serviceId,
    ServiceAuthorizationRequirement requirement, CancellationToken cancellationToken);

  public Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal, string serviceRouteName,
    ServiceAuthorizationRequirement requirement, CancellationToken cancellationToken);

}

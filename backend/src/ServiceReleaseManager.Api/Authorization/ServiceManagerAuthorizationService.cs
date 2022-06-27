using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public class ServiceManagerAuthorizationService : IServiceManagerAuthorizationService
{
  private readonly IAuthorizationService _authorizationService;
  private readonly IOrganisationService _organisationService;
  private readonly IOrganisationUserService _userService;
  private readonly IServiceUserService _serviceUserService;

  public ServiceManagerAuthorizationService(IAuthorizationService authorizationService, IOrganisationService organisationService,
    IOrganisationUserService userService, IServiceUserService serviceUserService)
  {
    _authorizationService = authorizationService;
    _organisationService = organisationService;
    _userService = userService;
    _serviceUserService = serviceUserService;
  }

  public async Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal, int organisationId,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var user = await getOrganisationUser(claimsPrincipal, organisationId, cancellationToken);
    var result = await _authorizationService.AuthorizeAsync(claimsPrincipal, user?.Role, requirement);
    return result.Succeeded;
  }

  public async Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal, string organisationRouteName,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var organisation = await _organisationService.GetByRouteName(organisationRouteName, cancellationToken);
    if(organisation == null) return false;
    return await EvaluateOrganisationAuthorization(claimsPrincipal, organisation.Id, requirement, cancellationToken);
  }

  public async Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal, int organisationId,
    ServiceAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var organisationUser = await getOrganisationUser(claimsPrincipal, organisationId, cancellationToken);
    if (organisationUser == null || requirement.EvaluationFunction == null) return false;
    var serviceUser = await _serviceUserService.getByOrganisationUserId(organisationUser.Id, cancellationToken);
    return serviceUser != null && requirement.EvaluationFunction.Invoke(serviceUser.ServiceRole);
  }

  public async Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal, string organisationRouteName, ServiceAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var organisation = await _organisationService.GetByRouteName(organisationRouteName, cancellationToken);
    if (organisation == null) return false;
    return await EvaluateServiceAuthorization(claimsPrincipal, organisation.Id, requirement, cancellationToken);
  }

  private async Task<OrganisationUser?> getOrganisationUser(ClaimsPrincipal claimsPrincipal, int organisationId, CancellationToken cancellationToken)
  {
    var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userId == null) return null;

    var users = await _userService.GetUsers(organisationId, cancellationToken);

    return users?.FirstOrDefault();
  }

  private bool isSuperUser(ClaimsPrincipal claimsPrincipal)
  {
    if (claimsPrincipal?.Identity?.Name == null)
    {
      return false;
    }
    return claimsPrincipal.IsInRole("superAdmin");
   
  }
}

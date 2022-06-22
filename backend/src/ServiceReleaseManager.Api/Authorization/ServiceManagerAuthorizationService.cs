using System.Security.Claims;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public class ServiceManagerAuthorizationService : IServiceManagerAuthorizationService
{
  private readonly IOrganisationUserService _userService;
  private readonly IServiceUserService _serviceUserService;

  public ServiceManagerAuthorizationService(IOrganisationUserService userService, IServiceUserService serviceUserService)
  {
    _userService = userService;
    _serviceUserService = serviceUserService;
  }

  public async Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal, OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    var user = await getOrganisationUser(claimsPrincipal, cancellationToken);

    if (user == null || requirement.EvaluationFunction == null) return false;

    return requirement.EvaluationFunction.Invoke(user.Role);
  }

  public async Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal, ServiceAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    var organisationUser = await getOrganisationUser(claimsPrincipal, cancellationToken);

    if (organisationUser == null || requirement.EvaluationFunction == null) return false;

    var serviceUser = await _serviceUserService.getByOrganisationUserId(organisationUser.Id, cancellationToken);

    return serviceUser != null && requirement.EvaluationFunction.Invoke(serviceUser.ServiceRole);
  }

  private async Task<OrganisationUser?> getOrganisationUser(ClaimsPrincipal claimsPrincipal, CancellationToken cancellationToken)
  {
    var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userId == null) return null;

    return await _userService.GetByUserId(userId, cancellationToken);
  }
}

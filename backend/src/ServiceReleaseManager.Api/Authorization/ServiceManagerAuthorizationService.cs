using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public class ServiceManagerAuthorizationService : IServiceManagerAuthorizationService
{
  private readonly IAuthorizationService _authorizationService;
  private readonly IOrganisationService _organisationService;
  private readonly IOrganisationUserService _userService;
  private readonly IServiceUserService _serviceUserService;
  private readonly IServiceService _serviceService;

  public ServiceManagerAuthorizationService(IAuthorizationService authorizationService,
    IOrganisationService organisationService,
    IOrganisationUserService userService, IServiceUserService serviceUserService,
    IServiceService serviceService)
  {
    _authorizationService = authorizationService;
    _organisationService = organisationService;
    _userService = userService;
    _serviceUserService = serviceUserService;
    _serviceService = serviceService;
  }

  public async Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal,
    int organisationId,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var user = await getOrganisationUser(claimsPrincipal, organisationId, cancellationToken);
    var result =
      await _authorizationService.AuthorizeAsync(claimsPrincipal, user?.Role, requirement);
    return result.Succeeded;
  }

  public async Task<bool> EvaluateOrganisationAuthorization(ClaimsPrincipal claimsPrincipal,
    string organisationRouteName,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var organisation =
      await _organisationService.GetByRouteName(organisationRouteName, cancellationToken);
    if (!organisation.IsSuccess) return false;
    return await EvaluateOrganisationAuthorization(claimsPrincipal, organisation.Value.Id,
      requirement, cancellationToken);
  }

  public async Task<bool> EvaluateOrganisationAuthorizationServiceId(
    ClaimsPrincipal claimsPrincipal, int serviceId,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var service = await _serviceService.GetById(serviceId, cancellationToken);
    if (!service.IsSuccess) return false;

    return await EvaluateOrganisationAuthorization(claimsPrincipal, service.Value.OrganisationId,
      requirement, cancellationToken);
  }

  public async Task<bool> EvaluateOrganisationAuthorizationServiceRouteName(
    ClaimsPrincipal claimsPrincipal, string serviceRouteName,
    OrganisationAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var service = await _serviceService.GetByRouteName(serviceRouteName, cancellationToken);
    if (!service.IsSuccess) return false;

    return await EvaluateOrganisationAuthorization(claimsPrincipal, service.Value.OrganisationId,
      requirement, cancellationToken);
  }

  public async Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal,
    int serviceId,
    ServiceAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var service = await _serviceService.GetById(serviceId, cancellationToken);
    if (!service.IsSuccess) return false;
    var organisationUser = await getOrganisationUser(claimsPrincipal, service.Value.OrganisationId,
      cancellationToken);
    if (organisationUser == null || requirement.EvaluationFunction == null) return false;
    var serviceUser = await _serviceUserService.GetOrganisationUserById(organisationUser.Id, service.Value.Id, cancellationToken);
    if (!serviceUser.IsSuccess) return false;
    var result = await _authorizationService.AuthorizeAsync(claimsPrincipal,
      serviceUser.Value.ServiceRole, requirement);
    return result.Succeeded;
  }

  public async Task<bool> EvaluateServiceAuthorization(ClaimsPrincipal claimsPrincipal,
    string serviceRouteName, ServiceAuthorizationRequirement requirement,
    CancellationToken cancellationToken)
  {
    if (isSuperUser(claimsPrincipal)) return true;

    var service = await _serviceService.GetByRouteName(serviceRouteName, cancellationToken);
    if (!service.IsSuccess) return false;

    return await EvaluateServiceAuthorization(claimsPrincipal, service.Value.Id, requirement,
      cancellationToken);
  }

  private async Task<OrganisationUser?> getOrganisationUser(ClaimsPrincipal claimsPrincipal,
    int organisationId, CancellationToken cancellationToken)
  {
    var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userId == null) return null;

    var users = await _userService.GetUsers(organisationId, cancellationToken);

    return users?.FirstOrDefault(u => u.UserId == userId);
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

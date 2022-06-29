using System.Security.Claims;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.Services;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListServicesMe : EndpointBase
  .WithRequest<ListServicesForMeRequest>
  .WithActionResult<List<ServiceRecord>>
{
  private readonly IOrganisationUserService _organisationUserService;
  private readonly IOrganisationService _organisationService;
  private readonly IServiceService _service;

  public ListServicesMe(IServiceService service, IOrganisationUserService organisationUserService, IOrganisationService organisationService)
  {
    _service = service;
    _organisationUserService = organisationUserService;
    _organisationService = organisationService;
  }

  [HttpGet(ListServicesForMeRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Get the services this user can access",
    OperationId = "Organisation.ServicesMe",
    Tags = new[] { "Organisation" }
  )]
  [SwaggerResponse(200, "The operation was successful", typeof(List<ServiceRecord>))]
  public override async Task<ActionResult<List<ServiceRecord>>> HandleAsync(
    [FromRoute] ListServicesForMeRequest request,
    CancellationToken cancellationToken = new())
  {
    var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
    if (email == null) return Unauthorized();

    var organisation = await _organisationService.GetByRouteName(request.OrganisationRouteName, cancellationToken);
    if (!organisation.IsSuccess)
    {
      return Unauthorized();
    }

    var user = organisation.Value.Users.Find(u => u.Email == email);
    if (user == null)
    {
      return Unauthorized();
    }

    var result = await _service.GetByOrganisationUserId(user.Id, cancellationToken);
    return this.ToActionResult(result.MapValue(s => s.ConvertAll(ServiceRecord.FromEntity)));
  }
}

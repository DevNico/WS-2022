using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Api.Endpoints.ServiceUsers;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListUsers : EndpointBase
  .WithRequest<ListUsersByServiceId>
  .WithActionResult<List<ServiceUserRecord>>
{
  private readonly IServiceUserService _service;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public ListUsers(IServiceUserService service,
    IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpGet(ListUsersByServiceId.Route)]
  [SwaggerOperation(
    Summary = "List all users for a service",
    OperationId = "Services.ListUsers",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "The users for the service", typeof(List<ServiceUserRecord>))]
  [SwaggerResponse(404, "The service was not found")]
  public override async Task<ActionResult<List<ServiceUserRecord>>> HandleAsync(
    ListUsersByServiceId request,
    CancellationToken cancellationToken = new())
  {
    if (!await _authorizationService.EvaluateOrganisationAuthorizationServiceId(User,
          request.ServiceId,
          ServiceUserOperations.ServiceUser_List, cancellationToken))
    {
      return Unauthorized();
    }

    var users = await _service.GetByServiceId(request.ServiceId, cancellationToken);
    return this.ToActionResult(users.MapValue(u => u.ConvertAll(ServiceUserRecord.FromEntity)));
  }
}

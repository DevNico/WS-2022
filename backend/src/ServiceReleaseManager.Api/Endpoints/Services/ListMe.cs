using System.Security.Claims;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListMe : EndpointBase
  .WithoutRequest
  .WithActionResult<List<ServiceRecord>>
{
  private readonly IOrganisationUserService _organisationUserService;
  private readonly IServiceService _service;

  public ListMe(IServiceService service, IOrganisationUserService organisationUserService)
  {
    _service = service;
    _organisationUserService = organisationUserService;
  }

  [HttpGet("me")]
  [Authorize]
  [SwaggerOperation(
    Summary = "Get the services this user can access",
    OperationId = "Service.Me",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "The operation was successful", typeof(List<ServiceRecord>))]
  public override async Task<ActionResult<List<ServiceRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
    if (email == null) return Unauthorized();

    var users = await _organisationUserService.GetByEmail(email, cancellationToken);
    if (!users.IsSuccess)
    {
      return NotFound();
    }

    var userIds = users.Value.ConvertAll(u => u.Id);
    var result = await _service.GetByOrganisationUserIds(userIds, cancellationToken);

    return this.ToActionResult(result.MapValue(s => s.ConvertAll(ServiceRecord.FromEntity)));
  }
}

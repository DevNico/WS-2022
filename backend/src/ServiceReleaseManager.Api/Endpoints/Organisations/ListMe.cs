using System.Security.Claims;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.Locales;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListMe : EndpointBase.WithoutRequest.WithActionResult<List<OrganisationRecord>>
{
  private readonly IOrganisationService _organisationService;

  public ListMe(IOrganisationService organisationService)
  {
    _organisationService = organisationService;
  }

  [HttpGet("me")]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Get all Organisations the current user is a member of",
    OperationId = "Organisations.Me",
    Tags = new[] { "Organisation" })
  ]
  [SwaggerResponse(StatusCodes.Status200OK, "Organisations found", typeof(List<OrganisationRecord>))]
  public override async Task<ActionResult<List<OrganisationRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
    if (email == null) return StatusCode(500);

    var result = await _organisationService.ListByUserEmail(email, cancellationToken);

    return this.ToActionResult(result.MapValue(organisations =>
      organisations.ConvertAll(OrganisationRecord.FromEntity)));
  }
}

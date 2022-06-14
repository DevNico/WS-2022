using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class Create : EndpointBase.WithRequest<CreateOrganisationRequest>.WithActionResult<
  OrganisationRecord>
{
  private readonly IOrganisationService _organisationService;

  public Create(IOrganisationService organisationService)
  {
    _organisationService = organisationService;
  }

  [HttpPost]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Creates a new Organisation",
    Description = "Creates a new Organisation",
    OperationId = "Organisation.Create",
    Tags = new[] { "Organisation" })
  ]
  public override async Task<ActionResult<OrganisationRecord>> HandleAsync(
    CreateOrganisationRequest request,
    CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.Name))
    {
      return BadRequest();
    }

    var result = await _organisationService.Create(request.Name, cancellationToken);
    return this.ToActionResult(result.MapValue(OrganisationRecord.FromEntity));
  }
}

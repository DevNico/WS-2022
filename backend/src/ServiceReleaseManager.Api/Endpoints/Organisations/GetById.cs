using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class GetById : EndpointBase.WithRequest<GetOrganisationByIdRequest>.WithActionResult<
  OrganisationRecord>
{
  private readonly IOrganisationService _organisationService;

  public GetById(IOrganisationService organisationService)
  {
    _organisationService = organisationService;
  }

  [HttpGet(GetOrganisationByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a single Organisation",
    Description = "Gets a single Organisation by its route name",
    OperationId = "Organisations.GetById",
    Tags = new[] { "Organisation" })
  ]
  [SwaggerResponse(StatusCodes.Status200OK, "Organisation found", typeof(OrganisationRecord))]
  public override async Task<ActionResult<OrganisationRecord>> HandleAsync(
    [FromRoute] GetOrganisationByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var result =
      await _organisationService.GetById(request.OrganisationId, cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationRecord.FromEntity));
  }
}

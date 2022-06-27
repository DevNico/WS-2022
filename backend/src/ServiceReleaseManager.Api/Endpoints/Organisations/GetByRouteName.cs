using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class GetByRouteName : EndpointBase.WithRequest<GetOrganisationByRouteNameRequest>.
  WithActionResult<
    OrganisationRecord>
{
  private readonly IOrganisationService _organisationService;

  public GetByRouteName(IOrganisationService organisationService)
  {
    _organisationService = organisationService;
  }

  [HttpGet(GetOrganisationByRouteNameRequest.Route)]
  [SwaggerOperation(
    Summary = "Gets a single Organisation",
    Description = "Gets a single Organisation by its route name",
    OperationId = "Organisations.GetByRouteName",
    Tags = new[] { "Organisation" })
  ]
  [SwaggerResponse(StatusCodes.Status200OK, "Organisation found", typeof(OrganisationRecord))]
  public override async Task<ActionResult<OrganisationRecord>> HandleAsync(
    [FromRoute] GetOrganisationByRouteNameRequest request,
    CancellationToken cancellationToken = new())
  {
    var result =
      await _organisationService.GetByRouteNameRequest(request.OrganisationRouteName,
        cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationRecord.FromEntity));
  }
}

using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class GetByRouteName : EndpointBaseAsync.WithRequest<GetOrganisationByRouteNameRequest>.
  WithActionResult<
    OrganisationRecord>
{
  public GetByRouteName(IOrganisationService organisationService)
  {
    _organisationService = organisationService;
  }

  private readonly IOrganisationService _organisationService;

  [HttpGet(GetOrganisationByRouteNameRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single Organisation",
    Description = "Gets a single Organisation by its route name",
    OperationId = "Organisations.GetByName",
    Tags = new[] { "Organisation" })
  ]
  public override async Task<ActionResult<OrganisationRecord>> HandleAsync(
    [FromRoute] GetOrganisationByRouteNameRequest request,
    CancellationToken cancellationToken = new())
  {
    var result =
      await _organisationService.GetByRouteName(request.OrganisationRouteName,
        cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationRecord.FromEntity));
  }
}

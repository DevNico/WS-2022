using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class List : EndpointBaseAsync.WithRequest<ListOrganisationsRequest>.WithActionResult<
  List<OrganisationRecord>>
{
  public List(IOrganisationService organisationService)
  {
    _organisationService = organisationService;
  }

  private readonly IOrganisationService _organisationService;

  [HttpGet(ListOrganisationsRequest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Gets a list of all Organisations",
    OperationId = "Organisations.List",
    Tags = new[] { "Organisation" })
  ]
  public override async Task<ActionResult<List<OrganisationRecord>>> HandleAsync(
    [FromQuery] ListOrganisationsRequest request,
    CancellationToken cancellationToken = new())
  {
    var result = await _organisationService.List(request.IncludeDeactivated, cancellationToken);

    return this.ToActionResult(result.MapValue(organisations =>
      organisations.ConvertAll(OrganisationRecord.FromEntity)));
  }
}

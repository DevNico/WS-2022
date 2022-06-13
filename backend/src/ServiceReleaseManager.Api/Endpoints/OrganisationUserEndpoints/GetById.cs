using Ardalis.ApiEndpoints;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class GetById : EndpointBaseAsync.WithRequest<GetOrganisationUserByIdRequest>.WithActionResult<
    OrganisationUserRecord>
{
  public GetById(IOrganisationUserService organisationUserService)
  {
    _organisationUserService = organisationUserService;
  }

  private readonly IOrganisationUserService _organisationUserService;

  [HttpGet(GetOrganisationUserByIdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single OrganisationUser",
    Description = "Gets a single OrganisationUser by UserId",
    OperationId = "Organisations.GetByUserId",
    Tags = new[] { "OrganisationUser" })
  ]
  public override async Task<ActionResult<OrganisationUserRecord>> HandleAsync(
    [FromRoute] GetOrganisationUserByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var result =
      await _organisationUserService.GetById(request.OrganisationUserId, cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationUserRecord.FromEntity));
  }
}

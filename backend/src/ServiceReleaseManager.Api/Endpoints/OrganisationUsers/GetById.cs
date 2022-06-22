using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Organisation;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class GetById : EndpointBase.WithRequest<GetOrganisationUserByIdRequest>.WithActionResult<
  OrganisationUserRecord>
{
  private readonly IOrganisationUserService _organisationUserService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public GetById(IOrganisationUserService organisationUserService, IServiceManagerAuthorizationService authorizationService)
  {
    _organisationUserService = organisationUserService;
    _authorizationService = authorizationService;
  }

  [HttpGet(GetOrganisationUserByIdRequest.Route)]
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
    if (!await _authorizationService.EvaluateOrganisationAuthorization(User,
      OrganisationUserOperations.OrganisationUser_Read, cancellationToken))
    {
      return NotFound();
    }

    var result =
      await _organisationUserService.GetById(request.OrganisationUserId, cancellationToken);

    return this.ToActionResult(result.MapValue(OrganisationUserRecord.FromEntity));
  }
}

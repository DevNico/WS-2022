using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public class Create : EndpointBase.WithRequest<CreateOrganisationUserRequest>.WithActionResult<
  OrganisationUserRecord>
{
  private readonly IOrganisationUserService _organisationUserService;

  public Create(IOrganisationUserService organisationUserService)
  {
    _organisationUserService = organisationUserService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Creates a new OrganisationUser",
    Description = "Creates a new OrganisationUser",
    OperationId = "OrganisationUser.Create",
    Tags = new[] { "OrganisationUser" })
  ]
  public override async Task<ActionResult<OrganisationUserRecord>> HandleAsync(
    [FromBody] CreateOrganisationUserRequest request,
    CancellationToken cancellationToken = new())
  {
    var newUser = new OrganisationUser(
      // TODO: Get userId from AuthContext
      "",
      request.Email,
      request.FirstName,
      request.LastName,
      request.RoleId
    );
    var result = await _organisationUserService.Create(newUser, cancellationToken);
    return this.ToActionResult(result.MapValue(OrganisationUserRecord.FromEntity));
  }
}

using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteOrganisationUserRequest>.WithoutResult
{
  private readonly IRepository<Organisation> _repository;

  public Delete(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpDelete(RouteHelper.OrganizationUsers_Delete)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Deletes a OrganisationUser",
    Description = "Deletes a OrganisationUser",
    OperationId = "OrganisationUser.Delete",
    Tags = new[] { "OrganisationUserEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteOrganisationUserRequest request,
    CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request?.OrganisationName))
    {
      return BadRequest();
    }

    var orgSpec = new OrganisationByNameSpec(request.OrganisationName);
    var org = await _repository.GetBySpecAsync(orgSpec, cancellationToken);
    if (org == null)
    {
      return Unauthorized();
    }

    var spec = new OrganisationUserByIdSpec(request.OrgUserId);
    var organisationUserToDelete = spec.Evaluate(org.Users).FirstOrDefault();
    if (organisationUserToDelete == null)
    {
      return NotFound();
    }


    organisationUserToDelete.Deactivate();
    await _repository.UpdateAsync(org);
    await _repository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}

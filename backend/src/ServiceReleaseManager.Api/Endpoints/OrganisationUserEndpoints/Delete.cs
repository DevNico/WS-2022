using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteOrganisationUserRequest>.WithoutResult
{
  private readonly IRepository<OrganisationUser> _repository;

  public Delete(IRepository<OrganisationUser> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteOrganisationRequest.Route)]
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
    if(request?.UserId == null)
    {
      return BadRequest();
    }

    var spec = new OrganisationUserByUserIdSpec(request.UserId);
    var organisationUserToDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (organisationUserToDelete == null)
    {
      return NotFound();
    }

    organisationUserToDelete.Deactivate();
    await _repository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}

using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteOrganisationRoleRequest>.WithoutResult
{
  private readonly IRepository<OrganisationRole> _repository;

  public Delete(IRepository<OrganisationRole> repository)
  {
    _repository = repository;
  }

  [HttpDelete(DeleteOrganisationRequest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Deletes a Organisation",
    Description = "Deletes a Organisation",
    OperationId = "Organisations.Delete",
    Tags = new[] { "OrganisationEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteOrganisationRoleRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request?.Name == null)
    {
      return BadRequest();
    }

    var spec = new OrganisationRoleByNameSpec(request.Name);
    var organisationRoleToDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (organisationRoleToDelete == null)
    {
      return NotFound();
    }
    await _repository.DeleteAsync(organisationRoleToDelete, cancellationToken);

    return NoContent();
  }
}

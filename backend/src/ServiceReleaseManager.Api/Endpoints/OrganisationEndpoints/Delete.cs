using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteOrganisationRequest>.WithoutResult
{
  private readonly IRepository<Organisation> _repository;

  public Delete(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpDelete(RouteHelper.Organizations_Delete)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Deletes a Organisation",
    Description = "Deletes a Organisation",
    OperationId = "Organisations.Delete",
    Tags = new[] { "OrganisationEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteOrganisationRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new OrganisationByNameSpec(request.OrganisationName);
    var organisationToDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (organisationToDelete == null)
    {
      return NotFound();
    }

    organisationToDelete.Deactivate();
    await _repository.UpdateAsync(organisationToDelete);
    await _repository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}

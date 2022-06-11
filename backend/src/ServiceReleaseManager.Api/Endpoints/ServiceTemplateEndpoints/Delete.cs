using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplateEndpoints;

public class Delete : EndpointBaseAsync
  .WithRequest<DeleteServiceTemplate>
  .WithoutResult
{
  private readonly IRepository<ServiceTemplate> _repository;

  public Delete(IRepository<ServiceTemplate> repository)
  {
    _repository = repository;
  }

  [Authorize]
  [HttpDelete(DeleteServiceTemplate.Route)]
  [SwaggerOperation(
    Description = "Deletes a service template by its id",
    Summary = "Deletes a service template",
    OperationId = "ServiceTemplate.Delete",
    Tags = new[] { "ServiceTemplateEndpoints" }
  )]
  [SwaggerResponse(201, "The service template was deleted")]
  [SwaggerResponse(404, "A service template with the given name was not found")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteServiceTemplate request,
    CancellationToken cancellationToken = new())
  {
    var spec = new ServiceTemplateByIdSpec(request.ServiceTemplateId);
    var toDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (toDelete == null)
    {
      return NotFound();
    }

    toDelete.Deactivate();
    await _repository.UpdateAsync(toDelete, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}

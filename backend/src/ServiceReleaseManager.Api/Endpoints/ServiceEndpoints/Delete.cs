using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class Delete : AuthorizedEndpointBase
  .WithRequest<DeleteServiceRequest>
  .WithoutResult
{
  private readonly IRepository<Service> _repository;

  public Delete(IRepository<Service> repository)
  {
    _repository = repository;
  }
  
  [HttpDelete(DeleteServiceRequest.Route)]
  [SwaggerOperation(
    Description = "Deletes a service",
    Summary = "Deletes a service by its id",
    OperationId = "Service.Delete",
    Tags = new[] { "ServiceEndpoints" }
  )]
  [SwaggerResponse(204, "The service was deleted")]
  [SwaggerResponse(404, "A service with the given id was not found")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteServiceRequest request, 
    CancellationToken cancellationToken = new())
  {
    var spec = new ServiceByIdSpec(request.ServiceId);
    var service = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (service == null)
    {
      return NotFound();
    }
    
    service.Deactivate();

    await _repository.UpdateAsync(service, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}

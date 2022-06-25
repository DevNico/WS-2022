using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class Delete : EndpointBase
  .WithRequest<DeleteServiceRequest>
  .WithoutResult
{
  private readonly IServiceService _service;

  public Delete(IServiceService service)
  {
    _service = service;
  }

  [HttpDelete(DeleteServiceRequest.Route)]
  [SwaggerOperation(
    Description = "Deletes a service",
    Summary = "Deletes a service by its id",
    OperationId = "Service.Delete",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "The service was deleted")]
  [SwaggerResponse(404, "A service with the given id was not found")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteServiceRequest request, 
    CancellationToken cancellationToken = new())
  {
    var result = await _service.Deactivate(request.ServiceId, cancellationToken);
    return this.ToActionResult(result);
  }
}

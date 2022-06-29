using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTriggers;

public class Delete : EndpointBase
  .WithRequest<DeleteReleaseTriggersRequest>
  .WithoutResult
{
  private readonly IReleaseTriggerService _service;

  public Delete(IReleaseTriggerService service)
  {
    _service = service;
  }

  [HttpDelete(DeleteReleaseTriggersRequest.Route)]
  [SwaggerOperation(
    Description = "Deletes a release trigger",
    Summary = "Deletes a release trigger by its id",
    OperationId = "ReleaseTrigger.Delete",
    Tags = new[] { "ReleaseTrigger" }
  )]
  [SwaggerResponse(200, "The release trigger was deleted")]
  [SwaggerResponse(404, "A release trigger with the given id was not found")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteReleaseTriggersRequest request,
    CancellationToken cancellationToken = new())
  {
    var result = await _service.Delete(request.ReleaseTriggerId, cancellationToken);
    return this.ToActionResult(result);
  }
}

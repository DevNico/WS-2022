using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTriggers;

public class GetById : EndpointBase
  .WithRequest<GetReleaseTriggerByIdRequest>
  .WithActionResult<ReleaseTriggerRecord>
{
  private readonly IReleaseTriggerService _service;

  public GetById(IReleaseTriggerService service)
  {
    _service = service;
  }

  [HttpGet(GetReleaseTriggerByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a release trigger",
    Description = "Get a release trigger by its id",
    OperationId = "ReleaseTrigger.GetById",
    Tags = new[] { "ReleaseTrigger" }
  )]
  [SwaggerResponse(200, "The release trigger was found", typeof(ReleaseTriggerRecord))]
  [SwaggerResponse(404, "The release trigger was not found")]
  public override async Task<ActionResult<ReleaseTriggerRecord>> HandleAsync(
    [FromRoute] GetReleaseTriggerByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var trigger = await _service.GetById(request.ReleaseTriggerId, cancellationToken);
    return this.ToActionResult(trigger.MapValue(ReleaseTriggerRecord.FromEntity));
  }
}

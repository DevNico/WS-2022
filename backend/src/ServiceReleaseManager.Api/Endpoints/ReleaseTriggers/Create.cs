using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTriggers;

public class Create : EndpointBase
  .WithRequest<CreateReleaseTriggerRequest>
  .WithActionResult<ReleaseTriggerRecord>
{
  private readonly IReleaseTriggerService _service;
  private readonly IServiceService _serviceService;

  public Create(IReleaseTriggerService service, IServiceService serviceService)
  {
    _service = service;
    _serviceService = serviceService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Create a release trigger",
    Description = "Create a release trigger",
    OperationId = "ReleaseTrigger.Create",
    Tags = new[] { "ReleaseTrigger" }
  )]
  [SwaggerResponse(200, "The release trigger was created", typeof(ReleaseTriggerRecord))]
  [SwaggerResponse(400, "A parameter was null or invalid", typeof(ErrorResponse))]
  public override async Task<ActionResult<ReleaseTriggerRecord>> HandleAsync(
    [FromBody] CreateReleaseTriggerRequest request,
    CancellationToken cancellationToken = new())
  {
    var service = await _serviceService.GetById(request.ServiceId, cancellationToken);
    if (!service.IsSuccess)
    {
      return BadRequest(
        new ErrorResponse($"A service with the id {request.ServiceId} does not exist."));
    }

    var trigger = new ReleaseTrigger(request.Name, request.Event, request.Url, service.Value);
    var created = await _service.Create(trigger, cancellationToken);
    return this.ToActionResult(created.MapValue(ReleaseTriggerRecord.FromEntity));
  }
}

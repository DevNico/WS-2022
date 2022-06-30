using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.ReleaseTriggers;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListReleaseTriggers : EndpointBase.WithRequest<ListReleaseTriggersRequest>.
  WithActionResult<List<ReleaseTriggerRecord>>
{
  private readonly IRepository<ReleaseTrigger> _repository;
  private readonly IServiceService _serviceService;

  public ListReleaseTriggers(IRepository<ReleaseTrigger> repository, IServiceService serviceService)
  {
    _repository = repository;
    _serviceService = serviceService;
  }

  [HttpGet(ListReleaseTriggersRequest.Route)]
  [SwaggerOperation(
    Summary = "List all release triggers",
    Description = "List all release triggers",
    OperationId = "Service.ListReleaseTriggers",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "Success", typeof(List<ReleaseTriggerRecord>))]
  [SwaggerResponse(400, "The service was not found")]
  public override async Task<ActionResult<List<ReleaseTriggerRecord>>> HandleAsync(
    [FromRoute] ListReleaseTriggersRequest request,
    CancellationToken cancellationToken = new()
  )
  {
    var service = await _serviceService.GetById(request.ServiceId, cancellationToken);
    if (!service.IsSuccess)
    {
      return BadRequest(
        new ErrorResponse($"A service with the id {request.ServiceId} does not exist.")
      );
    }

    var spec = new ReleaseTriggersByServiceSearchSpec(request.ServiceId);
    var services = await _repository.ListAsync(spec, cancellationToken);

    return Ok(services.ConvertAll(ReleaseTriggerRecord.FromEntity));
  }
}

using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class List : EndpointBase
  .WithRequest<ListReleasesByServiceId>
  .WithActionResult<List<ReleaseRecord>>
{
  private readonly IReleaseService _service;

  public List(IReleaseService service)
  {
    _service = service;
  }

  [HttpGet(ListReleasesByServiceId.Route)]
  [SwaggerOperation(
    Summary = "Gets a list of all Releases",
    OperationId = "Services.ListReleases",
    Tags = new[] { "Service" })
  ]
  [SwaggerResponse(200, "List of Releases", typeof(List<ReleaseRecord>))]
  [SwaggerResponse(404, "Service id not found")]
  public override async Task<ActionResult<List<ReleaseRecord>>> HandleAsync(
    [FromRoute] ListReleasesByServiceId request,
    CancellationToken cancellationToken = new())
  {
    var releases = await _service.GetByServiceId(request.ServiceId, cancellationToken);
    return this.ToActionResult(releases.MapValue(r => r.ConvertAll(ReleaseRecord.FromEntity)));
  }
}

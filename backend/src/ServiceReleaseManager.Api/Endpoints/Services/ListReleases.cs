using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.Releases;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class List : EndpointBaseAsync.WithRequest<ListReleasesByServiceId>.WithActionResult<
  List<ReleaseRecord>>
{
  private readonly IRepository<Release> _repository;

  public List(IRepository<Release> repository)
  {
    _repository = repository;
  }

  [HttpGet(ListReleasesByServiceId.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a list of all Releases",
    OperationId = "Releases.List",
    Tags = new[] { "Release Endpoints" })
  ]
  public override async Task<ActionResult<List<ReleaseRecord>>> HandleAsync(
    [FromRoute] ListReleasesByServiceId request,
    CancellationToken cancellationToken = new())
  {
    var releases = await _repository.ListAsync(cancellationToken);

    var response = releases
      .Where(r => r.ServiceId == request.ServiceId)
      .Select(ReleaseRecord.FromEntity)
      .ToList();

    return Ok(response);
  }
}

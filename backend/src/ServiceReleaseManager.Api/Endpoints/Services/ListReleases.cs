using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.Releases;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class List : EndpointBaseAsync.WithRequest<ListReleasesByServiceId>.WithActionResult<
  List<ReleaseRecord>>
{
  private readonly IRepository<Release> _releaseRepository;
  private readonly IRepository<Service> _serviceRepository;

  public List(IRepository<Release> releaseRepository, IRepository<Service> serviceRepository)
  {
    _releaseRepository = releaseRepository;
    _serviceRepository = serviceRepository;
  }

  [HttpGet(ListReleasesByServiceId.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a list of all Releases",
    OperationId = "Releases.List",
    Tags = new[] { "Release Endpoints" })
  ]
  [SwaggerResponse(StatusCodes.Status200OK, "List of Releases",
    typeof(List<ReleaseRecord>))]
  public override async Task<ActionResult<List<ReleaseRecord>>> HandleAsync(
    [FromRoute] ListReleasesByServiceId request,
    CancellationToken cancellationToken = new())
  {
    var service = await _serviceRepository.GetByIdAsync(request.ServiceId, cancellationToken);

    // TODO: Service Authorization
    if (service == null)
    {
      return BadRequest("Service id not found");
    }

    var releaseByServiceIdSpec = new ReleaseByServiceIdSpec(request.ServiceId);
    var allReleases = await _releaseRepository.ListAsync(cancellationToken);
    var releases = releaseByServiceIdSpec.Evaluate(allReleases);

    var response = releases
      .Select(ReleaseRecord.FromEntity)
      .ToList();

    return Ok(response);
  }
}

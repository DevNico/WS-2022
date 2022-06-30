using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Api.Endpoints.Releases;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class List : EndpointBase.WithRequest<ListReleasesByServiceId>.WithActionResult<
  List<ReleaseRecord>>
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly IReleaseService _service;

  public List(IReleaseService service, IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpGet(ListReleasesByServiceId.Route)]
  [SwaggerOperation(
      Summary = "Gets a list of all Releases",
      OperationId = "Services.ListReleases",
      Tags = new[] { "Service" }
    )
  ]
  [SwaggerResponse(200, "List of Releases", typeof(List<ReleaseRecord>))]
  [SwaggerResponse(404, "Service id not found")]
  public override async Task<ActionResult<List<ReleaseRecord>>> HandleAsync(
    [FromRoute] ListReleasesByServiceId request,
    CancellationToken cancellationToken = new()
  )
  {
    if (!await _authorizationService.EvaluateServiceAuthorization(
          User,
          request.ServiceId,
          ReleaseOperations.Release_List,
          cancellationToken
        ))
    {
      return Unauthorized();
    }

    var releases = await _service.GetByServiceId(request.ServiceId, cancellationToken);
    return this.ToActionResult(releases.MapValue(r => r.ConvertAll(ReleaseRecord.FromEntity)));
  }
}

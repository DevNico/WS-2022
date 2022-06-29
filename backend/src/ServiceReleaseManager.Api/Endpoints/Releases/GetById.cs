using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public class GetById : EndpointBaseAsync.WithRequest<GetReleaseByIdRequest>.WithActionResult<
  ReleaseRecord>
{
  private readonly IRepository<Release> _repository;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public GetById(IRepository<Release> repository,
    IServiceManagerAuthorizationService authorizationService)
  {
    _repository = repository;
    _authorizationService = authorizationService;
  }

  [HttpGet(GetReleaseByIdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single Release",
    Description = "Gets a single Release by ReleaseId",
    OperationId = "Releases.GetById",
    Tags = new[] { "Release" })
  ]
  public override async Task<ActionResult<ReleaseRecord>> HandleAsync(
    [FromRoute] GetReleaseByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new ReleaseByIdSpec(request.ReleaseId);
    var release = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (release == null)
    {
      return NotFound();
    }

    if (!await _authorizationService.EvaluateServiceAuthorization(User, release.ServiceId,
          ReleaseOperations.Release_Read, cancellationToken))
    {
      return NotFound();
    }

    var response = ReleaseRecord.FromEntity(release);
    return Ok(response);
  }
}

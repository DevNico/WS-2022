using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public class Delete : EndpointBaseAsync.WithRequest<DeleteReleaseReqest>.WithoutResult
{
  private readonly IRepository<Release> _repository;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Delete(IRepository<Release> repository, IServiceManagerAuthorizationService authorizationService)
  {
    _repository = repository;
    _authorizationService = authorizationService;
  }

  [HttpDelete(DeleteReleaseReqest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Deletes a release",
    Description = "Deletes a release",
    OperationId = "Release.Delete",
    Tags = new[] { "Release" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteReleaseReqest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new ReleaseByIdSpec(request.ReleaseId);
    var releaseToDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (releaseToDelete == null)
    {
      return NotFound();
    }

    if (!await _authorizationService.EvaluateServiceAuthorization(User, releaseToDelete.ServiceId,
      ReleaseOperations.Release_Delete, cancellationToken))
    {
      return NotFound();
    }

    await _repository.DeleteAsync(releaseToDelete, cancellationToken);
    return NoContent();
  }
}

using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTargetEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateReleaseTargetRequest>
  .WithActionResult<ReleaseTargetRecord>
{
  private readonly IRepository<ReleaseTarget> _repository;

  public Create(IRepository<ReleaseTarget> repository)
  {
    _repository = repository;
  }

  [HttpPost(CreateReleaseTargetRequest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Creates a new Release Target",
    Description = "Creates a new Release Target",
    OperationId = "ReleaseTarget.Create",
    Tags = new[] { "Release Target Endpoints" })
  ]
  public override async Task<ActionResult<ReleaseTargetRecord>> HandleAsync(
    CreateReleaseTargetRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.Name == null)
    {
      return BadRequest();
    }

    var user = HttpContext.User;

    var newReleaseTarget = new ReleaseTarget(request.Name, request.RequiresApproval);
    var createdReleaseTarget = await _repository.AddAsync(newReleaseTarget, cancellationToken);
    var response = ReleaseTargetRecord.FromEntity(createdReleaseTarget);

    return Ok(response);
  }
}

using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateReleaseRequest>
  .WithActionResult<ReleaseRecord>
{

  private readonly IRepository<Release> _repository;

  public Create(IRepository<Release> repository)
  {
    _repository = repository;
  }


  [HttpPost(CreateReleaseRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Creates a new Release",
    Description = "Creates a new Release",
    OperationId = "Release.Create",
    Tags = new[] { "Release Endpoints" })
  ]
  public override async Task<ActionResult<ReleaseRecord>> HandleAsync(
        CreateReleaseRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.Version == null || request.MetaData == null)
    {
      return BadRequest();
    }

    var user = HttpContext.User;

    var newRelease = new Release(request.Version, request.BuildNumber, request.MetaData);
    var createdRelease = await _repository.AddAsync(newRelease, cancellationToken);
    var response = ReleaseRecord.FromEntity(createdRelease);

    return Ok(response);
  }

}

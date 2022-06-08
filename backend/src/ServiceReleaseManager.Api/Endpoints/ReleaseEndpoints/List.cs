using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
// using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public class List : EndpointBaseAsync.WithoutRequest.WithActionResult<List<ReleaseRecord>>
{
  private readonly IRepository<Release> _repository;

  public List(IRepository<Release> repository)
  {
    _repository = repository;
  }

  [HttpGet("/releases")]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Gets a list of all Releases",
    OperationId = "Releases.List",
    Tags = new[] { "Release Endpoints" })
  ]
  public override async Task<ActionResult<List<ReleaseRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    //var releases = await _repository.ListAsync(cancellationToken);
    //var spec = new ActiveReleasesSearchSpec();
    //var activeOrganisations = spec.Evaluate(releases);

    //var response = activeOrganisations
    //  .Select(ReleaseRecord.FromEntity)
    //  .ToList();

    //return Ok(response);

    return NoContent();
  }
}

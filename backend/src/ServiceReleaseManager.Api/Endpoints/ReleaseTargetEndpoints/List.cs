using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
// using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTargetEndpoints;

public class List : EndpointBaseAsync.WithoutRequest.WithActionResult<List<ReleaseTargetRecord>>
{
  private readonly IRepository<ReleaseTarget> _repository;

  public List(IRepository<ReleaseTarget> repository)
  {
    _repository = repository;
  }

  [HttpGet("/release-targets")]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Gets a list of all Release Targets",
    OperationId = "ReleaseTarget.List",
    Tags = new[] { "Release Target Endpoints" })
  ]
  public override async Task<ActionResult<List<ReleaseTargetRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    //var releaseTargets = await _repository.ListAsync(cancellationToken);
    //var spec = new ActiveReleaseTargetsSearchSpec();
    //var activeOrganisations = spec.Evaluate(releaseTargets);

    //var response = activeOrganisations
    //  .Select(ReleaseTargetRecord.FromEntity)
    //  .ToList();

    //return Ok(response);

    return NoContent();
  }
}

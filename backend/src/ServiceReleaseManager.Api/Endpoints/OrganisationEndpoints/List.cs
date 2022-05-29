using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class List : EndpointBaseAsync.WithoutRequest.WithActionResult<List<OrganisationRecord>>
{
  private readonly IRepository<Organisation> _repository;

  public List(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpGet("/organisations")]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Gets a list of all Organisations",
    OperationId = "Organisations.List",
    Tags = new[] { "OrganisationEndpoints" })
  ]
  public override async Task<ActionResult<List<OrganisationRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    var organisations = await _repository.ListAsync(cancellationToken);
    var spec = new ActiveOrganisationsSearchSpec();
    var activeOrganisations = spec.Evaluate(organisations);

    var response = activeOrganisations
      .Select(OrganisationRecord.FromEntity)
      .ToList();

    return Ok(response);
  }
}

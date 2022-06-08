using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;


namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class List : EndpointBaseAsync.WithoutRequest.WithActionResult<List<OrganisationUserRecord>>
{
  private readonly IRepository<OrganisationUser> _repository;

  public List(IRepository<OrganisationUser> repository)
  {
    _repository = repository;
  }

  [HttpGet("/organisations")]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Gets a list of all OrganisationUsers",
    OperationId = "OrganisationUser.List",
    Tags = new[] { "OrganisationUserEndpoints" })
  ]
  public override async Task<ActionResult<List<OrganisationUserRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    var organisations = await _repository.ListAsync(cancellationToken);
    var spec = new ActiveOrganisationUsersSearchSpec();
    var activeOrganisations = spec.Evaluate(organisations);

    var response = activeOrganisations
      .Select(OrganisationUserRecord.FromEntity)
      .ToList();

    return Ok(response);
  }
}

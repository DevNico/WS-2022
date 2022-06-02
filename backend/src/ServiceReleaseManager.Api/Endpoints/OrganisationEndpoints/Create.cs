using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateOrganisationRequest>
  .WithActionResult<OrganisationRecord>
{
  private readonly IRepository<Organisation> _repository;

  public Create(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpPost(CreateOrganisationRequest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Creates a new Organisation",
    Description = "Creates a new Organisation",
    OperationId = "Organisation.Create",
    Tags = new[] { "OrganisationEndpoints" })
  ]
  public override async Task<ActionResult<OrganisationRecord>> HandleAsync(
    CreateOrganisationRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.Name == null)
    {
      return BadRequest();
    }
    
    var user = HttpContext.User;

    var newOrganisation = new Organisation(request.Name);
    var createdOrganisation = await _repository.AddAsync(newOrganisation, cancellationToken);
    var response = OrganisationRecord.FromEntity(createdOrganisation);

    return Ok(response);
  }
}

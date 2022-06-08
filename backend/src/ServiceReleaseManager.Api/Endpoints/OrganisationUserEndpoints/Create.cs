using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateOrganisationUserRequest>
  .WithActionResult<OrganisationUserRecord>
{
  private readonly IRepository<OrganisationUser> _repository;

  public Create(IRepository<OrganisationUser> repository)
  {
    _repository = repository;
  }

  [HttpPost(CreateOrganisationUserRequest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Creates a new OrganisationUser",
    Description = "Creates a new OrganisationUser",
    OperationId = "OrganisationUser.Create",
    Tags = new[] { "OrganisationUserEndpoints" })
  ]
  public override async Task<ActionResult<OrganisationUserRecord>> HandleAsync(CreateOrganisationUserRequest request, CancellationToken cancellationToken = new())
  {
    if (request.UserId == null || request.Email == null)
    {
      return BadRequest();
    }

    var spec = new OrganisationUserByUserIdSpec(request.UserId);
    var found = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (found != null)
    {
      return Conflict();
    }

    var newOrganisation = new OrganisationUser(request.UserId, request.Email, false, request.FirstName, request.LastName, null);
    var createdOrganisation = await _repository.AddAsync(newOrganisation, cancellationToken);
    var response = OrganisationUserRecord.FromEntity(createdOrganisation);

    return Ok(response);
  }
}

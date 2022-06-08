using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;


namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateOrganisationRoleRequest>
  .WithActionResult<OrganisationRoleRecord>
{
  private readonly IRepository<OrganisationRole> _repository;

  public Create(IRepository<OrganisationRole> repository)
  {
    _repository = repository;
  }

  [HttpPost(CreateOrganisationRoleRequest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Creates a new OrganisationRole",
    Description = "Creates a new OrganisationRole",
    OperationId = "OrganisationRole.Create",
    Tags = new[] { "OrganisationRoleEndpoints" })
  ]
  public async override Task<ActionResult<OrganisationRoleRecord>> HandleAsync(CreateOrganisationRoleRequest request, CancellationToken cancellationToken = new())
  {
    if (request.Name == null)
    {
      return BadRequest();
    }

    var spec = new OrganisationRoleByNameSpec(request.Name);
    var found = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (found != null)
    {
      return Conflict();
    }

    var user = HttpContext.User;

    var newOrganisation = new OrganisationRole(request.Name, request.ServiceRead, request.ServiceWrite, request.ServiceDelete, request.UserRead, request.UserWrite, request.UserDelete);
    var createdOrganisation = await _repository.AddAsync(newOrganisation, cancellationToken);
    var response = OrganisationRoleRecord.FromEntity(createdOrganisation);

    return Ok(response);
  }
}

using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateOrganisationRoleRequest>
  .WithActionResult<OrganisationRoleRecord>
{
  private readonly IRepository<Organisation> _organisationRepository;
  private readonly IRepository<OrganisationRole> _repository;

  public Create(IRepository<Organisation> organisationRepository, IRepository<OrganisationRole> repository)
  {
    _organisationRepository = organisationRepository;
    _repository = repository;
  }

  [HttpPost(RouteHelper.OrganizationRoles_Create)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Creates a new OrganisationRole",
    Description = "Creates a new OrganisationRole",
    OperationId = "OrganisationRole.Create",
    Tags = new[] { "OrganisationRoleEndpoints" })
  ]
  public async override Task<ActionResult<OrganisationRoleRecord>> HandleAsync(CreateOrganisationRoleRequest request, CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.OrganisationName))
    {
      return BadRequest();
    }

    var orgSpec = new OrganisationByNameSpec(request.OrganisationName);
    var org = await _organisationRepository.GetBySpecAsync(orgSpec, cancellationToken);
    if (org == null)
    {
      return Unauthorized();
    }

    var spec = new OrganisationRoleByNameSpec(request.Name);
    var found = spec.Evaluate(org.Roles).FirstOrDefault();
    if (found != null)
    {
      return Conflict();
    }

    var newRole = new OrganisationRole(request.Name, request.ServiceRead, request.ServiceWrite, request.ServiceDelete, request.UserRead, request.UserWrite, request.UserDelete);
    var createdOrganisation = await _repository.AddAsync(newRole, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);
    
    org.Roles.Add(newRole);
    await _organisationRepository.UpdateAsync(org);
    await _organisationRepository.SaveChangesAsync(cancellationToken);
    
    var response = OrganisationRoleRecord.FromEntity(createdOrganisation);
    return Ok(response);
  }
}

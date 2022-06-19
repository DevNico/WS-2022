﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Routes;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteOrganisationRoleRequest>.WithoutResult
{
  private readonly IRepository<Organisation> _repository;

  public Delete(IRepository<Organisation> repository)
  {
    _repository = repository;
  }

  [HttpDelete(RouteHelper.OrganizationRoles_Delete)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Deletes a OrganisationRole",
    Description = "Deletes a OrganisationRole",
    OperationId = "OrganisationRoles.Delete",
    Tags = new[] { "OrganisationRoleEndpoints" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteOrganisationRoleRequest request,
    CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.OrganisationName))
    {
      return BadRequest();
    }

    var orgSpec = new OrganisationByNameSpec(request.OrganisationName);
    var org = await _repository.GetBySpecAsync(orgSpec, cancellationToken);
    if (org == null)
    {
      return BadRequest();
    }

    var spec = new OrganisationRoleByIdSpec(request.RoleId);
    var organisationRoleToDelete = spec.Evaluate(org.Roles).FirstOrDefault();
    if (organisationRoleToDelete == null)
    {
      return NotFound();
    }

    org.Roles.Remove(organisationRoleToDelete);
    await _repository.UpdateAsync(org);
    await _repository.SaveChangesAsync();

    return NoContent();
  }
}
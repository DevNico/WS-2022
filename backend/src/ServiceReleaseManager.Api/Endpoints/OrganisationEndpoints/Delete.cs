﻿using Ardalis.ApiEndpoints;
using Ardalis.GuardClauses;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class Delete : EndpointBaseAsync.WithRequest<DeleteOrganisationRequest>.WithoutResult
{
  public Delete(IOrganisationService organisationService)
  {
    _organisationService = organisationService;
  }

  private readonly IOrganisationService _organisationService;

  [HttpDelete(DeleteOrganisationRequest.Route)]
  [Authorize(Roles = "superAdmin")]
  [SwaggerOperation(
    Summary = "Deletes a Organisation",
    Description = "Deletes a Organisation",
    OperationId = "Organisations.Delete",
    Tags = new[] { "Organisation" })
  ]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteOrganisationRequest request,
    CancellationToken cancellationToken = new())
  {
    Guard.Against.NullOrWhiteSpace(request.OrganisationRouteName);

    var result =
      await _organisationService.Delete(request.OrganisationRouteName,
        cancellationToken);

    return result.IsSuccess ? NoContent() : this.ToActionResult(result);
  }
}

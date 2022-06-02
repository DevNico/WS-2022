﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class GetById : EndpointBaseAsync.WithRequest<GetOrganisationByIdRequest>.WithActionResult<
  OrganisationRecord>
{
  private readonly IAuthorizationService _authorizationService;
  private readonly IRepository<Organisation> _repository;

  public GetById(IRepository<Organisation> repository, IAuthorizationService authorizationService)
  {
    _repository = repository;
    _authorizationService = authorizationService;
  }

  [HttpGet(GetOrganisationByIdRequest.Route)]
  [Authorize]
  [SwaggerOperation(
    Summary = "Gets a single Organisation",
    Description = "Gets a single Organisation by OrganisationId",
    OperationId = "Organisations.GetById",
    Tags = new[] { "OrganisationEndpoints" })
  ]
  public override async Task<ActionResult<OrganisationRecord>> HandleAsync(
    [FromRoute] GetOrganisationByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new OrganisationByIdSpec(request.OrganisationId);
    var organisation = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (organisation == null)
    {
      return NotFound();
    }

    // TODO: Authorize organisation resource

    var response = OrganisationRecord.FromEntity(organisation);
    return Ok(response);
  }
}
﻿using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class Delete : EndpointBase.WithRequest<DeleteServiceTemplate>.WithoutResult
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly IRepository<ServiceTemplate> _repository;

  public Delete(
    IRepository<ServiceTemplate> repository,
    IServiceManagerAuthorizationService authorizationService
  )
  {
    _repository = repository;
    _authorizationService = authorizationService;
  }

  [HttpDelete(DeleteServiceTemplate.Route)]
  [SwaggerOperation(
    Description = "Deletes a service template by its id",
    Summary = "Deletes a service template",
    OperationId = "ServiceTemplate.Delete",
    Tags = new[] { "ServiceTemplate" }
  )]
  [SwaggerResponse(201, "The service template was deleted")]
  [SwaggerResponse(404, "A service template with the given id was not found")]
  public override async Task<ActionResult> HandleAsync(
    [FromRoute] DeleteServiceTemplate request,
    CancellationToken cancellationToken = new()
  )
  {
    var spec = new ServiceTemplateByIdSpec(request.ServiceTemplateId);
    var toDelete = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (toDelete == null)
    {
      return NotFound();
    }

    if (!await _authorizationService.EvaluateOrganisationAuthorization(
          User,
          toDelete.OrganisationId,
          ServiceTemplateOperations.ServiceTemplate_Delete,
          cancellationToken
        ))
    {
      return NotFound();
    }

    toDelete.Deactivate();
    await _repository.UpdateAsync(toDelete, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    return NoContent();
  }
}

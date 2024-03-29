﻿using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class Create : EndpointBase.WithRequest<CreateServiceTemplate>.WithActionResult<
  ServiceTemplateRecord>
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly IMetadataFormatValidator _metadataValidator;
  private readonly IOrganisationService _organisationService;
  private readonly IRepository<ServiceTemplate> _repository;

  public Create(
    IRepository<ServiceTemplate> repository,
    IMetadataFormatValidator metadataValidator,
    IOrganisationService organisationService,
    IServiceManagerAuthorizationService authorizationService
  )
  {
    _repository = repository;
    _metadataValidator = metadataValidator;
    _organisationService = organisationService;
    _authorizationService = authorizationService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Add a new service template",
    Description = "Create a new service template",
    OperationId = "ServiceTemplate.Create",
    Tags = new[] { "ServiceTemplate" }
  )]
  [SwaggerResponse(200, "The service template was created", typeof(ServiceTemplateRecord))]
  [SwaggerResponse(400, "A parameter was null or invalid", typeof(ErrorResponse))]
  [SwaggerResponse(409, "A service template with the same name already exists")]
  public override async Task<ActionResult<ServiceTemplateRecord>> HandleAsync(
    CreateServiceTemplate request,
    CancellationToken cancellationToken = new()
  )
  {
    var organisation =
      await _organisationService.GetById(request.OrganisationId, cancellationToken);

    if (!organisation.IsSuccess)
    {
      return BadRequest(new ErrorResponse("Organisation not found"));
    }

    if (!await _authorizationService.EvaluateOrganisationAuthorization(
          User,
          request.OrganisationId,
          ServiceTemplateOperations.ServiceTemplate_Create,
          cancellationToken
        ))
    {
      return Unauthorized();
    }

    var nameSpec = new ServiceTemplateByNameSpec(request.Name);
    if (await _repository.CountAsync(nameSpec, cancellationToken) > 0)
    {
      return Conflict();
    }

    var staticMetadata = await _metadataValidator.NormalizeMetadata(request.StaticMetadata);
    if (!staticMetadata.IsSuccess)
    {
      return this.ToActionResult(Result.Invalid(staticMetadata.ValidationErrors));
    }

    var localizedMetadata = await _metadataValidator.NormalizeMetadata(request.LocalizedMetadata);
    if (!localizedMetadata.IsSuccess)
    {
      return this.ToActionResult(Result.Invalid(localizedMetadata.ValidationErrors));
    }

    nameSpec = new ServiceTemplateByNameSpec(request.Name.Trim(), false);
    var template = await _repository.GetBySpecAsync(nameSpec, cancellationToken);

    if (template != null)
    {
      template.IsActive = true;
      template.LocalizedMetadata = localizedMetadata;
      template.StaticMetadata = staticMetadata;
      await _repository.UpdateAsync(template, cancellationToken);
    }
    else
    {
      template = new ServiceTemplate(
        request.Name,
        staticMetadata,
        localizedMetadata,
        organisation.Value.Id
      );
      template = await _repository.AddAsync(template, cancellationToken);
    }

    await _repository.SaveChangesAsync(cancellationToken);
    var result = ServiceTemplateRecord.FromEntity(template);
    return Ok(result);
  }
}

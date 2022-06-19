﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class Update : AuthorizedEndpointBase
  .WithRequest<UpdateServiceTemplate>
  .WithActionResult<ServiceTemplateRecord>
{
  private readonly IRepository<ServiceTemplate> _repository;
  private readonly IMetadataFormatValidator _metadataValidator;

  public Update(IRepository<ServiceTemplate> repository, IMetadataFormatValidator metadataValidator)
  {
    _repository = repository;
    _metadataValidator = metadataValidator;
  }

  [HttpPut]
  [Authorize]
  [SwaggerOperation(
    Summary = "Update a service template",
    Description = "Update a service template",
    OperationId = "ServiceTemplate.Update",
    Tags = new[] { "ServiceTemplateEndpoints" }
  )]
  [SwaggerResponse(200, "The service template was updated", typeof(ServiceTemplateRecord))]
  [SwaggerResponse(400, "The request was invalid", typeof(ErrorResponse))]
  [SwaggerResponse(404, "A service template with the specified name was not found")]
  public override async Task<ActionResult<ServiceTemplateRecord>> HandleAsync(
    UpdateServiceTemplate request,
    CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length > 50 || request.LocalizedMetadata == null ||
        request.StaticMetadata == null)
    {
      return BadRequest(new ErrorResponse("A required parameter was null"));
    }

    var nameSpec = new ServiceTemplateByNameSpec(request.Name);
    var serviceTemplate = await _repository.GetBySpecAsync(nameSpec, cancellationToken);
    if (serviceTemplate == null)
    {
      return NotFound();
    }

    string staticMetadata, localizedMetadata;
    try
    {
      staticMetadata = _metadataValidator.NormalizeMetadata(request.StaticMetadata);
      localizedMetadata = _metadataValidator.NormalizeMetadata(request.LocalizedMetadata);
    }
    catch (MetadataFormatValidationError e)
    {
      return BadRequest(ErrorResponse.FromException(e));
    }

    serviceTemplate.LocalizedMetadata = localizedMetadata;
    serviceTemplate.StaticMetadata = staticMetadata;
    await _repository.UpdateAsync(serviceTemplate, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    var result = ServiceTemplateRecord.FromEntity(serviceTemplate);
    return Ok(result);
  }
}
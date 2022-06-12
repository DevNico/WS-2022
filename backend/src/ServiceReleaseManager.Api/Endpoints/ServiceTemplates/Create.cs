using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class Create : EndpointBaseAsync
  .WithRequest<CreateServiceTemplate>
  .WithActionResult<ServiceTemplateRecord>
{
  private readonly IRepository<ServiceTemplate> _repository;
  private readonly IMetadataFormatValidator _metadataValidator;

  public Create(IRepository<ServiceTemplate> repository, IMetadataFormatValidator metadataValidator)
  {
    _repository = repository;
    _metadataValidator = metadataValidator;
  }

  [Authorize]
  [HttpPost(CreateServiceTemplate.Route)]
  [SwaggerOperation(
    Summary = "Add a new service template",
    Description = "Create a new service template",
    OperationId = "ServiceTemplate.Create",
    Tags = new[] { "ServiceTemplateEndpoints" }
  )]
  [SwaggerResponse(200, "The service template was created", typeof(ServiceTemplateRecord))]
  [SwaggerResponse(400, "A parameter was null or invalid", typeof(ErrorResponse))]
  [SwaggerResponse(409, "A service template with the same name already exists")]
  public override async Task<ActionResult<ServiceTemplateRecord>> HandleAsync(
    CreateServiceTemplate request,
    CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length is 0 or > 50 ||
        request.LocalizedMetadata == null || request.StaticMetadata == null)
    {
      return BadRequest(new ErrorResponse("A required parameter was null"));
    }

    var nameSpec = new ServiceTemplateByNameSpec(request.Name.Trim());
    if (await _repository.CountAsync(nameSpec, cancellationToken) > 0)
    {
      return Conflict();
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
      template = new ServiceTemplate(request.Name.Trim(), staticMetadata, localizedMetadata);
      template = await _repository.AddAsync(template, cancellationToken);
    }

    await _repository.SaveChangesAsync(cancellationToken);
    var result = ServiceTemplateRecord.FromEntity(template);
    return Ok(result);
  }
}

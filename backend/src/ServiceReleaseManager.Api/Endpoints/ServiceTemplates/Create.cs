using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;
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
  [SwaggerResponse(400, "A parameter was null or invalid")]
  [SwaggerResponse(409, "A service template with the same name already exists")]
  public override async Task<ActionResult<ServiceTemplateRecord>> HandleAsync(CreateServiceTemplate request,
    CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length is 0 or > 50 ||
        string.IsNullOrWhiteSpace(request.LocalizedMetadata) ||
        string.IsNullOrWhiteSpace(request.StaticMetadata))
    {
      return BadRequest();
    }

    var nameSpec = new ServiceTemplateByNameSpec(request.Name);
    if (await _repository.CountAsync(nameSpec, cancellationToken) > 0)
    {
      return Conflict();
    }

    if (!_metadataValidator.IsValidMetadataJson(request.LocalizedMetadata) ||
        !_metadataValidator.IsValidMetadataJson(request.StaticMetadata))
    {
      return BadRequest();
    }

    var staticMetadata = _metadataValidator.NormalizeMetadataJson(request.StaticMetadata);
    var localizedMetadata = _metadataValidator.NormalizeMetadataJson(request.LocalizedMetadata);

    var template = new ServiceTemplate(request.Name.Trim(), staticMetadata, localizedMetadata);
    var created = await _repository.AddAsync(template, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    var result = ServiceTemplateRecord.FromEntity(created);
    return Ok(result);
  }
}

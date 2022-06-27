using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class Update : EndpointBase.WithRequest<UpdateServiceTemplate>.WithActionResult<
  ServiceTemplateRecord>
{
  private readonly IMetadataFormatValidator _metadataValidator;
  private readonly IRepository<ServiceTemplate> _repository;

  public Update(IRepository<ServiceTemplate> repository, IMetadataFormatValidator metadataValidator)
  {
    _repository = repository;
    _metadataValidator = metadataValidator;
  }

  [HttpPatch]
  [SwaggerOperation(
    Summary = "Update a service template",
    Description = "Update a service template",
    OperationId = "ServiceTemplate.Update",
    Tags = new[] { "ServiceTemplate" }
  )]
  [SwaggerResponse(200, "The service template was updated", typeof(ServiceTemplateRecord))]
  [SwaggerResponse(400, "The request was invalid", typeof(ErrorResponse))]
  [SwaggerResponse(404, "A service template with the specified name was not found")]
  public override async Task<ActionResult<ServiceTemplateRecord>> HandleAsync(
    UpdateServiceTemplate request,
    CancellationToken cancellationToken = new())
  {
    var idSpec = new ServiceTemplateByIdSpec(request.ServiceTemplateId);
    var serviceTemplate = await _repository.GetBySpecAsync(idSpec, cancellationToken);
    if (serviceTemplate == null)
    {
      return NotFound();
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

    serviceTemplate.Name = request.Name;
    serviceTemplate.LocalizedMetadata = localizedMetadata;
    serviceTemplate.StaticMetadata = staticMetadata;
    await _repository.UpdateAsync(serviceTemplate, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

    var result = ServiceTemplateRecord.FromEntity(serviceTemplate);
    return Ok(result);
  }
}

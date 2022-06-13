using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Sepcifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class Get : AuthorizedEndpointBase
  .WithRequest<GetServiceTemplate>
  .WithActionResult<ServiceTemplateRecord>
{
  private readonly IRepository<ServiceTemplate> _repository;

  public Get(IRepository<ServiceTemplate> repository)
  {
    _repository = repository;
  }

  [HttpGet("{serviceName}")]
  [SwaggerOperation(
    Summary = "Get a service template",
    Description = "Get a service template by its name",
    OperationId = "ServiceTemplate.Get",
    Tags = new[] { "ServiceTemplateEndpoints" }
  )]
  [SwaggerResponse(200, "The service template was found", typeof(ServiceTemplateRecord))]
  [SwaggerResponse(404, "The service template was not found")]
  public override async Task<ActionResult<ServiceTemplateRecord>> HandleAsync(
    [FromRoute] GetServiceTemplate request,
    CancellationToken cancellationToken = new())
  {
    var spec = new ServiceTemplateByNameSpec(request.ServiceName);
    var template = await _repository.GetBySpecAsync(spec, cancellationToken);
    if (template == null)
    {
      return NotFound();
    }

    var response = ServiceTemplateRecord.FromEntity(template);
    return Ok(response);
  }
}

using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.ServiceTemplates;
using ServiceReleaseManager.Core.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListServiceTemplates : EndpointBase.WithRequest<ListServiceTemplateRequest>.
  WithActionResult<List<ServiceTemplateRecord>>
{
  private readonly IServiceService _serviceService;

  public ListServiceTemplates(IServiceService serviceService)
  {
    _serviceService = serviceService;
  }

  [HttpGet(ListServiceTemplateRequest.Route)]
  [SwaggerOperation(
    Summary = "Get the service's service template",
    Description = "Get the service's service template",
    OperationId = "Service.GetServiceTemplate",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "Success", typeof(ServiceTemplateRecord))]
  [SwaggerResponse(400, "Not found")]
  public override async Task<ActionResult<List<ServiceTemplateRecord>>> HandleAsync(
    [FromRoute] ListServiceTemplateRequest request,
    CancellationToken cancellationToken = new()
  )
  {
    var service =
      await _serviceService.GetByRouteName(request.ServiceRouteName, cancellationToken);

    if (!service.IsSuccess)
    {
      return BadRequest();
    }

    var response = ServiceTemplateRecord.FromEntity(service.Value.ServiceTemplate);
    return Ok(response);
  }
}

using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.ServiceTemplates;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class ListServiceTemplates : EndpointBase.WithRequest<ListServiceTemplatesRequest>.
  WithActionResult<List<ServiceTemplateRecord>>
{
  private readonly IRepository<ServiceTemplate> _serviceTemplateRepository;
  private readonly IServiceService _serviceService;

  public ListServiceTemplates(
    IRepository<ServiceTemplate> serviceTemplateRepository, IServiceService serviceService)
  {
    _serviceTemplateRepository = serviceTemplateRepository;
    _serviceService = serviceService;
  }

  [HttpGet(ListServiceTemplatesRequest.Route)]
  [SwaggerOperation(
    Summary = "List all service templates",
    Description = "List all service templates",
    OperationId = "Service.ListServiceTemplates",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "Success", typeof(List<ServiceTemplateRecord>))]
  [SwaggerResponse(404, "Not found")]
  public override async Task<ActionResult<List<ServiceTemplateRecord>>> HandleAsync(
    [FromRoute] ListServiceTemplatesRequest request,
    CancellationToken cancellationToken = new())
  {
    var service =
      await _serviceService.GetByRouteName(request.ServiceRouteName, cancellationToken);

    if (!service.IsSuccess)
    {
      return NotFound();
    }

    var spec = new ActiveServiceTemplatesSearchSpec(service.Value.OrganisationId);
    var templates = await _serviceTemplateRepository.ListAsync(spec, cancellationToken);
    var response = templates.ConvertAll(ServiceTemplateRecord.FromEntity);

    return Ok(response);
  }
}

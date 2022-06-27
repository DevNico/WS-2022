using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Api.Endpoints.ServiceTemplates;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListServiceTemplates : EndpointBase.WithRequest<ListServiceTemplatesRequest>.WithActionResult<List<ServiceTemplateRecord>>
{
  private readonly IRepository<ServiceTemplate> _serviceTemplateRepository;
  private readonly IOrganisationService _organisationService;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public ListServiceTemplates(
    IRepository<ServiceTemplate> serviceTemplateRepository,
    IOrganisationService organisationService,
    IServiceManagerAuthorizationService authorizationService
  )
  {
    _serviceTemplateRepository = serviceTemplateRepository;
    _organisationService = organisationService;
    _authorizationService = authorizationService;
  }

  [HttpGet(ListServiceTemplatesRequest.Route)]
  [SwaggerOperation(
    Summary = "List all service templates",
    Description = "List all service templates",
    OperationId = "ServiceTemplate.List",
    Tags = new[] { "ServiceTemplateEndpoints" }
  )]
  [SwaggerResponse(200, "Success", typeof(List<ServiceTemplateRecord>))]
  public override async Task<ActionResult<List<ServiceTemplateRecord>>> HandleAsync(
    [FromRoute] ListServiceTemplatesRequest request,
    CancellationToken cancellationToken = new())
  {
    if (!await _authorizationService.EvaluateOrganisationAuthorization(User, request.OrganisationRouteName, ServiceTemplateOperations.ServiceTemplate_List, cancellationToken))
    {
      return Unauthorized();
    }

    var organisation =
      await _organisationService.GetByRouteNameRequest(request.OrganisationRouteName, cancellationToken);

    if (!organisation.IsSuccess)
    {
      return BadRequest();
    }

    var spec = new ActiveServiceTemplatesSearchSpec(organisation.Value.Id);
    var templates = await _serviceTemplateRepository.ListAsync(spec, cancellationToken);
    var response = templates.ConvertAll(ServiceTemplateRecord.FromEntity);

    return Ok(response);
  }
}

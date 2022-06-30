using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class Get : EndpointBase.WithRequest<GetServiceTemplateById>.WithActionResult<
  ServiceTemplateRecord>
{
  private readonly IServiceManagerAuthorizationService _authorizationService;
  private readonly IRepository<ServiceTemplate> _repository;

  public Get(
    IRepository<ServiceTemplate> repository,
    IServiceManagerAuthorizationService authorizationService
  )
  {
    _repository = repository;
    _authorizationService = authorizationService;
  }

  [HttpGet(GetServiceTemplateById.Route)]
  [SwaggerOperation(
    Summary = "Get a service template",
    Description = "Get a service template by its id",
    OperationId = "ServiceTemplate.Get",
    Tags = new[] { "ServiceTemplate" }
  )]
  [SwaggerResponse(200, "The service template was found", typeof(ServiceTemplateRecord))]
  [SwaggerResponse(404, "The service template was not found")]
  public override async Task<ActionResult<ServiceTemplateRecord>> HandleAsync(
    [FromRoute] GetServiceTemplateById request,
    CancellationToken cancellationToken = new()
  )
  {
    var spec = new ServiceTemplateByIdSpec(request.ServiceTemplateId);
    var template = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (template == null)
    {
      return NotFound();
    }

    if (!await _authorizationService.EvaluateOrganisationAuthorization(
          User,
          template.OrganisationId,
          ServiceTemplateOperations.ServiceTemplate_Read,
          cancellationToken
        ))
    {
      return NotFound();
    }

    var response = ServiceTemplateRecord.FromEntity(template);
    return Ok(response);
  }
}

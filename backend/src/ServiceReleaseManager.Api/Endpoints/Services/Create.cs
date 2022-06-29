using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Service;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class Create : EndpointBase.WithRequest<CreateServiceRequest>.WithActionResult<ServiceRecord>
{
  private readonly IServiceService _service;
  private readonly IServiceManagerAuthorizationService _authorizationService;

  public Create(IServiceService service, IServiceManagerAuthorizationService authorizationService)
  {
    _service = service;
    _authorizationService = authorizationService;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Create a service",
    Description = "Create a service",
    OperationId = "Service.Create",
    Tags = new[] { "Service" }
  )]
  [SwaggerResponse(200, "The service was created", typeof(ServiceRecord))]
  [SwaggerResponse(400, "A parameter was null or invalid", typeof(ErrorResponse))]
  [SwaggerResponse(409, "A service with the same name and organisation already exists",
    typeof(ErrorResponse))]
  public override async Task<ActionResult<ServiceRecord>> HandleAsync(CreateServiceRequest request,
    CancellationToken cancellationToken = new())
  {
    var serviceTemplate =
      await _service.GetServiceTemplate(request.ServiceTemplateId, cancellationToken);

    if (!serviceTemplate.IsSuccess ||
        !await _authorizationService.EvaluateOrganisationAuthorization(User,
          serviceTemplate.Value.OrganisationId, ServiceOperations.Service_Create,
          cancellationToken))
    {
      return Unauthorized();
    }

    var found = await _service.GetByNameAndOrganisationId(
      request.Name,
      serviceTemplate.Value.OrganisationId,
      cancellationToken
    );

    if (found.IsSuccess)
    {
      return Conflict(
        new ErrorResponse("A service with the same name and organisation already exists"));
    }

    var created = await _service.Create(
      request.Name,
      request.Description,
      request.ServiceTemplateId,
      cancellationToken
    );
    return this.ToActionResult(created.MapValue(ServiceRecord.FromEntity));
  }
}

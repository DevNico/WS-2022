using Ardalis.Result.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class Create : EndpointBase.WithRequest<CreateServiceRequest>.WithActionResult<ServiceRecord>
{
  private readonly IServiceService _service;

  public Create(IServiceService service)
  {
    _service = service;
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
    var found =
      await _service.GetByNameAndOrganisationId(request.Name, request.OrganisationId,
        cancellationToken);
    if (found.IsSuccess)
    {
      return Conflict(
        new ErrorResponse("A service with the same name and organisation already exists"));
    }

    var service = new Service(request.Name, request.Description, request.OrganisationId);
    var created = await _service.Create(service, cancellationToken);

    return this.ToActionResult(created.MapValue(ServiceRecord.FromEntity));
  }
}

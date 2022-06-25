using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class Create : EndpointBase.WithRequest<CreateServiceRequest>.WithActionResult<ServiceRecord>
{
  private readonly IRepository<Service> _repository;

  public Create(IRepository<Service> repository)
  {
    _repository = repository;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Create a service",
    Description = "Create a service",
    OperationId = "Service.Create",
    Tags = new[] { "ServiceEndpoints" }
  )]
  [SwaggerResponse(200, "The service was created", typeof(ServiceRecord))]
  [SwaggerResponse(400, "A parameter was null or invalid", typeof(ErrorResponse))]
  public override async Task<ActionResult<ServiceRecord>> HandleAsync(CreateServiceRequest request,
    CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length is < 5 or > 50 ||
        string.IsNullOrWhiteSpace(request.Description) || request.Description.Length > 200)
    {
      return BadRequest(new ErrorResponse("A required parameter was null"));
    }

    var service = new Service(request.Name, request.Description, request.OrganisationId);
    await _repository.AddAsync(service, cancellationToken);

    return Ok(ServiceRecord.FromEntity(service));
  }
}

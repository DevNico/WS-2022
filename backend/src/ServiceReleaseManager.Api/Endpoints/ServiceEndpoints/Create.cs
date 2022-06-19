using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class Create : AuthorizedEndpointBase.WithRequest<CreateServiceRequest>.WithActionResult<ServiceRecord>
{
  private readonly IRepository<Service> _repository;

  public Create(IRepository<Service> repository)
  {
    _repository = repository;
  }

  [HttpPost]
  [SwaggerOperation(
    Summary = "Create a service",
    Description = "Create a service by its id",
    OperationId = "Service.Create",
    Tags = new[] { "ServiceEndpoints" }
  )]
  [SwaggerResponse(200, "The service was created", typeof(ServiceRecord))]
  [SwaggerResponse(400, "A parameter was null or invalid", typeof(ErrorResponse))]
  public override async Task<ActionResult<ServiceRecord>> HandleAsync(CreateServiceRequest request, CancellationToken cancellationToken = new())
  {
    if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length is 0 or > 50)
    {
      return BadRequest(new ErrorResponse("A required parameter was null"));
    }
    
    var service = new Service(request.Name);
    await _repository.AddAsync(service, cancellationToken);

    return Ok(ServiceRecord.FromEntity(service));
  }
}

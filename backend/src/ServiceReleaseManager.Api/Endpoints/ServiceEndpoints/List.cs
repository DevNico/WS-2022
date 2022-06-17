using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class List : AuthorizedEndpointBase
  .WithoutRequest
  .WithActionResult<List<ServiceRecord>>
{
  private readonly IRepository<Service> _repository;

  public List(IRepository<Service> repository)
  {
    _repository = repository;
  }
  
  [HttpGet]
  [SwaggerOperation(
    Summary = "List all services",
    Description = "List all services",
    OperationId = "Service.List",
    Tags = new[] { "ServiceEndpoints" }
  )]
  [SwaggerResponse(200, "Success", typeof(List<ServiceRecord>))]
  public override async Task<ActionResult<List<ServiceRecord>>> HandleAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    var services = await _repository.ListAsync(new ActiveServiceSearchSpec(), cancellationToken);
    var response = services.ConvertAll(ServiceRecord.FromEntity);

    return Ok(response);
  }
}

﻿using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Api.Endpoints.Services;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class ListServices : EndpointBase.WithRequest<ListOrganisationServicesRequest>.
  WithActionResult<List<ServiceRecord>>
{
  private readonly IRepository<Service> _serviceRepository;

  public ListServices(IRepository<Service> serviceRepository)
  {
    _serviceRepository = serviceRepository;
  }

  [HttpGet(ListOrganisationServicesRequest.Route)]
  [SwaggerOperation(
    Summary = "List all services",
    Description = "List all services",
    OperationId = "Organisation.ListServices",
    Tags = new[] { "Organisation" }
  )]
  [SwaggerResponse(200, "Success", typeof(List<ServiceRecord>))]
  public override async Task<ActionResult<List<ServiceRecord>>> HandleAsync(
    [FromRoute] ListOrganisationServicesRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new ServiceByOrganisationSearchSpec(request.OrganisationRouteName);
    var services = await _serviceRepository.ListAsync(spec, cancellationToken);

    return Ok(services.ConvertAll(ServiceRecord.FromEntity));
  }
}
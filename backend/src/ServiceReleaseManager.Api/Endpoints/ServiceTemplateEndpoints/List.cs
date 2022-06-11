﻿using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplateEndpoints;

public class List : EndpointBaseAsync
  .WithoutRequest
  .WithActionResult<List<ServiceTemplateRecord>>
{
  private readonly IRepository<ServiceTemplate> _repository;
  
  public List(IRepository<ServiceTemplate> repository)
  {
    _repository = repository;
  }

  [HttpGet("/servicetemplates")]
  [Authorize]
  [SwaggerOperation(
    Summary = "List all service templates",
    Description = "List all service templates",
    OperationId = "ServiceTemplate.List",
    Tags = new[] { "ServiceTemplateEndpoints" }
  )]
  [SwaggerResponse(200, "Success", typeof(List<ServiceTemplateRecord>))]
  public override async Task<ActionResult<List<ServiceTemplateRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    var templates = await _repository.ListAsync(cancellationToken);
    var response = templates.ConvertAll(ServiceTemplateRecord.FromEntity);

    return Ok(response);
  }
}

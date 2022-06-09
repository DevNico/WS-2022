using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class List : EndpointBaseAsync.WithoutRequest.WithActionResult<List<LocaleRecord>>
{
  private readonly IRepository<Locale> _repository;

  public List(IRepository<Locale> repository)
  {
    _repository = repository;
  }

  [HttpGet("/locales")]
  [SwaggerOperation(
    Summary = "List all locales",
    OperationId = "Locales.List",
    Tags = new[] { "LocaleEndpoints" }
  )]
  public override async Task<ActionResult<List<LocaleRecord>>> HandleAsync(
    CancellationToken cancellationToken = new())
  {
    var locales = await _repository.ListAsync(cancellationToken);
    var response = locales.Select(LocaleRecord.FromEntity);

    return Ok(response);
  }
}

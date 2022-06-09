using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class GetById : EndpointBaseAsync.WithRequest<GetLocaleByIdRequest>.WithActionResult<LocaleRecord>
{
  private readonly IRepository<Locale> _repository;

  public GetById(IRepository<Locale> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetLocaleByIdRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a locale by id",
    Description = "Get a locale by id",
    OperationId = "Locale.GetById",
    Tags = new[] { "LocaleEndpoints" }
  )]
  public override async Task<ActionResult<LocaleRecord>> HandleAsync(
    [FromRoute] GetLocaleByIdRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new LocaleByIdSpec(request.LocaleId);
    var locale = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (locale == null)
    {
      return NotFound();
    }

    var response = LocaleRecord.FromEntity(locale);
    return Ok(response);
  }
}

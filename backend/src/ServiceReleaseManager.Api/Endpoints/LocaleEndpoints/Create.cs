using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateLocaleRequest>
  .WithActionResult<LocaleRecord>
{
  private readonly IRepository<Locale> _repository;

  public Create(IRepository<Locale> repository)
  {
    _repository = repository;
  }

  [HttpPost(CreateLocaleRequest.Route)]
  [SwaggerOperation(
    Summary = "Create a new locale",
    Description = "Create a new locale",
    OperationId = "Locale.Create",
    Tags = new[] { "LocaleEndpoints" }
  )]
  public override async Task<ActionResult<LocaleRecord>> HandleAsync(CreateLocaleRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.LanguageCode == null || request.CountryCode == null)
    {
      return BadRequest();
    }

    var spec = new LocaleByLanguageAndCountryCodeSpec(request.LanguageCode, request.CountryCode);
    if (await _repository.CountAsync(spec, cancellationToken) > 0)
    {
      return Conflict();
    }

    var locale = new Locale(request.LanguageCode, request.CountryCode, request.IsDefault.GetValueOrDefault(false));
    var createdLocale = await _repository.AddAsync(locale, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);
    var response = LocaleRecord.FromEntity(createdLocale);

    return Ok(response);
  }
}

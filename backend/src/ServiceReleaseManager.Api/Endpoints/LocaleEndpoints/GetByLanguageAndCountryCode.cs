using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class GetByLanguageAndCountryCode : EndpointBaseAsync.WithRequest<GetLocaleByLanguageAndCountryCodeRequest>.
  WithActionResult<LocaleRecord>
{
  private readonly IRepository<Locale> _repository;

  public GetByLanguageAndCountryCode(IRepository<Locale> repository)
  {
    _repository = repository;
  }

  [HttpGet(GetLocaleByLanguageAndCountryCodeRequest.Route)]
  [SwaggerOperation(
    Summary = "Get a locale by its language and country codes",
    Description = "Get a locale by its language and country codes",
    OperationId = "Locale.GetByLanguageAndCountryCode",
    Tags = new[] { "LocaleEndpoints" }
  )]
  public override async Task<ActionResult<LocaleRecord>> HandleAsync(
    [FromRoute] GetLocaleByLanguageAndCountryCodeRequest request,
    CancellationToken cancellationToken = new())
  {
    var spec = new LocaleByLanguageAndCountryCodeSpec(request.LocaleLanguage, request.LocaleCountry);
    var locale = await _repository.GetBySpecAsync(spec, cancellationToken);

    if (locale == null)
    {
      return NotFound();
    }

    var response = LocaleRecord.FromEntity(locale);
    return Ok(response);
  }
}

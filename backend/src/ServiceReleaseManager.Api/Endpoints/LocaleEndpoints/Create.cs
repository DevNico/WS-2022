using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.LocaleEndpoints;

public class Create : EndpointBaseAsync
  .WithRequest<CreateLocaleRequest>
  .WithActionResult<LocaleRecord>
{
  private readonly IRepository<Locale> _repository;
  private readonly IRepository<Service> _serviceRepository;

  public Create(IRepository<Locale> repository, IRepository<Service> serviceRepository)
  {
    _repository = repository;
    _serviceRepository = serviceRepository;
  }

  [Authorize]
  [HttpPost(CreateLocaleRequest.Route)]
  [SwaggerOperation(
    Summary = "Create a new locale",
    Description = "Create a new locale",
    OperationId = "Locale.Create",
    Tags = new[] { "LocaleEndpoints" }
  )]
  [SwaggerResponse(200, "The locale was created", typeof(LocaleRecord))]
  [SwaggerResponse(400, "The request was invalid")]
  [SwaggerResponse(404, "The service was not found")]
  [SwaggerResponse(409, "The locale already exists")]
  public override async Task<ActionResult<LocaleRecord>> HandleAsync(CreateLocaleRequest request,
    CancellationToken cancellationToken = new())
  {
    if (request.LanguageCode == null || request.CountryCode == null)
    {
      return BadRequest();
    }

    var spec = new ServiceByIdSpec(request.ServiceId);
    var service = await _serviceRepository.GetBySpecAsync(spec, cancellationToken);
    if (service == null)
    {
      return NotFound();
    }

    if (service.Locales.Any(l => l.LanguageCode == request.LanguageCode && l.CountryCode == request.CountryCode))
    {
      return Conflict();
    }

    var locale = new Locale(request.LanguageCode, request.CountryCode, request.IsDefault.GetValueOrDefault(false));
    var createdLocale = await _repository.AddAsync(locale, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);
    
    service.Locales.Add(createdLocale);
    await _serviceRepository.UpdateAsync(service, cancellationToken);
    await _serviceRepository.SaveChangesAsync(cancellationToken);

    var response = LocaleRecord.FromEntity(createdLocale);
    return Ok(response);
  }
}

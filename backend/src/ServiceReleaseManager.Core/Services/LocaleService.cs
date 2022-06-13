using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class LocaleService : ILocaleService
{
  private readonly IServiceService _serviceService;
  private readonly IRepository<Locale> _localeRepository;

  public LocaleService(IServiceService serviceService, IRepository<Locale> localeRepository)
  {
    _localeRepository = localeRepository;
    _serviceService = serviceService;
  }

  public async Task<Result<Locale>> GetById(int id, CancellationToken cancellationToken)
  {
    var locale = await _localeRepository.GetByIdAsync(id, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(locale);
  }

  public async Task<Result<List<Locale>>> ListByServiceId(int serviceId,
    CancellationToken cancellationToken)
  {
    var locales = await _localeRepository.ListAsync(cancellationToken);
    var spec = new LocalesByServiceIdSpec(serviceId);
    var localesByServiceId = spec.Evaluate(locales);

    return Result.Success(localesByServiceId.ToList());
  }

  public async Task<Result<Locale>> Create(Locale locale, CancellationToken cancellationToken)
  {
    var service = await _serviceService.GetById(locale.ServiceId, cancellationToken);
    if (!service.IsSuccess)
    {
      return Result.NotFound();
    }

    var existingLocale =
      await _getLocaleByCodes(locale.CountryCode, locale.LanguageCode, cancellationToken);
    if (existingLocale.IsSuccess)
    {
      return Result.Error("Locale already exists");
    }

    var createdLocale = await _localeRepository.AddAsync(locale, cancellationToken);
    await _localeRepository.SaveChangesAsync(cancellationToken);

    return Result.Success(createdLocale);
  }

  public async Task<Result> Delete(int localeId, CancellationToken cancellationToken)
  {
    var locale =
      await GetById(localeId, cancellationToken);

    if (!locale.IsSuccess)
    {
      return Result.NotFound();
    }

    await _localeRepository.DeleteAsync(locale, cancellationToken);
    await _localeRepository.SaveChangesAsync(cancellationToken);

    return Result.Success();
  }

  private async Task<Result<Locale>> _getLocaleByCodes(string countryCode, string languageCode,
    CancellationToken cancellationToken)
  {
    var spec = new LocaleByCodesSpec(countryCode, languageCode);
    var locale = await _localeRepository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(locale);
  }
}

using Ardalis.Result;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.Services;

public class LocaleService : ILocaleService
{
  private readonly IRepository<Locale> _localeRepository;
  private readonly IServiceService _serviceService;

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

  public async Task<Result<List<Locale>>> ListByServiceRouteName(
    string serviceRouteName,
    CancellationToken cancellationToken)
  {
    var service = await _serviceService.GetByRouteName(serviceRouteName, cancellationToken);
    if (!service.IsSuccess)
    {
      return Result<List<Locale>>.NotFound();
    }

    var locales = await _localeRepository.ListAsync(cancellationToken);
    var spec = new LocalesByServiceIdSpec(service.Value.Id);
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

    var existingLocale = await _getLocaleByTag(
      locale.ServiceId,
      locale.Tag,
      cancellationToken
    );
    if (existingLocale.IsSuccess)
    {
      return Result.Error("Locale already exists");
    }

    if (locale.IsDefault)
    {
      var defaultLocaleResult = await _getDefaultLocale(locale.ServiceId, cancellationToken);

      if (defaultLocaleResult.IsSuccess)
      {
        var defaultLocale = defaultLocaleResult.Value;
        defaultLocale.IsDefault = false;
        await _localeRepository.UpdateAsync(defaultLocale, cancellationToken);
      }
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

  private async Task<Result<Locale>> _getLocaleByTag(
    int serviceId,
    string tag,
    CancellationToken cancellationToken)
  {
    var spec = new LocaleByTagSpec(serviceId, tag);
    var locale = await _localeRepository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(locale);
  }

  private async Task<Result<Locale>> _getDefaultLocale(
    int serviceId,
    CancellationToken cancellationToken
  )
  {
    var spec = new DefaultLocaleSpec(serviceId);
    var locale = await _localeRepository.GetBySpecAsync(spec, cancellationToken);
    return ResultHelper.NullableSuccessNotFound(locale);
  }
}

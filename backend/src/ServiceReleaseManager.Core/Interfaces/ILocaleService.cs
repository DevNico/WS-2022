using Ardalis.Result;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface ILocaleService
{
  public Task<Result<Locale>> GetById(int id, CancellationToken cancellationToken);

  public Task<Result<List<Locale>>> ListByServiceId(int serviceId,
    CancellationToken cancellationToken);

  public Task<Result<Locale>> Create(Locale locale, CancellationToken cancellationToken);

  public Task<Result> Delete(int localeId, CancellationToken cancellationToken);
}

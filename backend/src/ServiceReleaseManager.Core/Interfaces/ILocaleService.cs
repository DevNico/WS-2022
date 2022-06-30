using Ardalis.Result;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Core.Interfaces;

public interface ILocaleService
{
  public Task<Result<Locale>> GetById(int id, CancellationToken cancellationToken);

  public Task<Result<List<Locale>>> ListByServiceRouteName(
    string serviceRouteName,
    CancellationToken cancellationToken
  );

  public Task<Result<Locale>> Create(Locale locale, CancellationToken cancellationToken);

  public Task<Result> Delete(int localeId, CancellationToken cancellationToken);
}

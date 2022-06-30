using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceByLocaleIdSpec : Specification<Service>, ISingleResultSpecification
{
  public ServiceByLocaleIdSpec(int localeId)
  {
    Query
     .Include(s => s.Locales)
     .Where(s => s.Locales.Any(l => l.Id == localeId));
  }
}

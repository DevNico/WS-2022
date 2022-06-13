using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ReleaseAggregate.Specifications;

public sealed class LocaleByIdSpec : Specification<Locale>, ISingleResultSpecification
{
  public LocaleByIdSpec(int id)
  {
    Query.Where(l => l.Id == id);
  }
}

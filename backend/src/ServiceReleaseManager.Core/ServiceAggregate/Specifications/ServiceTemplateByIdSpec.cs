using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public sealed class ServiceTemplateByIdSpec : Specification<ServiceTemplate>,
                                              ISingleResultSpecification
{
  public ServiceTemplateByIdSpec(int id)
  {
    Query
     .Where(s => s.IsActive)
     .Where(s => s.Id == id);
    Query
     .Include(s => s.Organisation);
  }
}

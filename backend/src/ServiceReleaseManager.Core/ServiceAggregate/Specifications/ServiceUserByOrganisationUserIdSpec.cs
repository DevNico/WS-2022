using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public class ServiceUserByOrganisationUserIdSpec : Specification<ServiceUser>, ISingleResultSpecification
{
  public ServiceUserByOrganisationUserIdSpec(int organisationUserId)
  {
    Query
      .Where(s => s.OrganisationUserId == organisationUserId)
      .Include(s => s.ServiceRole);
  }
}

﻿using Ardalis.Specification;

namespace ServiceReleaseManager.Core.ServiceAggregate.Specifications;

public class ServiceUserByOrganisationUserIdSpec : Specification<ServiceUser>,
                                                   ISingleResultSpecification
{
  public ServiceUserByOrganisationUserIdSpec(int organisationUserId, int serviceId)
  {
    Query
     .Where(s => s.OrganisationUserId == organisationUserId)
     .Where(s => s.ServiceId == serviceId)
     .Include(s => s.ServiceRole);
  }
}

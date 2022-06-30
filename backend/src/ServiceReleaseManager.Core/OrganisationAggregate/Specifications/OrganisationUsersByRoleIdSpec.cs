using Ardalis.Specification;

namespace ServiceReleaseManager.Core.OrganisationAggregate.Specifications;

public sealed class OrganisationUsersByRoleIdSpec : Specification<OrganisationUser>
{
  public OrganisationUsersByRoleIdSpec(int roleId)
  {
    Query
     .Where(u => u.IsActive)
     .Where(u => u.RoleId == roleId);
  }
}

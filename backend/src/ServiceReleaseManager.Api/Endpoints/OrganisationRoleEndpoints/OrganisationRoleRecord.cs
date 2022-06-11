using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationRoleEndpoints;

public record OrganisationRoleRecord(string name, bool serviceRead, bool serviceWrite,
  bool serviceDelete,
  bool userRead, bool userWrite, bool userDelete)
{
  public static OrganisationRoleRecord FromEntity(OrganisationRole role)
  {
    return new OrganisationRoleRecord(role.Name, role.ServiceRead, role.ServiceWrite,
      role.ServiceDelete, role.UserRead, role.UserWrite, role.UserDelete);
  }
}

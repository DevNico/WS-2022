using ServiceReleaseManager.Api.Endpoints.OrganisationRoles;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public record OrganisationUserRecord(
  int Id,
  string UserId,
  string Email,
  string FirstName,
  string LastName,
  OrganisationRoleRecord OrganisationRole
)
{
  public static OrganisationUserRecord FromEntity(OrganisationUser user)
  {
    return new OrganisationUserRecord(
      user.Id,
      user.UserId,
      user.Email,
      user.FirstName,
      user.LastName,
      OrganisationRoleRecord.FromEntity(user.Role)
    );
  }
}

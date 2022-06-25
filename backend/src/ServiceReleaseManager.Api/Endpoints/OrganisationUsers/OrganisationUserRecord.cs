using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public record OrganisationUserRecord(
  int Id,
  string UserId,
  string Email,
  string FirstName,
  string LastName
)
{
  public static OrganisationUserRecord FromEntity(OrganisationUser user)
  {
    return new OrganisationUserRecord(
      Id: user.Id,
      UserId: user.UserId,
      Email: user.Email,
      FirstName: user.FirstName,
      LastName: user.LastName
    );
  }
}

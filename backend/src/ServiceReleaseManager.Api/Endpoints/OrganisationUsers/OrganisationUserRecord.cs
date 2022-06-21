using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUsers;

public record OrganisationUserRecord(string UserId, string Email, string FirstName, string LastName,
  DateTime? LastSignIn)
{
  public static OrganisationUserRecord FromEntity(OrganisationUser user)
  {
    return new OrganisationUserRecord(user.UserId, user.Email, user.FirstName,
      user.LastName, user.LastSignIn);
  }
}

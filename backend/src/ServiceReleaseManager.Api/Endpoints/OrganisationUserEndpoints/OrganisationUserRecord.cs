using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public record OrganisationUserRecord(string userId, string email, bool emailVerified,
  string firstName, string lastName,
  DateTime? lastSignIn)
{
  public static OrganisationUserRecord FromEntity(OrganisationUser user)
  {
    return new OrganisationUserRecord(user.UserId, user.Email, user.EmailVerified, user.FirstName,
      user.LastName, user.LastSignIn);
  }
}

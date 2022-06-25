using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public record ServiceUserRecord(
  int ServiceRole,
  int OrganisationUser
)
{
  public static ServiceUserRecord FromEntity(ServiceUser entity)
  {
    return new ServiceUserRecord(
      entity.ServiceRoleId,
      entity.OrganisationUserId
    );
  }
}

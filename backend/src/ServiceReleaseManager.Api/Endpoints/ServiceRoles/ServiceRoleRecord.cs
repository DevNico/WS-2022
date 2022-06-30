using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public record ServiceRoleRecord(
  int Id,
  string Name,
  bool ReleaseCreate,
  bool ReleaseApprove,
  bool ReleasePublish,
  bool ReleaseMetadataEdit,
  bool ReleaseLocalizedMetadataEdit,
  int OrganisationId
)
{
  public static ServiceRoleRecord FromEntity(ServiceRole role)
  {
    return new ServiceRoleRecord(
      role.Id,
      role.Name,
      role.ReleaseCreate,
      role.ReleaseApprove,
      role.ReleasePublish,
      role.ReleaseMetadataEdit,
      role.ReleaseLocalizedMetadataEdit,
      role.OrganisationId
    );
  }
}

using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public record OrganisationRecord(int Id, string Name, DateTime UpdatedAt, DateTime CreatedAt)
{
  public static OrganisationRecord FromEntity(Organisation entity)
  {
    return new OrganisationRecord(entity.Id, entity.Name, entity.UpdatedAt, entity.CreatedAt);
  }
}

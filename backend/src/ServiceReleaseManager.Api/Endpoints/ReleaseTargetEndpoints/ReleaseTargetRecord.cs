using ServiceReleaseManager.Core.ReleaseAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseTargetEndpoints;

public record ReleaseTargetRecord(string Name, bool? RequiresApproval)
{
  public static ReleaseTargetRecord FromEntity(ReleaseTarget entity)
  {
    return new ReleaseTargetRecord(entity.Name, entity.RequiresApproval);
  }
}

using Microsoft.AspNetCore.Authorization.Infrastructure;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public class OrganisationAuthorizationRequirement : OperationAuthorizationRequirement
{
  public Func<OrganisationRole, bool>? EvaluationFunction { get; init; }
}

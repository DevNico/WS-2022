using Microsoft.AspNetCore.Authorization.Infrastructure;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public class ServiceAuthorizationRequirement : OperationAuthorizationRequirement
{
  public Func<ServiceRole, bool>? EvaluationFunction { get; init; }

}


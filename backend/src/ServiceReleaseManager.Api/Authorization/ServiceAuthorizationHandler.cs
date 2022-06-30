using Microsoft.AspNetCore.Authorization;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public class
  ServiceAuthorizationHandler : AuthorizationHandler<ServiceAuthorizationRequirement, ServiceRole>
{
  protected override Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    ServiceAuthorizationRequirement requirement,
    ServiceRole resource
  )
  {
    if (context.User.IsInRole("superAdmin"))
    {
      context.Succeed(requirement);
      return Task.CompletedTask;
    }

    if (requirement.EvaluationFunction != null && requirement.EvaluationFunction.Invoke(resource))
    {
      context.Succeed(requirement);
    }

    return Task.CompletedTask;
  }
}

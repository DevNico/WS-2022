using Microsoft.AspNetCore.Authorization;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public class OrganisationAuthorizationHandler : AuthorizationHandler<
  OrganisationAuthorizationRequirement, OrganisationRole>
{
  protected override Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    OrganisationAuthorizationRequirement requirement,
    OrganisationRole resource
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

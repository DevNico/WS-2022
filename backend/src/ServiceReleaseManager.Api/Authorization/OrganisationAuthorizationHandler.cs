using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ServiceReleaseManager.Core.OrganisationAggregate;

namespace ServiceReleaseManager.Api.Authorization;

public class OrganisationAuthorizationHandler :
  AuthorizationHandler<OrganisationRequirement, Organisation>
{

  protected override async Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    OrganisationRequirement requirement,
    Organisation resource)
  {
    // var identityUser = _userManager.GetUserAsync(context.User).Result;
    //
    // if (identityUser == null)
    // {
    //   context.Fail();
    //   return;
    // }
    //
    // if (await _userManager.IsInRoleAsync(identityUser, "Administrator"))
    // {
    //   context.Succeed(requirement);
    //   return;
    // }
    //
    // if (resource.Users.Any(o => o.UserId == identityUser.Id))
    // {
    //   context.Succeed(requirement);
    // }
  }
}

public class OrganisationRequirement : IAuthorizationRequirement
{
}

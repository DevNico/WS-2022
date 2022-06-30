using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Moq;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.OrganisationAggregate;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Api.Authorization;

public class OrganisationAuthorizationHandlerTest : OrganisationAuthorizationHandler
{
  [Fact]
  public async Task HandleRequirementAsync_Should()
  {
    var roleAdmin = new OrganisationRole(1, "test_admin", true, true, true, true, true);
    var roleUserAdmin = new OrganisationRole(1, "test_admin", false, false, true, true, true);
    var roleUser = new OrganisationRole(1, "test_normaluser", false, false, false, false, false);

    var req = new OrganisationAuthorizationRequirement
    {
      Name = "userReq", EvaluationFunction = r => r.UserWrite && r.UserRead && r.UserDelete
    };

    var authContextMock_Admin = new Mock<AuthorizationHandlerContext>(
      Enumerable.Empty<IAuthorizationRequirement>().Append(req),
      new ClaimsPrincipal(),
      roleAdmin
    );
    authContextMock_Admin.Setup(a => a.User).Returns(new ClaimsPrincipal());
    await HandleRequirementAsync(authContextMock_Admin.Object, req, roleAdmin);

    var authContextMock_UserAdmin = new Mock<AuthorizationHandlerContext>(
      Enumerable.Empty<IAuthorizationRequirement>().Append(req),
      new ClaimsPrincipal(),
      roleUserAdmin
    );
    authContextMock_UserAdmin.Setup(a => a.User).Returns(new ClaimsPrincipal());
    await HandleRequirementAsync(authContextMock_UserAdmin.Object, req, roleUserAdmin);

    var authContextMock_User = new Mock<AuthorizationHandlerContext>(
      Enumerable.Empty<IAuthorizationRequirement>().Append(req),
      new ClaimsPrincipal(),
      roleUser
    );
    authContextMock_User.Setup(a => a.User).Returns(new ClaimsPrincipal());
    await HandleRequirementAsync(authContextMock_User.Object, req, roleUser);

    authContextMock_Admin.Verify(a => a.Succeed(req), Times.Once());
    authContextMock_Admin.Verify(a => a.User, Times.Once());
    authContextMock_Admin.VerifyNoOtherCalls();

    authContextMock_UserAdmin.Verify(a => a.Succeed(req), Times.Once());
    authContextMock_UserAdmin.Verify(a => a.User, Times.Once());
    authContextMock_UserAdmin.VerifyNoOtherCalls();

    authContextMock_User.Verify(a => a.User, Times.Once());
    authContextMock_User.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task HandleRequirementAsync_SuperAdmin_Should()
  {
    var req = new OrganisationAuthorizationRequirement
    {
      Name = "userReq", EvaluationFunction = r => r.UserWrite && r.UserRead && r.UserDelete
    };

    var superAdminMock = new Mock<ClaimsPrincipal>();
    superAdminMock.Setup(c => c.IsInRole("superAdmin")).Returns(true);
    var authContextMock_SuperAdmin = new Mock<AuthorizationHandlerContext>(
      Enumerable.Empty<IAuthorizationRequirement>().Append(req),
      superAdminMock.Object,
      null
    );
    authContextMock_SuperAdmin.Setup(a => a.User).Returns(superAdminMock.Object);
    await HandleRequirementAsync(authContextMock_SuperAdmin.Object, req, null);

    superAdminMock.Verify(c => c.IsInRole("superAdmin"), Times.Once);
    superAdminMock.VerifyNoOtherCalls();
    authContextMock_SuperAdmin.Verify(a => a.Succeed(req), Times.Once());
    authContextMock_SuperAdmin.Verify(a => a.User, Times.Once());
    authContextMock_SuperAdmin.VerifyNoOtherCalls();
  }
}

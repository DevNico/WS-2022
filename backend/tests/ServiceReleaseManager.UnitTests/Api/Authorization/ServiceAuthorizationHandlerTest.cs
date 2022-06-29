using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Moq;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.ServiceAggregate;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Api.Authorization;

public class ServiceAuthorizationHandlerTest : ServiceAuthorizationHandler
{

  [Fact]
  public async Task HandleRequirementAsync_Should()
  {
    var roleAdmin = new ServiceRole("admin", true, true, true, true, true);
    var roleDev = new ServiceRole("dev", true, false, false, false, false);
    var roleUser = new ServiceRole("user", false, false, false, false, false);

    var req = new ServiceAuthorizationRequirement() { Name = "releaseCreate", EvaluationFunction = r => r.ReleaseCreate };

    var authContextMock_Admin = new Mock<AuthorizationHandlerContext>(Enumerable.Empty<IAuthorizationRequirement>().Append(req), new ClaimsPrincipal(), roleAdmin);
    authContextMock_Admin.Setup(a => a.User).Returns(new ClaimsPrincipal());
    await HandleRequirementAsync(authContextMock_Admin.Object, req, roleAdmin);

    var authContextMock_UserAdmin = new Mock<AuthorizationHandlerContext>(Enumerable.Empty<IAuthorizationRequirement>().Append(req), new ClaimsPrincipal(), roleDev);
    authContextMock_UserAdmin.Setup(a => a.User).Returns(new ClaimsPrincipal());
    await HandleRequirementAsync(authContextMock_UserAdmin.Object, req, roleDev);

    var authContextMock_User = new Mock<AuthorizationHandlerContext>(Enumerable.Empty<IAuthorizationRequirement>().Append(req), new ClaimsPrincipal(), roleUser);
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
    var req = new ServiceAuthorizationRequirement() { Name = "releaseCreate", EvaluationFunction = r => r.ReleaseCreate };

    var superAdminMock = new Mock<ClaimsPrincipal>();
    superAdminMock.Setup(c => c.IsInRole("superAdmin")).Returns(true);
    var authContextMock_SuperAdmin = new Mock<AuthorizationHandlerContext>(Enumerable.Empty<IAuthorizationRequirement>().Append(req), superAdminMock.Object, null);
    authContextMock_SuperAdmin.Setup(a => a.User).Returns(superAdminMock.Object);
    await HandleRequirementAsync(authContextMock_SuperAdmin.Object, req, null);

    superAdminMock.Verify(c => c.IsInRole("superAdmin"), Times.Once);
    superAdminMock.VerifyNoOtherCalls();
    authContextMock_SuperAdmin.Verify(a => a.Succeed(req), Times.Once());
    authContextMock_SuperAdmin.Verify(a => a.User, Times.Once());
    authContextMock_SuperAdmin.VerifyNoOtherCalls();
  }
}

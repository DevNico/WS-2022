using System.Security.Claims;
using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Moq;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Api.Authorization.Operations.Organisation;
using ServiceReleaseManager.Api.Endpoints.OrganisationUsers;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Api.Endpoints.OrganisationUsers;

public class OrganisationUsersCreateTest
{

  private readonly Mock<IOrganisationUserService> _organisationUserServiceMock;
  private readonly Mock<IKeycloakClient> _keycloakClientMock;
  private readonly Mock<ILogger<Create>> _loggerMock;
  private readonly Mock<IOrganisationService> _organisationServiceMock;
  private readonly Mock<IReadRepository<OrganisationRole>> _organisationRoleRepositoryMock;
  private readonly Mock<IServiceManagerAuthorizationService> _authorizationServiceMock;

  public OrganisationUsersCreateTest()
  {
    _organisationUserServiceMock = new Mock<IOrganisationUserService>();
    _keycloakClientMock = new Mock<IKeycloakClient>();
    _loggerMock = new Mock<ILogger<Create>>();
    _organisationServiceMock = new Mock<IOrganisationService>();
    _organisationRoleRepositoryMock = new Mock<IReadRepository<OrganisationRole>>();
    _authorizationServiceMock = new Mock<IServiceManagerAuthorizationService>();
  }

  [Fact]
  public async Task OrganisationUsersCreateShould()
  {
    var cancellationToken = new CancellationToken();

    int orgId = 123;
    var organisation = new Organisation("test") { Id = orgId };

    int roleId = 456;
    var role = new OrganisationRole(orgId, "test", true, true, true, true, true) { Id = roleId };

    var userId = "aaa";
    var userEmail = "a@a.a";
    var user = new OrganisationUser(userId, userEmail, "", "", role, orgId);
    var keycloakUser = new KeycloakUserRecord(userId, "", "", "", userEmail, true, true, true, new List<string>(), DateTime.Now, DateTime.MinValue);

    var request = new CreateOrganisationUserRequest() { OrganisationId = orgId, RoleId = role.Id, Email = userEmail};

    _authorizationServiceMock.Setup(m => m.EvaluateOrganisationAuthorization(It.IsAny<ClaimsPrincipal>(), It.IsAny<int>(),
      It.IsAny<OrganisationAuthorizationRequirement>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);

    _organisationServiceMock.Setup(m => m.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Result<Organisation>(organisation));

    _organisationRoleRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(role);

    _keycloakClientMock.Setup(m => m.GetUserByEmail(It.IsAny<string>())).ReturnsAsync(keycloakUser);

    _organisationUserServiceMock.Setup(m => m.Create(It.IsAny<OrganisationUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Result<OrganisationUser>(user));

    var create = new Create(_organisationUserServiceMock.Object, _keycloakClientMock.Object,      _loggerMock.Object, 
      _organisationServiceMock.Object, _organisationRoleRepositoryMock.Object, _authorizationServiceMock.Object);
    
    var result = await create.HandleAsync(request, cancellationToken);

    Assert.NotNull(result.Result);

    _authorizationServiceMock.Verify(m => m.EvaluateOrganisationAuthorization(null, orgId, OrganisationUserOperations.OrganisationUser_Create, cancellationToken), Times.Once);
    _authorizationServiceMock.VerifyNoOtherCalls();

    _organisationServiceMock.Verify(m => m.GetById(orgId, cancellationToken), Times.Once);
    _organisationServiceMock.VerifyNoOtherCalls();

    _organisationRoleRepositoryMock.Verify(m => m.GetByIdAsync(roleId, cancellationToken), Times.Once);
    _organisationRoleRepositoryMock.VerifyNoOtherCalls();

    _keycloakClientMock.Verify(m => m.GetUserByEmail(userEmail), Times.Exactly(2));
    _keycloakClientMock.VerifyNoOtherCalls();

    _organisationUserServiceMock.Verify(
      m => m.Create(It.Is<OrganisationUser>(u => u.UserId == user.UserId && u.Email == user.Email), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _organisationUserServiceMock.VerifyNoOtherCalls();
  }
}

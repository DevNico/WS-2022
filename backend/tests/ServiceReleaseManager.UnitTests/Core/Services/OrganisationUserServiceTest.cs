using Ardalis.Result;
using Moq;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.Core.Services;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Services;

public class OrganisationUserServiceTest
{
  private Mock<IOrganisationService> _organisationServiceMock;
  private Mock<IRepository<OrganisationUser>> _organisationUserRepositoryMock;

  public OrganisationUserServiceTest()
  {
    _organisationServiceMock = new Mock<IOrganisationService>();
    _organisationUserRepositoryMock = new Mock<IRepository<OrganisationUser>>();
  }

  [Fact]
  public async Task CreateShould()
  {
    var user = getExampleUser(123, "aaa", 234);

    var cancellationToken = new CancellationToken();

    _organisationUserRepositoryMock.Setup(m => m.AddAsync(It.IsAny<OrganisationUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

    var organisationService = new OrganisationUserService(_organisationUserRepositoryMock.Object, _organisationServiceMock.Object);
    var result = await organisationService.Create(user, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);

    _organisationUserRepositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<OrganisationUserByOrganisationIdAndEmailSpec>(),
      It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _organisationUserRepositoryMock.Verify(m => m.AddAsync(user, cancellationToken), Times.Once);
    _organisationUserRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _organisationUserRepositoryMock.VerifyNoOtherCalls();

    _organisationServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByIdShould()
  {
    var userId = 345;
    var user = getExampleUser(123, "aaa", userId);

    var cancellationToken = new CancellationToken();

    _organisationUserRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

    var organisationService = new OrganisationUserService(_organisationUserRepositoryMock.Object, _organisationServiceMock.Object);
    var result = await organisationService.GetById(userId, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);

    _organisationUserRepositoryMock.Verify(m => m.GetByIdAsync(userId, cancellationToken), Times.Once);
    _organisationUserRepositoryMock.VerifyNoOtherCalls();

    _organisationServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByEmailShould()
  {
    var userId = 345;
    var email = "p@p.p";
    var user = getExampleUser(123, "aaa", userId, email);

    var cancellationToken = new CancellationToken();

    _organisationUserRepositoryMock.Setup(m => m.ListAsync(It.IsAny<OrganisationUsersByEmailSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<OrganisationUser> { user });

    var organisationService = new OrganisationUserService(_organisationUserRepositoryMock.Object, _organisationServiceMock.Object);
    var result = await organisationService.GetByEmail(email, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);

    _organisationUserRepositoryMock.Verify(m => m.ListAsync(It.IsAny<OrganisationUsersByEmailSpec>(),
      It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _organisationUserRepositoryMock.VerifyNoOtherCalls();

    _organisationServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task ListByOrganisationRouteNameShould()
  {
    var orgId = 123;
    var orgName = "org";
    var user0 = getExampleUser(orgId, "aaa", 987);
    var user1 = getExampleUser(orgId, "bbb", 876);

    var organisation = new Organisation(orgName) { Id = orgId };
    organisation.Users.Add(user0);
    organisation.Users.Add(user1);

    var cancellationToken = new CancellationToken();

    _organisationServiceMock.Setup(m => m.GetByRouteName(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Result<Organisation>(organisation));
    _organisationUserRepositoryMock.Setup(m => m.ListAsync(It.IsAny<OrganisationUserByOrganisationIdSpec>(),
      It.IsAny<CancellationToken>())).ReturnsAsync(new List<OrganisationUser> { user0, user1 });

    var organisationService = new OrganisationUserService(_organisationUserRepositoryMock.Object, _organisationServiceMock.Object);
    var result = await organisationService.ListByOrganisationRouteName(orgName, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.NotEmpty(result.Value);

    _organisationServiceMock.Verify(m => m.GetByRouteName(orgName, cancellationToken), Times.Once);
    _organisationServiceMock.VerifyNoOtherCalls();

    _organisationUserRepositoryMock.Verify(m => m.ListAsync(It.IsAny<OrganisationUserByOrganisationIdSpec>(),
      It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _organisationUserRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task DeleteShould()
  {
    var userId = 345;
    var user = getExampleUser(123, "aaa", userId);

    var cancellationToken = new CancellationToken();

    _organisationUserRepositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<OrganisationUserByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(user);

    var organisationService = new OrganisationUserService(_organisationUserRepositoryMock.Object, _organisationServiceMock.Object);
    var result = await organisationService.Delete(userId, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Null(result.Value);

    _organisationUserRepositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<OrganisationUserByIdSpec>(), It.Is<CancellationToken>(c => c ==cancellationToken)), Times.Once);
    _organisationUserRepositoryMock.Verify(m => m.UpdateAsync(user, cancellationToken), Times.Once);
    _organisationUserRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _organisationUserRepositoryMock.VerifyNoOtherCalls();

    _organisationServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task getUsersShould()
  {
    var orgId = 123;
    var user0 = getExampleUser(orgId, "aaa", 987);
    var user1 = getExampleUser(orgId, "bbb", 876);

    var cancellationToken = new CancellationToken();

    _organisationUserRepositoryMock.Setup(m => m.ListAsync(It.IsAny<OrganisationUserByOrganisationIdSpec>(),
      It.IsAny<CancellationToken>())).ReturnsAsync(new List<OrganisationUser> { user0, user1 });

    var organisationService = new OrganisationUserService(_organisationUserRepositoryMock.Object, _organisationServiceMock.Object);
    var result = await organisationService.GetUsers(orgId, cancellationToken);

    Assert.NotEmpty(result);

    _organisationUserRepositoryMock.Verify(m => m.ListAsync(It.IsAny<OrganisationUserByOrganisationIdSpec>(),
      It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _organisationUserRepositoryMock.VerifyNoOtherCalls();

    _organisationServiceMock.VerifyNoOtherCalls();
  }

  private OrganisationUser getExampleUser(int orgId, string userUserId, int userId, string email = "a@a.a")
  {
    var role = new OrganisationRole(orgId, "test", true, true, true, true, true);
    return new OrganisationUser(userUserId, email, "", "", role, orgId) { Id = userId};
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    var user = getExampleUser(123, "aaa");

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

  private OrganisationUser getExampleUser(int orgId, string userId)
  {
    var role = new OrganisationRole(orgId, "test", true, true, true, true, true);
    return new OrganisationUser(userId, "", "", "", role, orgId);
  }
}

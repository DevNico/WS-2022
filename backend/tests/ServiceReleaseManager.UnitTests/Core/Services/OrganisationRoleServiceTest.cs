using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.Services;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Services;

public class OrganisationRoleServiceTest
{
  private Mock<IRepository<Organisation>> _organisationRepositoryMock;
  private Mock<IRepository<OrganisationRole>> _roleServiceMock;
  private Mock<IRepository<OrganisationUser>> _userServiceMock;

  public OrganisationRoleServiceTest()
  {
    _organisationRepositoryMock = new Mock<IRepository<Organisation>>();
    _roleServiceMock = new Mock<IRepository<OrganisationRole>>();
    _userServiceMock = new Mock<IRepository<OrganisationUser>>();
  }

  [Fact]
  public async Task CreateShould()
  {
    var organisation = new Organisation("test") { Id = 123 };
    var role = new OrganisationRole(organisation.Id, "", true, true, true, true, true);

    var cancellationToken = new CancellationToken();

    _organisationRepositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<OrganisationByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(organisation);

    _roleServiceMock.Setup(m => m.AddAsync(It.IsAny<OrganisationRole>(), It.IsAny<CancellationToken>())).ReturnsAsync(role);

    var organisationRoleService = new OrganisationRoleService(_organisationRepositoryMock.Object, _roleServiceMock.Object, _userServiceMock.Object);
    var result = await organisationRoleService.Create(role, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);

    _organisationRepositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<OrganisationByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)));
    _organisationRepositoryMock.Verify(m => m.UpdateAsync(organisation, cancellationToken), Times.Once);
    _organisationRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _organisationRepositoryMock.VerifyNoOtherCalls();

    _roleServiceMock.Verify(m => m.AddAsync(role, cancellationToken), Times.Once);
    _roleServiceMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _roleServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task DeleteShould()
  {
    var orgId = 123;
    var roleId = 987;
    var role = new OrganisationRole(orgId, "", true, true, true, true, true) { Id = roleId};
    var organisation = new Organisation("test") { Id = orgId, Roles = new List<OrganisationRole> { role } };

    var cancellationToken = new CancellationToken();

    _userServiceMock.Setup(m => m.CountAsync(It.IsAny<OrganisationUsersByRoleIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(0);

    _organisationRepositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<OrganisationByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(organisation);

    _roleServiceMock.Setup(m => m.GetBySpecAsync(It.IsAny<OrganisationRoleByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(role);

    var organisationRoleService = new OrganisationRoleService(_organisationRepositoryMock.Object, _roleServiceMock.Object, _userServiceMock.Object);
    var result = await organisationRoleService.Delete(roleId, cancellationToken);

    Assert.True(result.IsSuccess);

    _userServiceMock.Verify(m => m.CountAsync(It.IsAny<OrganisationUsersByRoleIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _userServiceMock.VerifyNoOtherCalls();

    _organisationRepositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<OrganisationByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _organisationRepositoryMock.Verify(m => m.UpdateAsync(organisation, cancellationToken), Times.Once);
    _organisationRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _organisationRepositoryMock.VerifyNoOtherCalls();

    _roleServiceMock.Verify(m => m.GetBySpecAsync(It.IsAny<OrganisationRoleByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _roleServiceMock.Verify(m => m.DeleteAsync(role, cancellationToken), Times.Once);
    _roleServiceMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _roleServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task ListByOrganisationRouteNameShould()
  {
    var orgName = "test";
    var organisation = new Organisation(orgName) { Id = 123 };
    var role0 = new OrganisationRole(organisation.Id, "0", true, true, true, true, true);
    var role1 = new OrganisationRole(organisation.Id, "1", true, true, true, true, true);
    organisation.Roles.Add(role0);
    organisation.Roles.Add(role1);

    var cancellationToken = new CancellationToken();

    _organisationRepositoryMock.Setup(m => m.GetBySpecAsync<IEnumerable<OrganisationRole>>(It.IsAny<OrganisationRolesByOrganisationRouteNameSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(organisation.Roles);

    var organisationRoleService = new OrganisationRoleService(_organisationRepositoryMock.Object, _roleServiceMock.Object, _userServiceMock.Object);
    var result = await organisationRoleService.ListByOrganisationRouteName(orgName, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);

    _organisationRepositoryMock.Verify(m => m.GetBySpecAsync<IEnumerable<OrganisationRole>>(It.IsAny<OrganisationRolesByOrganisationRouteNameSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)));
    _organisationRepositoryMock.VerifyNoOtherCalls();

    _userServiceMock.VerifyNoOtherCalls();
    _roleServiceMock.VerifyNoOtherCalls();
  }
}

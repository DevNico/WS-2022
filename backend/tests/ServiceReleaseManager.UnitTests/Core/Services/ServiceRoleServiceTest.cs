using Moq;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.Core.Services;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Services;

public class ServiceRoleServiceTest
{
  private readonly Organisation _organisation;
  private readonly Mock<IOrganisationService> _organisationServiceMock;
  private readonly Mock<IRepository<ServiceRole>> _repositoryMock;
  private readonly ServiceRole _serviceRole0;

  public ServiceRoleServiceTest()
  {
    _repositoryMock = new Mock<IRepository<ServiceRole>>();
    _organisationServiceMock = new Mock<IOrganisationService>();

    _organisation = new Organisation("Organisation");
    _serviceRole0 = GetServiceRole();
  }

  [Fact]
  public async Task CreateShould()
  {
    var cancellationToken = new CancellationToken();
    var serviceRole = GetServiceRole();

    _repositoryMock.Setup(m => m.AddAsync(It.IsAny<ServiceRole>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(serviceRole);
    _organisationServiceMock.Setup(r => r.GetById(0, cancellationToken))
                            .ReturnsAsync(_organisation);

    var serviceService = GetServiceRoleService();
    var result = await serviceService.Create(_organisation.Id, serviceRole, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(serviceRole, result.Value);

    _repositoryMock.Verify(m => m.AddAsync(It.IsAny<ServiceRole>(), cancellationToken), Times.Once);
    _repositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _organisationServiceMock.Verify(m => m.Update(_organisation, cancellationToken), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task DeleteShould()
  {
    var serviceService = GetServiceRoleService();
    var cancellationToken = new CancellationToken();

    _repositoryMock
     .Setup(m => m.GetBySpecAsync(It.IsAny<ServiceRoleByIdSpec>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(_serviceRole0);

    var result = await serviceService.Deactivate(0, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Null(result.Value);

    _repositoryMock.Verify(
      m => m.GetBySpecAsync(
        It.IsAny<ServiceRoleByIdSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _repositoryMock.Verify(m => m.UpdateAsync(_serviceRole0, cancellationToken), Times.Once);
    _repositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task DeleteInvalidShould()
  {
    var serviceService = GetServiceRoleService();
    var cancellationToken = new CancellationToken();

    var result = await serviceService.Deactivate(1, cancellationToken);

    Assert.False(result.IsSuccess);
  }

  [Fact]
  public async Task GetByIdShould()
  {
    var serviceService = GetServiceRoleService();
    var cancellationToken = new CancellationToken();

    _repositoryMock
     .Setup(m => m.GetBySpecAsync(It.IsAny<ServiceRoleByIdSpec>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(_serviceRole0);

    var result = await serviceService.GetById(0, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(_serviceRole0, result.Value);

    _repositoryMock.Verify(
      m => m.GetBySpecAsync(
        It.IsAny<ServiceRoleByIdSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _repositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByRouteNameShould()
  {
    var serviceService = GetServiceRoleService();
    var cancellationToken = new CancellationToken();

    _repositoryMock
     .Setup(m => m.ListAsync(It.IsAny<ServiceRoleByNameSpec>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new List<ServiceRole> { _serviceRole0 });

    var result = await serviceService.GetByName(_serviceRole0.Name, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(1, result.Value.Count);

    _repositoryMock.Verify(
      m => m.ListAsync(
        It.IsAny<ServiceRoleByNameSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _repositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByOrganisationUserIdShould()
  {
    var serviceService = GetServiceRoleService();
    var cancellationToken = new CancellationToken();

    _repositoryMock
     .Setup(
        m => m.ListAsync(
          It.IsAny<ServiceRolesByOrganisationIdSpec>(),
          It.IsAny<CancellationToken>()
        )
      ).ReturnsAsync(new List<ServiceRole> { _serviceRole0 });

    var result = await serviceService.GetByOrganisationId(0, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(1, result.Value.Count);

    _repositoryMock.Verify(
      m => m.ListAsync(
        It.IsAny<ServiceRolesByOrganisationIdSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _repositoryMock.VerifyNoOtherCalls();
  }

  private static ServiceRole GetServiceRole()
  {
    return new ServiceRole(0, "Test", true, true, true, true, true);
  }

  private ServiceRoleService GetServiceRoleService()
  {
    return new ServiceRoleService(_repositoryMock.Object, _organisationServiceMock.Object);
  }
}

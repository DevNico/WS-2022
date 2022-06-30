using Moq;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.Core.Services;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Services;

public class ReleaseServiceTest
{
  private readonly Mock<IRepository<Release>> _repositoryMock;
  private readonly Mock<IRepository<Service>> _serviceRepositoryMock;

  public ReleaseServiceTest()
  {
    _repositoryMock = new Mock<IRepository<Release>>();
    _serviceRepositoryMock = new Mock<IRepository<Service>>();
  }

  [Fact]
  public async Task TestReleaseServiceAsync()
  {
    int serviceId = 123;
    var release0 = new Release("1.0", "", serviceId);
    var release1 = new Release("1.1", "", serviceId);

    var cancellationToken = new CancellationToken();

    _serviceRepositoryMock.Setup(m => m.CountAsync(It.IsAny<ServiceByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(1);

    _repositoryMock.Setup(m => m.ListAsync(It.IsAny<ReleaseByServiceIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<Release> { release0, release1 });

    var releaseService = new ReleaseService(_repositoryMock.Object, _serviceRepositoryMock.Object);
    var result = await releaseService.GetByServiceId(serviceId, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.NotNull(result.Value);

    _serviceRepositoryMock.Verify(m => m.CountAsync(It.IsAny<ServiceByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _serviceRepositoryMock.VerifyNoOtherCalls();

    _repositoryMock.Verify(m => m.ListAsync(It.IsAny<ReleaseByServiceIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }
}

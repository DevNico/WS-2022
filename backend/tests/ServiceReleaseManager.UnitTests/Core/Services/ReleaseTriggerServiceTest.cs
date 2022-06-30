using Moq;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.Services;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Services;

public class ReleaseTriggerServiceTest
{
  private readonly Mock<IRepository<ReleaseTrigger>> _repositoryMock;
  public ReleaseTriggerServiceTest()
  {
    _repositoryMock = new Mock<IRepository<ReleaseTrigger>>();
  }

  [Fact]
  public async Task CreateShould()
  {
    var orgId = 123;
    var serviceId = 456;
    var trigger = getExampleTrigger(orgId, serviceId);

    var cancellationToken = new CancellationToken();

    _repositoryMock.Setup(m => m.AddAsync(It.IsAny<ReleaseTrigger>(), It.IsAny<CancellationToken>())).ReturnsAsync(trigger);

    var releaseTriggerService = new ReleaseTriggerService(_repositoryMock.Object);
    var result = await releaseTriggerService.Create(trigger, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(trigger, result.Value);

    _repositoryMock.Verify(m => m.AddAsync(trigger, cancellationToken), Times.Once);
    _repositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task DeleteShould()
  {
    var orgId = 123;
    var serviceId = 456;
    var triggerId = 789;
    var trigger = getExampleTrigger(orgId, serviceId, triggerId);

    var cancellationToken = new CancellationToken();

    _repositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<ReleaseTriggerByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(trigger);

    var releaseTriggerService = new ReleaseTriggerService(_repositoryMock.Object);
    var result = await releaseTriggerService.Delete(triggerId, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Null(result.Value);

    _repositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<ReleaseTriggerByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _repositoryMock.Verify(m => m.DeleteAsync(trigger, cancellationToken), Times.Once);
    _repositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByIdShould()
  {
    var orgId = 123;
    var serviceId = 456;
    var triggerId = 789;
    var trigger = getExampleTrigger(orgId, serviceId, triggerId);

    var cancellationToken = new CancellationToken();

    _repositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<ReleaseTriggerByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(trigger);

    var releaseTriggerService = new ReleaseTriggerService(_repositoryMock.Object);
    var result = await releaseTriggerService.GetById(triggerId, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(trigger, result.Value);

    _repositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<ReleaseTriggerByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }

  private ReleaseTrigger getExampleTrigger(int orgId, int serviceId, int triggerId = 987)
  {
    var organisation = new Organisation("org") { Id = orgId};
    var template = new ServiceTemplate("temp", "", "", orgId);
    var service = new Service("ser", "", template, organisation) { Id = serviceId };
    return new ReleaseTrigger("trig", "", "", service);
  }
}

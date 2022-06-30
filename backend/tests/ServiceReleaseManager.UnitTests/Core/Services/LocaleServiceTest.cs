using Ardalis.Result;
using Moq;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.Core.Services;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Services;

public class LocaleServiceTest
{

  private readonly Mock<IRepository<Locale>> _localeRepositoryMock;
  private readonly Mock<IServiceService> _serviceServiceMock;

  public LocaleServiceTest()
  {
    _localeRepositoryMock = new Mock<IRepository<Locale>>();
    _serviceServiceMock = new Mock<IServiceService>();
  }

  [Fact]
  public async Task CreateShould()
  {
    var orgId = 987;
    var serviceId = 123;
    var service = getExampleService(orgId, serviceId, "test");
    var locale = new Locale("tag", false, serviceId);

    var cancellationToken = new CancellationToken();

    _serviceServiceMock.Setup(m => m.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Result<Service>(service));
    _localeRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Locale>(), It.IsAny<CancellationToken>())).ReturnsAsync(locale);

    var localeService = new LocaleService(_serviceServiceMock.Object, _localeRepositoryMock.Object);
    var result = await localeService.Create(locale, cancellationToken);

    Assert.Equal(ResultStatus.Ok, result.Status);
    Assert.NotNull(result.Value);

    _serviceServiceMock.Verify(m => m.GetById(serviceId, cancellationToken), Times.Once);
    _serviceServiceMock.VerifyNoOtherCalls();

    _localeRepositoryMock.Verify(m => m.AddAsync(locale, cancellationToken), Times.Once);
    _localeRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _localeRepositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<LocaleByTagSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _localeRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByIdShould()
  {
    var serviceId = 123;
    var localeId = 345;
    var locale = new Locale("tag", false, serviceId) { Id = localeId };

    var cancellationToken = new CancellationToken();

    _localeRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(locale);

    var localeService = new LocaleService(_serviceServiceMock.Object, _localeRepositoryMock.Object);
    var result = await localeService.GetById(localeId, cancellationToken);

    Assert.Equal(ResultStatus.Ok, result.Status);
    Assert.NotNull(result.Value);

    _localeRepositoryMock.Verify(m => m.GetByIdAsync(localeId, cancellationToken), Times.Once);
    _localeRepositoryMock.VerifyNoOtherCalls();

    _serviceServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task ListByServiceRouteNameShould()
  {
    var serviceId = 123;
    var serviceRouteName = "test";
    var service = getExampleService(100, serviceId, serviceRouteName);
    var locale0 = new Locale("tag0", false, serviceId) { Id = 1 };
    var locale1 = new Locale("tag1", false, serviceId) { Id = 2 };

    var cancellationToken = new CancellationToken();

    _serviceServiceMock.Setup(m => m.GetByRouteName(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Result<Service>(service));

    _localeRepositoryMock.Setup(m => m.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Locale> { locale0, locale1});

    var localeService = new LocaleService(_serviceServiceMock.Object, _localeRepositoryMock.Object);
    var result = await localeService.ListByServiceRouteName(serviceRouteName, cancellationToken);

    Assert.Equal(ResultStatus.Ok, result.Status);
    Assert.NotNull(result.Value);

    _serviceServiceMock.Verify(m => m.GetByRouteName(serviceRouteName, cancellationToken), Times.Once());
    _serviceServiceMock.VerifyNoOtherCalls();

    _localeRepositoryMock.Verify(m => m.ListAsync(cancellationToken), Times.Once);
    _localeRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task DeleteShould()
  {
    var serviceId = 123;
    var localeId = 345;
    var locale = new Locale("tag", false, serviceId) { Id = localeId };

    var cancellationToken = new CancellationToken();

    _localeRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(locale);

    var localeService = new LocaleService(_serviceServiceMock.Object, _localeRepositoryMock.Object);
    var result = await localeService.Delete(localeId, cancellationToken);

    Assert.Equal(ResultStatus.Ok, result.Status);
    Assert.Null(result.Value);

    _localeRepositoryMock.Verify(m => m.GetByIdAsync(localeId, cancellationToken), Times.Once);
    _localeRepositoryMock.Verify(m => m.DeleteAsync(locale, cancellationToken), Times.Once);
    _localeRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _localeRepositoryMock.VerifyNoOtherCalls();

    _serviceServiceMock.VerifyNoOtherCalls();
  }

  private Service getExampleService(int orgId, int serviceId, string serviceName)
  {
    var organisation = new Organisation("org") { Id = orgId };
    var template = new ServiceTemplate("template", "", "", orgId);
    return new Service(serviceName, "", template, organisation) { Id = serviceId };
  }
}

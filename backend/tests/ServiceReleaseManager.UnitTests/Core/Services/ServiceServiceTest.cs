using Moq;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Specifications;
using ServiceReleaseManager.Core.Services;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Services;

public class ServiceServiceTest
{
  private readonly Mock<IRepository<Service>> _repositoryMock;
  private readonly Mock<IRepository<Organisation>> _organisationRepositoryMock;
  private readonly Mock<IRepository<ServiceTemplate>> _serviceTemplateRepositoryMock;
  private readonly Organisation _organisation;
  private readonly ServiceTemplate _serviceTemplate;
  private readonly Service _service0;

  public ServiceServiceTest()
  {
    _repositoryMock = new Mock<IRepository<Service>>();
    _organisationRepositoryMock = new Mock<IRepository<Organisation>>();
    _serviceTemplateRepositoryMock = new Mock<IRepository<ServiceTemplate>>();

    _organisation = new Organisation("Organisation");
    _serviceTemplate = new ServiceTemplate("Template", "{}", "{}", 0) { Organisation = _organisation };
    _service0 = GetService(0);
  }

  [Fact]
  public async Task CreateShould()
  {
    var cancellationToken = new CancellationToken();
    var service = GetService(1);

    _repositoryMock.Setup(m => m.AddAsync(It.IsAny<Service>(), It.IsAny<CancellationToken>())).ReturnsAsync(service);
    _serviceTemplateRepositoryMock.Setup(r => r.GetBySpecAsync(It.IsAny<ServiceTemplateByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(_serviceTemplate);
    _organisationRepositoryMock.Setup(r => r.GetBySpecAsync(It.IsAny<OrganisationByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(_organisation);
    
    var serviceService = new ServiceService(_repositoryMock.Object, _serviceTemplateRepositoryMock.Object);
    var result = await serviceService.Create(service.Name, service.Description, service.ServiceTemplateId, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(service, result.Value);

    _repositoryMock.Verify(m => m.AddAsync(It.IsAny<Service>(), cancellationToken), Times.Once);
    _repositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }
  
  [Fact]
  public async Task CreateWithoutTemplateShould()
  {
    var cancellationToken = new CancellationToken();
    var service = GetService(1);
    service.ServiceTemplate = null!;
    service.ServiceTemplateId = 0;

    _repositoryMock.Setup(m => m.AddAsync(It.IsAny<Service>(), It.IsAny<CancellationToken>())).ReturnsAsync(service);
    _organisationRepositoryMock.Setup(r => r.GetBySpecAsync(It.IsAny<OrganisationByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(_organisation);

    var serviceService = GetServiceService();
    var result = await serviceService.Create(service.Name, service.Description, service.ServiceTemplateId, cancellationToken);

    Assert.False(result.IsSuccess);
    
    _repositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task DeleteShould()
  {
    var serviceService = GetServiceService();
    var cancellationToken = new CancellationToken();

    _repositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<ServiceByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(_service0);
    
    var result = await serviceService.Deactivate(0, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Null(result.Value);

    _repositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<ServiceByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _repositoryMock.Verify(m => m.UpdateAsync(_service0, cancellationToken), Times.Once);
    _repositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByIdShould()
  {
    var serviceService = GetServiceService();
    var cancellationToken = new CancellationToken();

    _repositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<ServiceByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(_service0);
    
    var result = await serviceService.GetById(0, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(_service0, result.Value);

    _repositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<ServiceByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }
  
  [Fact]
  public async Task GetByRouteNameShould()
  {
    var serviceService = GetServiceService();
    var cancellationToken = new CancellationToken();

    _repositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<ServiceByRouteNameSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(_service0);
    
    var result = await serviceService.GetByRouteName(_service0.RouteName, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(_service0, result.Value);

    _repositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<ServiceByRouteNameSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }
  
  [Fact]
  public async Task GetByNameAndOrganisationIdShould()
  {
    var serviceService = GetServiceService();
    var cancellationToken = new CancellationToken();

    _repositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<ServiceByNameAndOrganisationIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(_service0);
    
    var result = await serviceService.GetByNameAndOrganisationId(_service0.Name, _organisation.Id, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(_service0, result.Value);

    _repositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<ServiceByNameAndOrganisationIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }
  
  [Fact]
  public async Task GetByOrganisationUserIdShould()
  {
    var serviceService = GetServiceService();
    var cancellationToken = new CancellationToken();

    _repositoryMock.Setup(m => m.ListAsync(It.IsAny<ServicesByOrganisationUserIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<Service>() { _service0 });
    
    var result = await serviceService.GetByOrganisationUserId(0, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(1, result.Value.Count);

    _repositoryMock.Verify(m => m.ListAsync(It.IsAny<ServicesByOrganisationUserIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _repositoryMock.VerifyNoOtherCalls();
  }
  
  [Fact]
  public async Task ServiceTemplateShould()
  {
    var serviceService = GetServiceService();
    var cancellationToken = new CancellationToken();

    _serviceTemplateRepositoryMock.Setup(m => m.GetBySpecAsync(It.IsAny<ServiceTemplateByIdSpec>(), It.IsAny<CancellationToken>())).ReturnsAsync(_serviceTemplate);
    
    var result = await serviceService.GetServiceTemplate(0, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(_serviceTemplate, result.Value);

    _serviceTemplateRepositoryMock.Verify(m => m.GetBySpecAsync(It.IsAny<ServiceTemplateByIdSpec>(), It.Is<CancellationToken>(c => c == cancellationToken)), Times.Once);
    _serviceTemplateRepositoryMock.VerifyNoOtherCalls();
  }

  private Service GetService(int id)
  {
    return new Service($"Service {id}", "Description", _serviceTemplate, _organisation) { Id = id };
  }

  private ServiceService GetServiceService()
  {
    return new ServiceService(_repositoryMock.Object, _serviceTemplateRepositoryMock.Object);
  }
}

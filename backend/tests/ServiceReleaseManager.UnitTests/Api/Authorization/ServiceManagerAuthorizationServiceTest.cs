using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Result;
using Microsoft.AspNetCore.Authorization;
using Moq;
using ServiceReleaseManager.Api.Authorization;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Api.Authorization;

public class ServiceManagerAuthorizationServiceTest
{

  private readonly Mock<IAuthorizationService> _authorizationServiceMock;
  private readonly Mock<IOrganisationService> _organisationServiceMock;
  private readonly Mock<IOrganisationUserService> _organisationUserServiceMock;
  private readonly Mock<IServiceUserService> _serviceUserServiceMock;
  private readonly Mock<IServiceService> _serviceServiceMock;
  private readonly Mock<ClaimsPrincipal> _claimsPrincipalMock;

  public ServiceManagerAuthorizationServiceTest()
  {
    _authorizationServiceMock = new Mock<IAuthorizationService>();
    _organisationServiceMock = new Mock<IOrganisationService>();
    _organisationUserServiceMock = new Mock<IOrganisationUserService>();
    _serviceUserServiceMock = new Mock<IServiceUserService>();
    _serviceServiceMock = new Mock<IServiceService>();
    _claimsPrincipalMock = new Mock<ClaimsPrincipal>();
  }

  #region OrganisationUserAurhorization
  [Fact]
  public async Task EvaluateOrganisationAuthorizationOrgIDShould()
  {
    int orgId = 12345;
    string userId = "aaa";
    var requirement = new OrganisationAuthorizationRequirement() { EvaluationFunction = _ => true };
    var organisationUser = getExampleUser(userId);
    var cancellationToken = new CancellationToken();

    setupEvaluateOrganisationAuthorization(userId, organisationUser);

    var service = getServiceManagerAuthorizationService();
    var result = await service.EvaluateOrganisationAuthorization(_claimsPrincipalMock.Object, orgId, requirement, cancellationToken);

    Assert.True(result);

    verifyEvaluateOrganisationAuthorization(orgId, organisationUser, requirement, cancellationToken);

    _authorizationServiceMock.VerifyNoOtherCalls();
    _organisationUserServiceMock.VerifyNoOtherCalls();
    _organisationServiceMock.VerifyNoOtherCalls();
    _serviceUserServiceMock.VerifyNoOtherCalls();
    _serviceServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task EvaluateOrganisationAuthorizationOrgRouteNameShould()
  {
    string orgRouteName = "test";
    int orgId = 12345;
    string userId = "aaa";
    var requirement = new OrganisationAuthorizationRequirement() { EvaluationFunction = _ => true };
    var organisationUser = getExampleUser(userId);
    var cancellationToken = new CancellationToken();
    var authResult = AuthorizationResult.Success();

    var organisation = new Organisation(orgRouteName) { Id = orgId };
   
    _organisationServiceMock.Setup(m => m.GetByRouteName(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Result<Organisation>(organisation));

    setupEvaluateOrganisationAuthorization(userId, organisationUser);

    var service = getServiceManagerAuthorizationService();
    var result = await service.EvaluateOrganisationAuthorization(_claimsPrincipalMock.Object, orgRouteName, requirement, cancellationToken);

    Assert.True(result);

    _organisationServiceMock.Verify(m => m.GetByRouteName(orgRouteName, cancellationToken), Times.Once);
    _organisationServiceMock.VerifyNoOtherCalls();

    verifyEvaluateOrganisationAuthorization(orgId, organisationUser, requirement, cancellationToken);

    _authorizationServiceMock.VerifyNoOtherCalls();
    _organisationUserServiceMock.VerifyNoOtherCalls();
    _serviceUserServiceMock.VerifyNoOtherCalls();
    _serviceServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task EvaluateOrganisationAuthorizationServiceIdShould()
  {
    int serviceId = 9876;

    int orgId = 12345;
    string userId = "aaa";
    var requirement = new OrganisationAuthorizationRequirement() { EvaluationFunction = _ => true };
    var organisationUser = getExampleUser(userId);
    var cancellationToken = new CancellationToken();
    var authResult = AuthorizationResult.Success();

    var service = getService(orgId, serviceId, "test");

    _serviceServiceMock.Setup(m => m.GetById(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Result<Service>(service));

    setupEvaluateOrganisationAuthorization(userId, organisationUser);

    var serviceManagerAuthorizationService = getServiceManagerAuthorizationService();
    var result = await serviceManagerAuthorizationService.EvaluateOrganisationAuthorizationServiceId(_claimsPrincipalMock.Object, serviceId, requirement, cancellationToken);

    Assert.True(result);


    _serviceServiceMock.Verify(m => m.GetById(serviceId, cancellationToken), Times.Once);
    _serviceServiceMock.VerifyNoOtherCalls();

    verifyEvaluateOrganisationAuthorization(orgId, organisationUser, requirement, cancellationToken);

    _authorizationServiceMock.VerifyNoOtherCalls();
    _organisationUserServiceMock.VerifyNoOtherCalls();
    _organisationServiceMock.VerifyNoOtherCalls();
    _serviceUserServiceMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task EvaluateOrganisationAuthorizationServiceRouteNameShould()
  {
    string serviceRouteName = "test-service";
    int serviceId = 9876;

    int orgId = 12345;
    string userId = "aaa";
    var requirement = new OrganisationAuthorizationRequirement() { EvaluationFunction = _ => true };
    var organisationUser = getExampleUser(userId);
    var cancellationToken = new CancellationToken();
    var authResult = AuthorizationResult.Success();

    var service = getService(orgId, serviceId, serviceRouteName);

    _serviceServiceMock.Setup(m => m.GetByRouteName(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(new Result<Service>(service));

    setupEvaluateOrganisationAuthorization(userId, organisationUser);

    var serviceManagerAuthorizationService = getServiceManagerAuthorizationService();
    var result = await serviceManagerAuthorizationService.EvaluateOrganisationAuthorizationServiceRouteName(_claimsPrincipalMock.Object, serviceRouteName, requirement, cancellationToken);

    Assert.True(result);


    _serviceServiceMock.Verify(m => m.GetByRouteName(serviceRouteName, cancellationToken), Times.Once);
    _serviceServiceMock.VerifyNoOtherCalls();


    verifyEvaluateOrganisationAuthorization(orgId, organisationUser, requirement, cancellationToken);

    _authorizationServiceMock.VerifyNoOtherCalls();
    _organisationUserServiceMock.VerifyNoOtherCalls();
    _organisationServiceMock.VerifyNoOtherCalls();
    _serviceUserServiceMock.VerifyNoOtherCalls();
  }
  #endregion


  #region Mock setup and verify methods
  private void setupEvaluateOrganisationAuthorization(string userId, OrganisationUser organisationUser)
  {
    _claimsPrincipalMock.Setup(m => m.FindFirst(It.IsAny<string>())).Returns(new Claim(ClaimTypes.NameIdentifier, userId));

    _authorizationServiceMock
      .Setup(m => m.AuthorizeAsync(It.IsAny<ClaimsPrincipal>(), It.IsAny<Object>(), It.IsAny<IEnumerable<IAuthorizationRequirement>>()))
      .ReturnsAsync(AuthorizationResult.Success());

    _organisationUserServiceMock
      .Setup(m => m.GetUsers(It.IsAny<int>(), It.IsAny<CancellationToken>())).ReturnsAsync(new List<OrganisationUser> { organisationUser });
  }

  private void verifyEvaluateOrganisationAuthorization(int orgId, OrganisationUser organisationUser, IAuthorizationRequirement requirement, CancellationToken cancellationToken)
  {
    _claimsPrincipalMock.Verify(m => m.FindFirst(ClaimTypes.NameIdentifier), Times.Once);
    _authorizationServiceMock.Verify(m => m.AuthorizeAsync(It.Is<ClaimsPrincipal>(c => c == _claimsPrincipalMock.Object), It.Is<OrganisationRole>(r => r == organisationUser.Role),
      It.Is<IEnumerable<IAuthorizationRequirement>>(e => e.Any(r => r == requirement) && e.Count() == 1)), Times.Once);
    _organisationUserServiceMock.Verify(m => m.GetUsers(orgId, cancellationToken), Times.Once);
  }

  #endregion

  #region ExapmleData
  private ServiceManagerAuthorizationService getServiceManagerAuthorizationService()
  {
    return new ServiceManagerAuthorizationService(_authorizationServiceMock.Object, _organisationServiceMock.Object,
      _organisationUserServiceMock.Object, _serviceUserServiceMock.Object, _serviceServiceMock.Object);
  }

  private OrganisationUser getExampleUser(string userId)
  {
    var role = new OrganisationRole(0, "admin", true, true, true, true, true);
    return new OrganisationUser(userId, "", "", "", role, 0);
  }

  private Service getService(int orgId, int id, string name)
  {
    var organisation = new Organisation("test") { Id = orgId };
    var template = new ServiceTemplate("t1", "", "", orgId);

    return new Service(name, "", template, organisation) { OrganisationId = orgId, Id = id };
  }
  #endregion
}

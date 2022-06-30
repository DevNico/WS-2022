using Moq;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.OrganisationAggregate.Specifications;
using ServiceReleaseManager.Core.Services;
using ServiceReleaseManager.SharedKernel.Interfaces;
using Xunit;

namespace ServiceReleaseManager.UnitTests.Core.Services;

public class OrganisationServiceTest
{
  private readonly Mock<IRepository<Organisation>> _organisationRepositoryMock;

  public OrganisationServiceTest()
  {
    _organisationRepositoryMock = new Mock<IRepository<Organisation>>();
  }

  [Fact]
  public async Task CreateShould()
  {
    var orgName = "test";
    var organisation = new Organisation(orgName);

    var cancellationToken = new CancellationToken();

    _organisationRepositoryMock
     .Setup(m => m.AddAsync(It.IsAny<Organisation>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(organisation);

    var organisationService = new OrganisationService(_organisationRepositoryMock.Object);
    var result = await organisationService.Create(orgName, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(organisation, result.Value);

    _organisationRepositoryMock.Verify(
      m => m.GetBySpecAsync(
        It.IsAny<OrganisationByRouteNameSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _organisationRepositoryMock.Verify(
      m => m.AddAsync(
        It.Is<Organisation>(
          o => o.IsActive == true && o.Name == orgName &&
               orgName.Equals(o.RouteName, StringComparison.OrdinalIgnoreCase)
        ),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _organisationRepositoryMock.Verify(
      m => m.UpdateAsync(
        It.Is<Organisation>(
          o => o.IsActive == true && o.Name == orgName &&
               orgName.Equals(o.RouteName, StringComparison.OrdinalIgnoreCase)
        ),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _organisationRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _organisationRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByIdShould()
  {
    var orgID = 123;
    var organisation = new Organisation("test") { Id = orgID };

    var cancellationToken = new CancellationToken();

    _organisationRepositoryMock
     .Setup(m => m.GetBySpecAsync(It.IsAny<OrganisationByIdSpec>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(organisation);

    var organisationService = new OrganisationService(_organisationRepositoryMock.Object);
    var result = await organisationService.GetById(orgID, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(organisation, result.Value);

    _organisationRepositoryMock.Verify(
      m => m.GetBySpecAsync(
        It.IsAny<OrganisationByIdSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _organisationRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task GetByRouteNameShould()
  {
    var orgName = "test";
    var organisation = new Organisation(orgName);

    var cancellationToken = new CancellationToken();

    _organisationRepositoryMock
     .Setup(
        m => m.GetBySpecAsync(
          It.IsAny<OrganisationByRouteNameSpec>(),
          It.IsAny<CancellationToken>()
        )
      ).ReturnsAsync(organisation);

    var organisationService = new OrganisationService(_organisationRepositoryMock.Object);
    var result = await organisationService.GetByRouteName(orgName, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Equal(organisation, result.Value);

    _organisationRepositoryMock.Verify(
      m => m.GetBySpecAsync(
        It.IsAny<OrganisationByRouteNameSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _organisationRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task UpdateShould()
  {
    var orgName = "test";
    var orgId = 1234;
    var organisation = new Organisation(orgName) { Id = orgId };
    var orgUpdate = new Organisation(orgName) { Id = orgId, Name = "newName" };

    var cancellationToken = new CancellationToken();

    var organisationService = new OrganisationService(_organisationRepositoryMock.Object);
    var result = await organisationService.Update(orgUpdate, cancellationToken);

    Assert.True(result.IsSuccess);

    _organisationRepositoryMock.Verify(
      m => m.UpdateAsync(orgUpdate, cancellationToken),
      Times.Once
    );
    _organisationRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _organisationRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task DeleteShould()
  {
    var orgName = "test";
    var organisation = new Organisation(orgName);

    var cancellationToken = new CancellationToken();

    _organisationRepositoryMock
     .Setup(
        m => m.GetBySpecAsync(
          It.IsAny<OrganisationByRouteNameSpec>(),
          It.IsAny<CancellationToken>()
        )
      ).ReturnsAsync(organisation);

    var organisationService = new OrganisationService(_organisationRepositoryMock.Object);
    var result = await organisationService.Delete(orgName, cancellationToken);

    Assert.True(result.IsSuccess);

    _organisationRepositoryMock.Verify(
      m => m.GetBySpecAsync(
        It.IsAny<OrganisationByRouteNameSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _organisationRepositoryMock.Verify(
      m => m.UpdateAsync(organisation, cancellationToken),
      Times.Once
    );
    _organisationRepositoryMock.Verify(m => m.SaveChangesAsync(cancellationToken), Times.Once);
    _organisationRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task ListShould()
  {
    var organisation0 = new Organisation("test0") { Id = 123 };
    var organisation1 = new Organisation("test1") { Id = 234 };

    var cancellationToken = new CancellationToken();

    _organisationRepositoryMock
     .Setup(m => m.ListAsync(It.IsAny<OrganisationsSearchSpec>(), It.IsAny<CancellationToken>()))
     .ReturnsAsync(new List<Organisation> { organisation0, organisation1 });

    var organisationService = new OrganisationService(_organisationRepositoryMock.Object);
    var result = await organisationService.List(true, cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Contains(organisation0, result.Value);
    Assert.Contains(organisation1, result.Value);

    _organisationRepositoryMock.Verify(
      m => m.ListAsync(
        It.IsAny<OrganisationsSearchSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _organisationRepositoryMock.VerifyNoOtherCalls();
  }

  [Fact]
  public async Task ListByUserEmailShould()
  {
    var organisation0 = new Organisation("test0") { Id = 123 };
    var organisation1 = new Organisation("test1") { Id = 234 };

    var cancellationToken = new CancellationToken();

    _organisationRepositoryMock
     .Setup(
        m => m.ListAsync(It.IsAny<OrganisationsByUserEmailSpec>(), It.IsAny<CancellationToken>())
      ).ReturnsAsync(new List<Organisation> { organisation0, organisation1 });

    var organisationService = new OrganisationService(_organisationRepositoryMock.Object);
    var result = await organisationService.ListByUserEmail("", cancellationToken);

    Assert.True(result.IsSuccess);
    Assert.Contains(organisation0, result.Value);
    Assert.Contains(organisation1, result.Value);

    _organisationRepositoryMock.Verify(
      m => m.ListAsync(
        It.IsAny<OrganisationsByUserEmailSpec>(),
        It.Is<CancellationToken>(c => c == cancellationToken)
      ),
      Times.Once
    );
    _organisationRepositoryMock.VerifyNoOtherCalls();
  }
}

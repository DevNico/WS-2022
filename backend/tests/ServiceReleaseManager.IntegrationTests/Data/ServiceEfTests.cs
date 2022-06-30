using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.Core.ServiceAggregate;
using Xunit;

namespace ServiceReleaseManager.IntegrationTests.Data;

public class ServiceEfTests : BaseEfRepoTestFixture
{
  [Fact]
  public async Task OrganisationAndServiceTest()
  {
    var organisationRepository = GetRepository();
    const string testOrganisationName = "testOrganisation";
    var organisation = new Organisation(testOrganisationName);
    await organisationRepository.AddAsync(organisation);

    var serviceRepository = GetServiceRepository();
    const string testServiceName = "TestService";
    const string testServiceDescription = "TestServiceDescription";
    var service = new Service(
      testServiceName,
      testServiceDescription,
      new ServiceTemplate("", "", "", organisation.Id),
      organisation
    );
    await serviceRepository.AddAsync(service);

    var newOrganisation = (await organisationRepository.ListAsync()).FirstOrDefault();
    var newService = (await serviceRepository.ListAsync()).FirstOrDefault();

    Assert.Equal(testServiceName, newService?.Name);
    Assert.Equal(testServiceDescription, newService?.Description);
    Assert.Equal(newService?.Organisation?.Id, newOrganisation?.Id);
    Assert.True(newService?.Id > 0);
  }

  [Fact]
  public async Task ServiceAndReleaseTest()
  {
    var organisationRepository = GetRepository();
    const string testOrganisationName = "testOrganisation";
    var organisation = new Organisation(testOrganisationName);
    await organisationRepository.AddAsync(organisation);

    var serviceRepository = GetServiceRepository();
    const string testServiceName = "TestService";
    const string testServiceDescription = "TestServiceDescription";
    var service = new Service(
      testServiceName,
      testServiceDescription,
      new ServiceTemplate("", "", "", organisation.Id),
      organisation
    );
    await serviceRepository.AddAsync(service);

    var releaseRepository = GetReleaseRepository();
    const string testReleaseVersion = "1.0.0";
    const string testReleaseMetadata = "Test Release Metadata";
    var release = new Release(testReleaseVersion, testReleaseMetadata, service.Id);
    await releaseRepository.AddAsync(release);

    var newService = (await serviceRepository.ListAsync()).FirstOrDefault();
    var newRelease = (await releaseRepository.ListAsync()).FirstOrDefault();

    Assert.Equal(testReleaseVersion, newRelease?.Version);
    Assert.Equal(testReleaseMetadata, newRelease?.Metadata);
    Assert.Equal(newService?.Id, newRelease?.ServiceId);
    Assert.True(newRelease?.Id > 0);
  }
}

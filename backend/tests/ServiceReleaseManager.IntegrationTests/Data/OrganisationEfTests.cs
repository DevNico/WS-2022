using ServiceReleaseManager.Core.OrganisationAggregate;
using Xunit;

namespace ServiceReleaseManager.IntegrationTests.Data;

public class OrganisationEfTests : BaseEfRepoTestFixture
{
  [Fact]
  public async Task OrganisationRoleTest()
  {
    var organisationRepository = GetRepository();
    const string testOrganisationName = "testOrganisation";
    var organisation = new Organisation(testOrganisationName);
    await organisationRepository.AddAsync(organisation);

    var organisationRoleRepository = GetOrganisationRoleRepository();
    const string testOrganisationRoleName = "testOrganisationRoleName";
    var organisationRole = new OrganisationRole(
      organisation.Id,
      testOrganisationRoleName,
      false,
      false,
      false,
      false,
      false
    );
    await organisationRoleRepository.AddAsync(organisationRole);

    var newOrganisation = (await organisationRepository.ListAsync()).FirstOrDefault();
    var newOrganisationRole = (await organisationRoleRepository.ListAsync()).FirstOrDefault();

    Assert.Equal(testOrganisationRoleName, newOrganisationRole?.Name);
    Assert.Equal(newOrganisationRole?.OrganisationId, newOrganisation?.Id);
    Assert.True(newOrganisationRole?.Id > 0);
  }

  [Fact]
  public async Task OrganisationUserTest()
  {
    var organisationRepository = GetRepository();
    const string testOrganisationName = "testOrganisation";
    var organisation = new Organisation(testOrganisationName);
    await organisationRepository.AddAsync(organisation);

    var organisationRoleRepository = GetOrganisationRoleRepository();
    const string testOrganisationRoleName = "testOrganisationRoleName";
    var organisationRole = new OrganisationRole(
      organisation.Id,
      testOrganisationRoleName,
      false,
      false,
      false,
      false,
      false
    );
    await organisationRoleRepository.AddAsync(organisationRole);

    var organisationUserRepository = GetOrganisationUserRepository();
    const string testOrganisationUserId = "0";
    const string testOrganisationUserEmail = "mail@example.com";
    const string testOrganisationUserFirstName = "TestFirstName";
    const string testOrganisationUserLastName = "TestLastName";

    var organisationUser = new OrganisationUser(
      testOrganisationUserId,
      testOrganisationUserEmail,
      testOrganisationUserFirstName,
      testOrganisationUserLastName,
      organisationRole,
      organisation.Id
    );
    await organisationUserRepository.AddAsync(organisationUser);

    var newOrganisation = (await organisationRepository.ListAsync()).FirstOrDefault();
    var newOrganisationRole = (await organisationRoleRepository.ListAsync()).FirstOrDefault();
    var newOrganisationUser = (await organisationUserRepository.ListAsync()).FirstOrDefault();

    Assert.Equal(testOrganisationUserId, newOrganisationUser?.UserId);
    Assert.Equal(testOrganisationUserEmail, newOrganisationUser?.Email);
    Assert.Equal(testOrganisationUserFirstName, newOrganisationUser?.FirstName);
    Assert.Equal(testOrganisationUserLastName, newOrganisationUser?.LastName);
    Assert.Equal(newOrganisationUser?.OrganisationId, newOrganisation?.Id);
    Assert.Equal(newOrganisationUser?.Role, newOrganisationRole);
    Assert.True(newOrganisationUser?.Id > 0);
  }
}

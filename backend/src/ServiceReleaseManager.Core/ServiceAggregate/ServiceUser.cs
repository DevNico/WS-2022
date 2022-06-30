using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ServiceAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class ServiceUser : EntityBase, IAggregateRoot
{
  public ServiceUser(int serviceId, ServiceRole serviceRole, OrganisationUser organisationUser)
  {
    ServiceId = serviceId;
    ServiceRole = serviceRole;
    ServiceRoleId = serviceRole.Id;
    OrganisationUser = organisationUser;
    OrganisationUserId = organisationUser.Id;
    IsActive = true;
  }

  public ServiceUser()
  {
  }

  public ServiceRole ServiceRole { get; set; }
  public int ServiceRoleId { get; set; }

  public OrganisationUser OrganisationUser { get; set; }
  public int OrganisationUserId { get; set; }

  public int ServiceId { get; set; }

  public bool IsActive { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var e = new ServiceUserDeactivatedEvent(this);
    RegisterDomainEvent(e);
  }
}

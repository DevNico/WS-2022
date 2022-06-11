using ServiceReleaseManager.Core.OrganisationAggregate.Events;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class Organisation : EntityBase, IAggregateRoot
{
  public Organisation(string name)
  {
    Name = name;
    IsActive = true;
    Roles = new List<OrganisationRole>();
    Users = new List<OrganisationUser>();
    Services = new List<Service>();

    var organisationCreatedEvent = new OrganisationCreatedEvent(this);
    RegisterDomainEvent(organisationCreatedEvent);
  }

  public String Name { get; set; }

  public bool IsActive { get; set; }

  public List<OrganisationRole> Roles { get; set; }
  public List<OrganisationUser> Users { get; set; }

  public List<Service> Services { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var organisationDeactivatedEvent = new OrganisationDeactivatedEvent(this);
    RegisterDomainEvent(organisationDeactivatedEvent);
  }
}

﻿using ServiceReleaseManager.Core.OrganisationAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class Organisation : EntityBase, IAggregateRoot
{
  public Organisation(string name)
  {
    Name = name;
    IsActive = true;
    Users = new List<OrganisationUser>();

    var organisationCreatedEvent = new OrganisationCreatedEvent(this);
    RegisterDomainEvent(organisationCreatedEvent);
  }

  public String Name { get; set; }

  public bool IsActive { get; set; }

  public List<OrganisationUser> Users { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var organisationDeactivatedEvent = new OrganisationDeactivatedEvent(this);
    RegisterDomainEvent(organisationDeactivatedEvent);
  }
}

using System.ComponentModel.DataAnnotations;
using Ardalis.GuardClauses;
using ServiceReleaseManager.Core.OrganisationAggregate.Events;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class Organisation : EntityBase, IAggregateRoot
{
  public Organisation(string name)
  {
    Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    RouteName = Name.Replace(" ", "-").ToLower();
    IsActive = true;

    var organisationCreatedEvent = new OrganisationCreatedEvent(this);
    RegisterDomainEvent(organisationCreatedEvent);
  }

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  [RegularExpression("^(?:[a-zA-Z0-9] ?)+$")]
  public String Name { get; set; }

  public String RouteName { get; }

  public bool IsActive { get; set; }

  public List<OrganisationRole> Roles { get; set; } = new();

  public List<OrganisationUser> Users { get; set; } = new();

  public List<Service> Services { get; set; } = new();

  public void Deactivate()
  {
    IsActive = false;
    var organisationDeactivatedEvent = new OrganisationDeactivatedEvent(this);
    RegisterDomainEvent(organisationDeactivatedEvent);
  }
}

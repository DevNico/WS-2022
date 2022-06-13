using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.OrganisationAggregate.Events;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class OrganisationUser : EntityBase, IAggregateRoot
{
  public OrganisationUser(string userId, string email, string firstName, string lastName)
  {
    UserId = userId;
    Email = email;
    FirstName = firstName;
    LastName = lastName;
    LastSignIn = null;
    Role = OrganisationRole.Administrator;
    IsActive = true;

    var organisationUserCreatedEvent = new OrganisationUserCreatedEvent(this);
    RegisterDomainEvent(organisationUserCreatedEvent);
  }

  [Required]
  public String UserId { get; set; }

  [Required, EmailAddress]
  public String Email { get; set; }

  [Required, MaxLength(50)]
  public String FirstName { get; set; }

  [Required, MaxLength(50)]
  public String LastName { get; set; }

  public DateTime? LastSignIn { get; set; }

  public int OrganisationId { get; set; }

  public OrganisationRole Role { get; set; }
  public int RoleId { get; set; }

  public List<ServiceUser> ServiceUser { get; set; } = new();

  [Required]
  [DefaultValue(true)]
  public bool IsActive { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var organisationUserDeactivatedEvent = new OrganisationUserDeactivatedEvent(this);
    RegisterDomainEvent(organisationUserDeactivatedEvent);
  }
}

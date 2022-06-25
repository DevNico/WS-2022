using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.OrganisationAggregate.Events;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class OrganisationUser : EntityBase, IAggregateRoot
{
  public OrganisationUser(string userId, string email, string firstName, string lastName,
    int organisationId, int roleId)
  {
    UserId = userId;
    Email = email;
    FirstName = firstName;
    LastName = lastName;
    LastSignIn = null;
    OrganisationId = organisationId;
    RoleId = roleId;
    IsActive = true;

    var organisationUserCreatedEvent = new OrganisationUserCreatedEvent(this);
    RegisterDomainEvent(organisationUserCreatedEvent);
  }

  [Required]
  public String UserId { get; }

  [Required]
  [EmailAddress]
  public String Email { get; }

  [Required]
  [MaxLength(50)]
  public String FirstName { get; set; }

  [Required]
  [MaxLength(50)]
  public String LastName { get; set; }

  public DateTime? LastSignIn { get; set; }

  public Organisation Organisation { get; set; }
  public int OrganisationId { get; }

  public OrganisationRole Role { get; set; }
  public int RoleId { get; }

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

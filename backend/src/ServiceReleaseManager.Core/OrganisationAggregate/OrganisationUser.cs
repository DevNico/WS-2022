using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.OrganisationAggregate.Events;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class OrganisationUser : EntityBase, IAggregateRoot
{
  // Used by EF Core
  protected OrganisationUser()
  {
  }

  public OrganisationUser(
    string userId,
    string email,
    string firstName,
    string lastName,
    OrganisationRole role,
    int organisationId)
  {
    UserId = userId;
    Email = email.ToLower();
    FirstName = firstName;
    LastName = lastName;
    Role = role;
    RoleId = role.Id;
    OrganisationId = organisationId;
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

  public int OrganisationId { get; set; }

  public OrganisationRole Role { get; set; }

  public int RoleId { get; set; }

  public List<ServiceUser> ServiceUserList { get; set; } = new();

  [Required]
  public bool IsActive { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var organisationUserDeactivatedEvent = new OrganisationUserDeactivatedEvent(this);
    RegisterDomainEvent(organisationUserDeactivatedEvent);
  }
}

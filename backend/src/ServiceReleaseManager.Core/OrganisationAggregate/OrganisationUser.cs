using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;
using ServiceReleaseManager.Core.OrganisationAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class OrganisationUser : EntityBase, IAggregateRoot
{
  [UsedImplicitly]
  protected OrganisationUser()
  {
  }

  public OrganisationUser(
    string userId,
    string email,
    string firstName,
    string lastName,
    OrganisationRole role,
    int organisationId
  )
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
  public String UserId { get; set; } = null!;

  [Required]
  [EmailAddress]
  public String Email { get; } = null!;

  [Required]
  [MaxLength(50)]
  public String FirstName { get; set; } = null!;

  [Required]
  [MaxLength(50)]
  public String LastName { get; set; } = null!;

  public int OrganisationId { get; set; }

  public OrganisationRole Role { get; set; } = null!;

  public int RoleId { get; set; }

  [Required]
  public bool IsActive { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var organisationUserDeactivatedEvent = new OrganisationUserDeactivatedEvent(this);
    RegisterDomainEvent(organisationUserDeactivatedEvent);
  }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.OrganisationAggregate.Events;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class OrganisationUser : EntityBase
{
  public OrganisationUser(string userId, string email, bool emailVerified, string firstName,
    string lastName,
    DateTime? lastSignIn)
  {
    UserId = userId;
    Email = email;
    EmailVerified = emailVerified;
    FirstName = firstName;
    LastName = lastName;
    LastSignIn = lastSignIn;
    Role = OrganisationRole.Administrator;
    ServiceRoles = new List<UserSevice>();
    IsActive = true;

    var organisationUserCreatedEvent = new OrganisationUserCreatedEvent(this);
    RegisterDomainEvent(organisationUserCreatedEvent);
  }

  [Required] public String UserId { get; set; }

  [Required] [EmailAddress] public String Email { get; set; }

  [Required] public bool EmailVerified { get; set; }

  [Required] [MaxLength(50)] public String FirstName { get; set; }

  [Required] [MaxLength(50)] public String LastName { get; set; }

  public DateTime? LastSignIn { get; set; }

  [Required] public OrganisationRole Role { get; set; }

  public List<UserSevice> ServiceRoles { get; set; }

  [Required] [DefaultValue(true)] public bool IsActive { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var organisationUserDeactivatedEvent = new OrganisationUserDeactivatedEvent(this);
    RegisterDomainEvent(organisationUserDeactivatedEvent);
  }
}

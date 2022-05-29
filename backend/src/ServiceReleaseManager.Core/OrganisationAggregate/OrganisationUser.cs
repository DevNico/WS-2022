using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class OrganisationUser : EntityBase
{
  public OrganisationUser(string userId, string email, bool emailVerified, string firstName, string lastName, DateTime lastSignIn)
  {
    UserId = userId;
    Email = email;
    EmailVerified = emailVerified;
    FirstName = firstName;
    LastName = lastName;
    LastSignIn = lastSignIn;
    Role = OrganisationRole.Administrator;
  }

  public String UserId { get; set; }
  
  public String Email { get; set; }
  
  public bool EmailVerified { get; set; }
  
  public String FirstName { get; set; }
  
  public String LastName { get; set; }
  
  public DateTime LastSignIn { get; set; }
  
  public OrganisationRole Role { get; set; }  
}

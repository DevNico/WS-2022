using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class OrganisationRole : EntityBase
{
  public OrganisationRole(string name, bool serviceRead, bool serviceWrite, bool serviceDelete,
    bool userRead, bool userWrite, bool userDelete)
  {
    Name = name;
    ServiceRead = serviceRead;
    ServiceWrite = serviceWrite;
    ServiceDelete = serviceDelete;
    UserRead = userRead;
    UserWrite = userWrite;
    UserDelete = userDelete;
  }

  public static readonly OrganisationRole Administrator =
    new("Administrator", true, true, true, true, true, true);

  public String Name { get; set; }
  public bool ServiceRead { get; set; }
  public bool ServiceWrite { get; set; }
  public bool ServiceDelete { get; set; }
  public bool UserRead { get; set; }
  public bool UserWrite { get; set; }
  public bool UserDelete { get; set; }
}

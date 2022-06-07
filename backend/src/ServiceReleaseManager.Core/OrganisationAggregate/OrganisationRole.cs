using System.ComponentModel.DataAnnotations;
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


  [Required] [MaxLength(50)] public String Name { get; set; }

  [Required] public bool ServiceRead { get; set; }

  [Required] public bool ServiceWrite { get; set; }

  [Required] public bool ServiceDelete { get; set; }

  [Required] public bool UserRead { get; set; }

  [Required] public bool UserWrite { get; set; }

  [Required] public bool UserDelete { get; set; }
}

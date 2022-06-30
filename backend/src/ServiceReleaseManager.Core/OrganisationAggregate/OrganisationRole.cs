using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.OrganisationAggregate;

public class OrganisationRole : EntityBase, IAggregateRoot
{
  public OrganisationRole(
    int organisationId,
    string name,
    bool serviceWrite,
    bool serviceDelete,
    bool userRead,
    bool userWrite,
    bool userDelete
  )
  {
    OrganisationId = organisationId;
    Name = name;
    ServiceWrite = serviceWrite;
    ServiceDelete = serviceDelete;
    UserRead = userRead;
    UserWrite = userWrite;
    UserDelete = userDelete;
  }

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public String Name { get; set; }

  [Required]
  public bool ServiceWrite { get; set; }

  [Required]
  public bool ServiceDelete { get; set; }

  [Required]
  public bool UserRead { get; set; }

  [Required]
  public bool UserWrite { get; set; }

  [Required]
  public bool UserDelete { get; set; }

  public int OrganisationId { get; set; }

  public static List<OrganisationRole> GetDefaultRoles(int organisationId)
  {
    return new List<OrganisationRole>
    {
      new(organisationId, "Admin", true, true, true, true, true),
      new(organisationId, "User", false, false, true, false, false)
    };
  }
}

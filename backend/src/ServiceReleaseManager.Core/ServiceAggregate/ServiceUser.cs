using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class ServiceUser : EntityBase
{
  public ServiceRole ServiceRole { get; set; }
  public int ServiceRoleId { get; set; }

  public OrganisationUser OrganisationUser { get; set; }
  public int OrganisationUserId { get; set; }
  
  public int ServiceId { get; set; }
}

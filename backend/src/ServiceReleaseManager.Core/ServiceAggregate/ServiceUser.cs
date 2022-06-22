using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class ServiceUser : EntityBase, IAggregateRoot
{
  public ServiceRole ServiceRole { get; set; }
  public int ServiceRoleId { get; set; }

  public OrganisationUser OrganisationUser { get; set; }
  public int OrganisationUserId { get; set; }

  public int ServiceId { get; set; }
}

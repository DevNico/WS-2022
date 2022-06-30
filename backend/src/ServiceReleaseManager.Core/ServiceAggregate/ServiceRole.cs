using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.ServiceAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class ServiceRole : EntityBase, IAggregateRoot
{
  public ServiceRole(int organisationId, string name, bool releaseCreate, bool releaseApprove, bool releasePublish,
    bool releaseMetadataEdit, bool releaseLocalizedMetadataEdit)
  {
    OrganisationId = organisationId;
    Name = name;
    ReleaseCreate = releaseCreate;
    ReleaseApprove = releaseApprove;
    ReleasePublish = releasePublish;
    ReleaseMetadataEdit = releaseMetadataEdit;
    ReleaseLocalizedMetadataEdit = releaseLocalizedMetadataEdit;
    IsActive = true;
  }

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public string Name { get; set; }

  [Required]
  public bool ReleaseCreate { get; set; }

  [Required]
  public bool ReleaseApprove { get; set; }

  [Required]
  public bool ReleasePublish { get; set; }

  [Required]
  public bool ReleaseMetadataEdit { get; set; }

  [Required]
  public bool ReleaseLocalizedMetadataEdit { get; set; }

  public int OrganisationId { get; set; }

  [Required]
  public bool IsActive { get; set; }

  public void Deactivate()
  {
    IsActive = false;
    var e = new ServiceRoleDeactivatedEvent(this);
    RegisterDomainEvent(e);
  }
}

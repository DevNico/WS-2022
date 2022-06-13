using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class ServiceRole : EntityBase
{
  public ServiceRole(string name, bool releaseCreate, bool releaseApprove, bool releasePublish,
    bool releaseMetadataEdit, bool releaseLocalizedMetadataEdit)
  {
    Name = name;
    ReleaseCreate = releaseCreate;
    ReleaseApprove = releaseApprove;
    ReleasePublish = releasePublish;
    ReleaseMetadataEdit = releaseMetadataEdit;
    ReleaseLocalizedMetadataEdit = releaseLocalizedMetadataEdit;
  }

  [Required] [MaxLength(50)] [Key] public string Name { get; set; }

  [Required] public bool ReleaseCreate { get; set; }

  [Required] public bool ReleaseApprove { get; set; }

  [Required] public bool ReleasePublish { get; set; }

  [Required] public bool ReleaseMetadataEdit { get; set; }

  [Required] public bool ReleaseLocalizedMetadataEdit { get; set; }
}

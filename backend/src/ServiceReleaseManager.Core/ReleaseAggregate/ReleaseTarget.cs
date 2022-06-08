using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ReleaseAggregate;

public class ReleaseTarget : EntityBase, IAggregateRoot
{
  public ReleaseTarget(string name, bool requiresApproval = false)
  {
    Name = name;
    RequiresApproval = requiresApproval;
    ReleaseTriggers = new List<ReleaseTrigger>();
  }

  [Required] [MaxLength(50)] public string Name { get; set; }

  [Required] [DefaultValue(false)] public bool RequiresApproval { get; set; }

  public List<ReleaseTrigger> ReleaseTriggers { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ReleaseAggregate;

public class Release : EntityBase, IAggregateRoot
{
  public Release(string version, int buildNumber, string metadata)
  {
    ApprovedBy = null;
    ApprovedAt = null;
    PublishedBy = null;
    PublishedAt = null;
    Version = version;
    BuildNumber = buildNumber;
    Metadata = metadata;

    var releaseCreatedEvent = new ReleaseCreatedEvent(this);
    RegisterDomainEvent(releaseCreatedEvent);
  }

  public OrganisationUser? ApprovedBy { get; set; }

  public DateTime? ApprovedAt { get; set; }

  public OrganisationUser? PublishedBy { get; set; }
  
  public DateTime? PublishedAt { get; set; }
  
  [Required]
  [MaxLength(30)]
  public string Version { get; set; }
  
  [Required]
  public int BuildNumber { get; set; }
  
  [Required]
  [Column(TypeName = "json")]
  public string Metadata { get; set; }

  public void Approve(OrganisationUser user)
  {
    ApprovedBy = user;
    ApprovedAt = DateTime.Now;

    var releaseApprovedEvent = new ReleaseApprovedEvent(this, user, ApprovedAt.Value);
    RegisterDomainEvent(releaseApprovedEvent);
  }

  public void Publish(OrganisationUser user)
  {
    PublishedBy = user;
    PublishedAt = DateTime.Now;

    var releasePublishedEvent = new ReleasePublishedEvent(this, user, PublishedAt.Value);
    RegisterDomainEvent(releasePublishedEvent);
  }
}

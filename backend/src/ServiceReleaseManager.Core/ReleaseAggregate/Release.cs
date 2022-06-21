using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceReleaseManager.Core.OrganisationAggregate;
using ServiceReleaseManager.Core.ReleaseAggregate.Events;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ReleaseAggregate;

public class Release : EntityBase, IAggregateRoot
{
  public Release(string version, string metadata)
  {
    ApprovedBy = null;
    ApprovedAt = null;
    PublishedBy = null;
    PublishedAt = null;
    Version = version;
    Metadata = metadata;

    var releaseCreatedEvent = new ReleaseCreatedEvent(this);
    RegisterDomainEvent(releaseCreatedEvent);
  }

  public OrganisationUser? ApprovedBy { get; private set; }

  public DateTime? ApprovedAt { get; private set; }

  public OrganisationUser? PublishedBy { get; private set; }

  public DateTime? PublishedAt { get; private set; }

  [Required]
  [MaxLength(30)]
  public string Version { get; set; }

  [Required]
  [Column(TypeName = "json")]
  public string Metadata { get; set; }

  public int ServiceId { get; set; }

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

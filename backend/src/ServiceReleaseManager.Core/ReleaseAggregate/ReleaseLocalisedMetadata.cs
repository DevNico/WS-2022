using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using ServiceReleaseManager.Core.ServiceAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ReleaseAggregate;

public class ReleaseLocalisedMetadata : EntityBase, IAggregateRoot
{
  [UsedImplicitly]
  protected ReleaseLocalisedMetadata()
  {
  }

  public ReleaseLocalisedMetadata(string metadata, Release release, Locale locale)
  {
    Metadata = metadata;
    ReleaseId = release.Id;
    LocaleId = locale.Id;
    Release = release;
    Locale = locale;
  }

  [Required]
  [Column(TypeName = "json")]
  public string Metadata { get; set; } = null!;

  [Required]
  public int ReleaseId { get; }

  public Release Release { get; set; } = null!;

  [Required]
  public int LocaleId { get; }

  public Locale Locale { get; set; } = null!;
}

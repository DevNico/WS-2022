using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ReleaseEndpoints;

public class CreateReleaseRequest
{
  public const string Route = "/releases";

  [Required]
  public string Version { get; set; } = default!;

  [Required]
  public string MetaData { get; set; } = default!;

  [Required]
  public List<CreateReleaseLocalisedMetadata> LocalisedMetadataList { get; set; } = default!;

  [Required]
  public int ServiceId { get; set; } = default!;

  public record CreateReleaseLocalisedMetadata
  {
    [Required]
    public string Metadata { get; set; } = default!;

    [Required]
    public int LocaleId { get; set; } = default!;
  }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.Releases;

public class UpdateReleaseRequest
{
  public const string Route = "/releases/{ReleaseId:int}";

  [FromRoute]
  public int ReleaseId { get; set; } = default!;

  [Required]
  public string MetaData { get; set; } = default!;

  [Required]
  public List<UpdateReleaseLocalisedMetadata> LocalisedMetadataList { get; set; } = default!;

  public record UpdateReleaseLocalisedMetadata
  {
    [Required]
    public string Metadata { get; set; } = default!;

    [Required]
    public int LocaleId { get; set; } = default!;
  }
}

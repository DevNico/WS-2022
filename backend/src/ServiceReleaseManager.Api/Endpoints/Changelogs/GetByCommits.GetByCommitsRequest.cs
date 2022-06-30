using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Changelogs;

public class GetByCommitsRequest
{
  public const string Route = "commits";

  [Required]
  public string Owner { get; set; } = null!;
  
  [Required]
  public string Repo { get; set; } = null!;
  
  public DateTime? ClosedAfter { get; set; } = null;

  [DefaultValue(100)]
  public int Limit { get; set; } = default;
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Changelogs;

public class GetByPullRequestsRequest
{
  public const string Route = "pull-requests";

  [Required]
  public string Owner { get; set; } = null!;

  [Required]
  public string Repo { get; set; } = null!;

  public DateTime? MergedAfter { get; set; } = null;

  [DefaultValue(100)]
  public int Limit { get; set; } = default;
}

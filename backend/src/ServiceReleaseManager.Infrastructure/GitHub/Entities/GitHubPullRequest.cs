using System.Text.Json.Serialization;

namespace ServiceReleaseManager.Infrastructure.GitHub.Entities;

[Serializable]
public class GitHubPullRequest : GitHubEntity
{
  [JsonPropertyName("state")]
  public string State { get; set; } = default!;

  [JsonPropertyName("merged_at")]
  public DateTime? MergedAt { get; set; } = default!;
}

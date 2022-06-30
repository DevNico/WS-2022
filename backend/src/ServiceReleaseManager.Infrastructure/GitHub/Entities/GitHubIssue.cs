using System.Text.Json.Serialization;

namespace ServiceReleaseManager.Infrastructure.GitHub.Entities;

[Serializable]
public class GitHubIssue : GitHubEntity
{
  [JsonPropertyName("state")]
  public string State { get; set; } = default!;

  [JsonPropertyName("closed_at")]
  public DateTime? ClosedAt { get; set; } = default!;
}

using System.Text.Json.Serialization;

namespace ServiceReleaseManager.Infrastructure.GitHub.Entities;

public class GitHubEntity
{
  [JsonPropertyName("id")]
  public int Id { get; set; } = default!;

  [JsonPropertyName("url")]
  public string Url { get; set; } = default!;

  [JsonPropertyName("title")]
  public virtual string Title { get; set; } = default!;

  [JsonPropertyName("created_at")]
  public DateTime? CreatedAt { get; set; } = default!;
}

using System.Text.Json.Serialization;

namespace ServiceReleaseManager.Infrastructure.GitHub.Entities;

[Serializable]
public class CommitInformation
{
  [JsonPropertyName("message")]
  public string Message { get; set; } = default!;
}

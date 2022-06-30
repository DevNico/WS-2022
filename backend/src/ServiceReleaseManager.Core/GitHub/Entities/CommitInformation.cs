using System.Text.Json.Serialization;

namespace ServiceReleaseManager.Core.GitHub.Entities;

[Serializable]
public class CommitInformation
{
  [JsonPropertyName("message")]
  public string Message { get; set; } = default!;
}

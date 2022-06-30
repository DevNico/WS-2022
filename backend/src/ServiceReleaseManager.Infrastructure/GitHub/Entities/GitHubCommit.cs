using System.Text.Json.Serialization;

namespace ServiceReleaseManager.Infrastructure.GitHub.Entities;

[Serializable]
public class GitHubCommit : GitHubEntity
{
  [JsonPropertyName("commit")]
  public CommitInformation Info { get; set; } = default!;

  [JsonIgnore]
  public override string Title
  {
    get => Info.Message;
    set => Info.Message = value;
  }
}

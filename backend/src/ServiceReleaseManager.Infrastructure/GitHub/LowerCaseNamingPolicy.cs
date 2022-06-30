using System.Text.Json;

namespace ServiceReleaseManager.Infrastructure.GitHub;

public class LowerCaseNamingPolicy : JsonNamingPolicy
{
  public override string ConvertName(string name)
  {
    return name.ToLower();
  }
}

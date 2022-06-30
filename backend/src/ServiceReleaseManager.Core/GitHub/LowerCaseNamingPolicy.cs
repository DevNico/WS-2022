using System.Text.Json;

namespace ServiceReleaseManager.Core.GitHub;

public class LowerCaseNamingPolicy : JsonNamingPolicy
{
  public override string ConvertName(string name)
  {
    return name.ToLower();
  }
}

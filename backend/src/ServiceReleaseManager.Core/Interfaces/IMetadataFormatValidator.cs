using Ardalis.Result;
using Newtonsoft.Json;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IMetadataFormatValidator
{
  public Task<Result<string>> NormalizeMetadata(List<MetadataArrayElement> metadata);
}

public record MetadataArrayElement(
  int Index,
  string Name,
  string Type,
  string Label,
  int? MinLength,
  int? MaxLength,
  string? Placeholder,
  bool Required
)
{
  public static List<MetadataArrayElement> FromJson(string json)
  {
    return JsonConvert.DeserializeObject<List<MetadataArrayElement>>(json)!;
  }
}

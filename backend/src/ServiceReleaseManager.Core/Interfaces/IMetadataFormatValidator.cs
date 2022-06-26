using Newtonsoft.Json;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IMetadataFormatValidator
{
  public string NormalizeMetadata(List<MetadataArrayElement> metadata);
}

public class MetadataFormatValidationError : Exception
{
  public MetadataFormatValidationError(string message) : base(message)
  {
  }
}

public record MetadataArrayElement(
  int Index,
  string Name,
  string Type,
  string Label,
  int? MinLength,
  int? MaxLength,
  bool Required
)
{
  public static List<MetadataArrayElement> FromJson(string json)
  {
    return JsonConvert.DeserializeObject<List<MetadataArrayElement>>(json)!;
  }
}

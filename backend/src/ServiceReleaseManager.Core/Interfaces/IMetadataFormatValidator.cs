namespace ServiceReleaseManager.Core.Interfaces;

public interface IMetadataFormatValidator
{
  public bool IsValidMetadataJson(string metadataJson);
  
  public string NormalizeMetadataJson(string metadataJson);
}

public record MetadataArrayElement(
  int Index,
  string Name,
  string Type,
  string Label,
  int? MinLength,
  int? MaxLength,
  bool Required
);

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceReleaseManager.Core.Interfaces;

namespace ServiceReleaseManager.Infrastructure;

public class MetadataFormatValidator : IMetadataFormatValidator
{
  private static readonly List<string> _allowedTypes = new()
  {
    "all_fields",
    "email",
    "text",
    "semver",
    "checkbox",
    "phone"
  };

  private readonly ILogger _logger;

  public MetadataFormatValidator(ILogger<MetadataFormatValidator> logger)
  {
    _logger = logger;
  }

  public string NormalizeMetadata(List<MetadataArrayElement> metadata)
  {
    ValidateMetadata(metadata);
    var normalized = metadata!.ConvertAll(NormalizeArrayElement);
    normalized.Sort((x, y) => x.Index - y.Index);

    return JsonConvert.SerializeObject(normalized);
  }

  private void ValidateMetadataArrayElement(MetadataArrayElement element,
    List<MetadataArrayElement> elements)
  {
    switch (element.Index)
    {
      case 0 when element.Type == "all_fields" &&
                  elements.FindAll(e => e.Type == "all_fields").Count <= 1:
        break;
      case < 0:
      case > 0 when !elements.Exists(e => e.Index == element.Index - 1):
        _logger.LogDebug("Field '{}' has invalid index: {}", element.Name,
          element.Index.ToString());
        throw new MetadataFormatValidationError(
          $"Field '{element.Name}' has invalid index: {element.Index}");
    }

    if (string.IsNullOrWhiteSpace(element.Name) || string.IsNullOrWhiteSpace(element.Type) ||
        string.IsNullOrWhiteSpace(element.Label) || element.Label.Length > 50 ||
        element.Name.Length > 50)
    {
      _logger.LogDebug("Field '{}' has invalid name, type, or label", element.Name);
      throw new MetadataFormatValidationError(
        $"Field '{element.Name}' has invalid name, type, or label");
    }

    if (_allowedTypes.Contains(element.Type.Trim().ToLower()))
    {
      return;
    }

    _logger.LogDebug("Field '{}' has invalid type: '{}'", element.Name, element.Type);
    throw new MetadataFormatValidationError(
      $"Field '{element.Name}' has invalid type: '{element.Type}'");
  }

  private static MetadataArrayElement NormalizeArrayElement(MetadataArrayElement element)
  {
    var lengthAllowed = element.Type.Trim().ToLower() is "text" or "phone" or "email";

    return new MetadataArrayElement(element.Index, element.Name.Trim(),
      element.Type.Trim().ToLower(),
      element.Label.Trim(), lengthAllowed ? element.MinLength : null,
      lengthAllowed ? element.MaxLength : null,
      element.Required);
  }

  private void ValidateMetadata(List<MetadataArrayElement> metadata)
  {
    if (metadata.Count == 0)
    {
      _logger.LogDebug("The metadata array is empty");
      throw new MetadataFormatValidationError("The metadata array is empty");
    }

    foreach (var metadataArrayElement in metadata)
    {
      ValidateMetadataArrayElement(metadataArrayElement, metadata);
    }
  }
}

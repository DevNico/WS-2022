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

  private static bool ValidateMetadataArrayElement(MetadataArrayElement element, List<MetadataArrayElement> elements)
  {
    switch (element.Index)
    {
      case 0 when element.Type == "all_fields" && elements.FindAll(e => e.Type == "all_fields").Count <= 1:
        break;
      case < 0:
      case > 0 when !elements.Exists(e => e.Index == element.Index - 1):
        return false;
    }

    if (string.IsNullOrWhiteSpace(element.Name) || string.IsNullOrWhiteSpace(element.Type) ||
        string.IsNullOrWhiteSpace(element.Label) || element.Label.Length > 50 || element.Name.Length > 50)
    {
      return false;
    }

    return _allowedTypes.Contains(element.Type);
  }

  private static MetadataArrayElement NormalizeArrayElement(MetadataArrayElement element)
  {
    bool lengthAllowed = element.Type is "text" or "phone" or "email";

    return new MetadataArrayElement(element.Index, element.Name.Trim(), element.Type.Trim(), element.Label.Trim(),
      lengthAllowed ? element.MinLength : null, lengthAllowed ? element.MaxLength : null, element.Required);
  }

  public string NormalizeMetadataJson(string metadataJson)
  {
    var metadata = JsonConvert.DeserializeObject<List<MetadataArrayElement>>(metadataJson);
    var normalized = metadata!.ConvertAll(NormalizeArrayElement);
    normalized.Sort((x, y) => x.Index - y.Index);

    return JsonConvert.SerializeObject(normalized);
  }

  public bool IsValidMetadataJson(string metadataJson)
  {
    var metadata = JsonConvert.DeserializeObject<List<MetadataArrayElement>>(metadataJson);
    return metadata != null && metadata.All(e => ValidateMetadataArrayElement(e, metadata));
  }
}

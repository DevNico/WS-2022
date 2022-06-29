using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NJsonSchema;
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
    "phone",
    "placeholder"
  };

  private readonly ILogger _logger;
  private readonly string _contentRootPath;

  public MetadataFormatValidator(ILogger logger, string contentRootPath)
  {
    _logger = logger;
    _contentRootPath = contentRootPath;
  }

  public async Task<Result<string>> NormalizeMetadata(List<MetadataArrayElement> metadata)
  {
    var validationResult = await ValidateMetadata(metadata);
    if (!validationResult.IsSuccess)
    {
      return validationResult;
    }

    var normalized = metadata.ConvertAll(NormalizeArrayElement);
    normalized.Sort((x, y) => x.Index - y.Index);

    return Result.Success(JsonConvert.SerializeObject(normalized));
  }

  private Result ValidateMetadataArrayElement(MetadataArrayElement element,
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
        return Result.Invalid(new List<ValidationError>
        {
          new()
          {
            ErrorMessage = $"Field '{element.Name}' has invalid index: {element.Index}",
            Severity = ValidationSeverity.Error,
            Identifier = "JsonSchema",
            ErrorCode = "JsonSchema.ValidationError"
          }
        });
    }

    if (string.IsNullOrWhiteSpace(element.Name) || string.IsNullOrWhiteSpace(element.Type) ||
        string.IsNullOrWhiteSpace(element.Label) || element.Label.Length > 50 ||
        element.Name.Length > 50)
    {
      _logger.LogDebug("Field '{}' has invalid name, type, or label", element.Name);
      return Result.Invalid(new List<ValidationError>
      {
        new()
        {
          ErrorMessage = $"Field '{element.Name}' has invalid name, type, or label",
          Severity = ValidationSeverity.Error,
          Identifier = "JsonSchema",
          ErrorCode = "JsonSchema.ValidationError"
        }
      });
    }

    if (_allowedTypes.Contains(element.Type.Trim().ToLower()))
    {
      return Result.Success();
    }

    _logger.LogDebug("Field '{}' has invalid type: '{}'", element.Name, element.Type);
    return Result.Invalid(new List<ValidationError>
    {
      new()
      {
        ErrorMessage = $"Field '{element.Name}' has invalid type: '{element.Type}'",
        Severity = ValidationSeverity.Error,
        Identifier = "JsonSchema",
        ErrorCode = "JsonSchema.ValidationError"
      }
    });
  }

  private static MetadataArrayElement NormalizeArrayElement(MetadataArrayElement element)
  {
    var lengthAllowed = element.Type.Trim().ToLower() is "text" or "phone" or "email";

    return new MetadataArrayElement(element.Index, element.Name.Trim(),
      element.Type.Trim().ToLower(),
      element.Label.Trim(), lengthAllowed ? element.MinLength : null,
      lengthAllowed ? element.MaxLength : null,
      element.Placeholder?.Trim(),
      element.Required);
  }

  private async Task<Result> ValidateMetadata(List<MetadataArrayElement> metadata)
  {
    var schema =
      await JsonSchema.FromFileAsync(Path.Combine(_contentRootPath,
        "StaticFiles", "service-template-schema.json"));
    var serializerSettings = new JsonSerializerSettings();
    serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    var serialized = JsonConvert.SerializeObject(metadata, serializerSettings);
    var errors = schema.Validate(serialized);
    if (errors != null && errors.Any())
    {
      _logger.LogDebug("Json schema: {Schema}", serialized);
      foreach (var validationError in errors)
      {
        _logger.LogDebug("Validation error: {Error}", validationError.ToString());
      }

      return Result.Invalid(errors.ToList().ConvertAll(e => new ValidationError()
      {
        ErrorMessage = e.ToString(),
        Severity = ValidationSeverity.Error,
        Identifier = "JsonSchema",
        ErrorCode = "JsonSchema.ValidationError"
      }));
    }

    if (metadata.Count == 0)
    {
      _logger.LogDebug("The metadata array is empty");
      return Result.Invalid(new List<ValidationError>
      {
        new()
        {
          Identifier = "JsonSchema",
          ErrorCode = "JsonSchema.ValidationError",
          ErrorMessage = "The metadata array is empty",
          Severity = ValidationSeverity.Error
        }
      });
    }

    foreach (var validationResult in metadata
               .Select(metadataArrayElement =>
                 ValidateMetadataArrayElement(metadataArrayElement, metadata))
               .Where(validationResult => !validationResult.IsSuccess))
    {
      return validationResult;
    }

    return Result.Success();
  }
}

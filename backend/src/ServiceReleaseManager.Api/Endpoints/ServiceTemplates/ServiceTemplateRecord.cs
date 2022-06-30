using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public record ServiceTemplateRecord(
  int Id,
  string Name,
  List<MetadataArrayElement> StaticMetadata,
  List<MetadataArrayElement> LocalizedMetadata
)
{
  public static ServiceTemplateRecord FromEntity(ServiceTemplate serviceTemplate)
  {
    return new ServiceTemplateRecord(
      serviceTemplate.Id,
      serviceTemplate.Name,
      MetadataArrayElement.FromJson(serviceTemplate.StaticMetadata),
      MetadataArrayElement.FromJson(serviceTemplate.LocalizedMetadata)
    );
  }
}

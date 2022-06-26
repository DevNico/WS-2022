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
      Id: serviceTemplate.Id,
      Name: serviceTemplate.Name,
      StaticMetadata: MetadataArrayElement.FromJson(serviceTemplate.StaticMetadata),
      LocalizedMetadata: MetadataArrayElement.FromJson(serviceTemplate.LocalizedMetadata)
    );
  }
}

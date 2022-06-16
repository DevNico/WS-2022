using Newtonsoft.Json;
using ServiceReleaseManager.Core.Interfaces;
using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public record ServiceTemplateRecord(int Id, string name, List<MetadataArrayElement> staticMetadata,
  List<MetadataArrayElement> localizedMetadata)
{
  public static ServiceTemplateRecord FromEntity(ServiceTemplate serviceTemplate)
  {
    return new ServiceTemplateRecord(serviceTemplate.Id, serviceTemplate.Name,
      JsonConvert.DeserializeObject<List<MetadataArrayElement>>(serviceTemplate.StaticMetadata)!,
      JsonConvert.DeserializeObject<List<MetadataArrayElement>>(serviceTemplate
        .LocalizedMetadata)!);
  }
}

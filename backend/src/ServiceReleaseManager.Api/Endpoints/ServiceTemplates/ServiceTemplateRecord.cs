using ServiceReleaseManager.Core.ServiceAggregate;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public record ServiceTemplateRecord(int Id, string name, string staticMetadata, string localizedMetadata)
{
  public static ServiceTemplateRecord FromEntity(ServiceTemplate serviceTemplate)
  {
    return new ServiceTemplateRecord(serviceTemplate.Id, serviceTemplate.Name, serviceTemplate.StaticMetadata,
      serviceTemplate.LocalizedMetadata);
  }
}

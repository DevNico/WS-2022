using ServiceReleaseManager.Api.Routes;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class DeleteServiceTemplate
{
  public const string Route = $"{RouteHelper.BaseRoute}/{{serviceTemplateId:int}}";

  public int ServiceTemplateId { get; set; }

  public static string BuildRoute(string serviceTemplateId)
  {
    return Route.Replace("{serviceTemplateId:int}", serviceTemplateId);
  }
}

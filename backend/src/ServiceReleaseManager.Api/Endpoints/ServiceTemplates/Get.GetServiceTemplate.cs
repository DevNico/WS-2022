using ServiceReleaseManager.Api.Routes;

namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplates;

public class GetServiceTemplate
{
  public const string Route = $"{RouteHelper.BaseRoute}/{{serviceName}}";
  
  public string ServiceName { get; set; }

  public static string BuildRoute(string serviceName)
  {
    return Route.Replace("{serviceName}", serviceName);
  }
}

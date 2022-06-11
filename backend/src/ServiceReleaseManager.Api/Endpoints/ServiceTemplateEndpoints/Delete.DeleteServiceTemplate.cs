namespace ServiceReleaseManager.Api.Endpoints.ServiceTemplateEndpoints;

public class DeleteServiceTemplate
{
  public const string Route = "/servicetemplates/{ServiceTemplateId:int}";
  
  public int ServiceTemplateId { get; set; }
  
  public static string BuildRoute(string serviceTemplateId)
  {
    return Route.Replace("{ServiceTemplateId:int}", serviceTemplateId);
  }
}

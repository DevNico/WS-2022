using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class GetServiceByIdRequest
{
  public const string Route = "{ServiceId:int}";
  
  [Required] public int ServiceId { get; set; }
}

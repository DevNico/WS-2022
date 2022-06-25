using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Services;

public class GetServiceByIdRequest
{
  public const string Route = "{ServiceId:int}";
  
  [Required] public int ServiceId { get; set; }
}

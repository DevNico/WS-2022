using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class DeleteServiceRequest
{
  public const string Route = "{ServiceId:int}";
  
  [Required] public int ServiceId { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class GetServiceByIdRequest
{
  [Required] public int ServiceId { get; set; }
}

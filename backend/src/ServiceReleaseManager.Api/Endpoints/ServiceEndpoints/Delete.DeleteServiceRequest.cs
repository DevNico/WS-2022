using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class DeleteServiceRequest
{
  [Required] public int ServiceId { get; set; }
}

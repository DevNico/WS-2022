using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceEndpoints;

public class CreateServiceRequest
{
  [Required] public string? Name { get; set; }
}

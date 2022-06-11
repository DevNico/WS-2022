using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class DeleteOrganisationRequest
{
  [Required] public string? OrganisationName { get; set; }
}

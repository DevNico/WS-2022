using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class ListOrganisationUserRequest
{
  [Required] public string? OrganisationName { get; set; }
}

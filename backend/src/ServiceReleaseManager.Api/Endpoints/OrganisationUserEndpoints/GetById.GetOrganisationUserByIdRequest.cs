using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class GetOrganisationUserByIdRequest
{
  [Required] public string? OrganisationName { get; set; }
  [Required] public int OrgUserId { get; set; }
}

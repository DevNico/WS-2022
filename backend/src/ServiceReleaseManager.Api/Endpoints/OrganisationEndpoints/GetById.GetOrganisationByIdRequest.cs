using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class GetOrganisationByIdRequest
{
  [Required] public int OrganisationId { get; set; }
}

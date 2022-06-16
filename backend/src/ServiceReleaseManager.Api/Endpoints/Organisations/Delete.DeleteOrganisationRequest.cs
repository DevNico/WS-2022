using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.Organisations;

public class DeleteOrganisationRequest
{
  public const string Route = "{OrganisationRouteName}";

  [Required]
  public string? OrganisationRouteName { get; set; } = default!;
}

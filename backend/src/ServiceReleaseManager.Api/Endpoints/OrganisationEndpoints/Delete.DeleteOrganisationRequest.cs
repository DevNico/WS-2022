using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationEndpoints;

public class DeleteOrganisationRequest
{
  public const string Route = "organisations/{OrganisationRouteName}";

  [Required]
  public string? OrganisationRouteName { get; set; } = default!;

  public static string BuildRoute(string organisationRouteName) =>
    Route.Replace("{OrganisationRouteName}", organisationRouteName);
}

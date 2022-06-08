using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class CreateOrganisationUserRequest
{
  public const string Route = "/organisationusers";  

  [Required] public string? UserId { get; set; }
  [Required] public string? Email { get; set; }
  [Required] public string? FirstName { get; set; }
  [Required] public string? LastName { get; set; }
}

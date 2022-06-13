using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ServiceReleaseManager.Api.Endpoints.OrganisationUserEndpoints;

public class CreateOrganisationUserRequest
{
  public const string Route = "/users";

  [Required]
  public int OrganisationId { get; set; } = default!;

  [Required, EmailAddress]
  public string Email { get; set; } = default!;

  [Required, MinLength(5), MaxLength(50)]
  public string FirstName { get; set; } = default!;

  [Required, MinLength(5), MaxLength(50)]
  public string LastName { get; set; } = default!;
}

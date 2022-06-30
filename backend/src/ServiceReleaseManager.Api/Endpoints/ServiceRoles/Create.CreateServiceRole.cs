using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceRoles;

public class CreateServiceRole
{
  [Required]
  public int OrganisationId { get; set; }

  [Required]
  [MinLength(2)]
  [MaxLength(50)]
  public string Name { get; set; } = default!;

  [Required]
  public bool ReleaseCreate { get; set; }

  [Required]
  public bool ReleaseApprove { get; set; }

  [Required]
  public bool ReleasePublish { get; set; }

  [Required]
  public bool ReleaseMetadataEdit { get; set; }

  [Required]
  public bool ReleaseLocalizedMetadataEdit { get; set; }
}

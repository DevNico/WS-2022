using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.Core.ReleaseAggregate;
using ServiceReleaseManager.SharedKernel;
using ServiceReleaseManager.SharedKernel.Interfaces;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class Service : EntityBase, IAggregateRoot
{
  public Service(string name, string description)
  {
    Name = name;
    Description = description;
  }

  [Required]
  [MinLength(5)]
  [MaxLength(50)]
  public string Name { get; set; }

  [Required]
  [MaxLength(200)]
  public string Description { get; set; }

  public List<Locale> Locales { get; set; } = new();

  public List<ServiceUser> ServiceUsers { get; set; } = new();

  public List<Release> Releases { get; set; } = new();

  public List<ReleaseTarget> ReleaseTargets { get; set; } = new();

  public int OrganisationId { get; set; }
}

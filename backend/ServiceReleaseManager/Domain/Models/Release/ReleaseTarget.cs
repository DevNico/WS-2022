using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.Domain.Models.Release;

public class ReleaseTarget
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public Service.Service Service { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public bool RequiresApproval { get; set; }
}
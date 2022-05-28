using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.Domain.Models.Service;

public class ServiceRole
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
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
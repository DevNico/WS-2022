using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.Domain.Models.Release;

public class ReleaseTrigger
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public ReleaseTarget ReleaseTarget { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Event { get; set; }
    
    [Required]
    public string Url { get; set; }
}
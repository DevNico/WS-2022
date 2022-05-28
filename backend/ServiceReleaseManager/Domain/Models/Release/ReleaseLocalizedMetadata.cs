using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.Domain.Models.Release;

public class ReleaseLocalizedMetadata
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "json")]
    public string Metadata { get; set; }
    
    [Required]
    public Release Release { get; set; }
    
    [Required]
    public Locale Locale { get; set; }
}
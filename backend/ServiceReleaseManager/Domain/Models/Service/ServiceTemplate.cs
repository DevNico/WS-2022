using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.Domain.Models.Service;

public class ServiceTemplate
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "json")]
    public string StaticMetadata { get; set; }
    
    [Required]
    [Column(TypeName = "json")]
    public string LocalizedMetadata { get; set; }
}
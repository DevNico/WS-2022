using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.Domain.Models.Release;

public class Release
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public User.User? ApprovedBy { get; set; }
    
    public DateTime? ApprovedAt { get; set; }
    
    public User.User? PublishedBy { get; set; }
    
    public DateTime? PublishedAt { get; set; }
    
    [Required]
    public Service.Service Service { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string Version { get; set; }
    
    [Required]
    public int BuildNumber { get; set; }
    
    [Required]
    [Column(TypeName = "json")]
    public string Metadata { get; set; }
}
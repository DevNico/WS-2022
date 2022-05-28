using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.Domain.Models.Organisation;

public class OrganisationRole
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public bool ServiceCreate { get; set; }
    
    [Required]
    public bool ServiceDelete { get; set; }
    
    [Required]
    public bool UserCreate { get; set; }
    
    [Required]
    public bool UserDelete { get; set; }
}
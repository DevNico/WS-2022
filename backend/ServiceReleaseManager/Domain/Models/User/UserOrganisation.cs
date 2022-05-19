using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceReleaseManager.Domain.Models.Organisation;

namespace ServiceReleaseManager.Domain.Models.User;

public class UserOrganisation
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
 
    [Required]
    public User User { get; set; }
    
    [Required]
    public Organisation.Organisation Organisation { get; set; }
    
    [Required]
    public OrganisationRole OrganisationRole { get; set; }
}
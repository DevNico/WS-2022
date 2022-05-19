using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ServiceReleaseManager.Domain.Models.Service;

namespace ServiceReleaseManager.Domain.Models.User;

public class UserService
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public ServiceRole Role { get; set; }
    
    [Required]
    public User User { get; set; }
    
    [Required]
    public Service.Service Service { get; set; }
}
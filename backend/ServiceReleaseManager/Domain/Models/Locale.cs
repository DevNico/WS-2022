using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceReleaseManager.Domain.Models;

public class Locale
{
    [Key, Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public bool Default { get; set; }
    
    [Required]
    [MaxLength(2)]
    public string LanguageCode { get; set; }
    
    [Required]
    [MaxLength(2)]
    public string CountryCode { get; set; }
    
    [Required]
    public Service.Service Service { get; set; }
}
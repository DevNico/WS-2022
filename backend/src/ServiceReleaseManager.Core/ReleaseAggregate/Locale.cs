﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ReleaseAggregate;

public class Locale : EntityBase
{
  public Locale(string languageCode, string countryCode, bool isDefault = false)
  {
    LanguageCode = languageCode;
    CountryCode = countryCode;
    IsDefault = isDefault;
  }
  
  [Required]
  [DefaultValue(false)]
  public bool IsDefault { get; set; }
  
  [Required]
  [MaxLength(3)]
  public string LanguageCode { get; set; }
  
  [Required]
  [MaxLength(3)]
  public string CountryCode { get; set; }
}

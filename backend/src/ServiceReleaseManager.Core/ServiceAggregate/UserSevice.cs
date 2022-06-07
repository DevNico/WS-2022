using System.ComponentModel.DataAnnotations;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.ServiceAggregate;

public class UserSevice : EntityBase
{
  [Required] public Service Service { get; set; }
  [Required] public ServiceRole ServiceRole { get; set; }
}

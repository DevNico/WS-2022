using System.ComponentModel.DataAnnotations;

namespace ServiceReleaseManager.Api.Endpoints.ServiceUsers;

public class DeleteServiceUser
{
  public const string Route = "{ServiceUserId:int}";

  [Required]
  public int ServiceUserId { get; set; }
}

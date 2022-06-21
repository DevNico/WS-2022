namespace ServiceReleaseManager.SharedKernel;

public record ErrorResponse(
  string message
)
{
  public static ErrorResponse FromException(Exception exception)
  {
    return new ErrorResponse(exception.Message);
  }
}

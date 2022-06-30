namespace ServiceReleaseManager.Core.GitHub.Exceptions;

public class GitHubException : Exception
{
  public GitHubException(string message, Exception innerException) : base(message, innerException)
  {
  }
}

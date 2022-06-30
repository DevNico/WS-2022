using System.Globalization;
using Ardalis.Result;
using ServiceReleaseManager.Core.GitHub.Entities;

namespace ServiceReleaseManager.Core.GitHub;

public class GitHubProxy : IGitHubProxy
{
  public const string LimitOutOfRangeMessage = "The limit has to be between 1 and 100!";

  /// <summary>
  /// List pull requests from a repository.
  /// </summary>
  /// <param name="owner">The owner of the repository</param>
  /// <param name="repo">The name of the repository</param>
  /// <param name="ct">The cancellation token</param>
  /// <param name="limit">Limits how many pull requests should be retrieved</param>
  /// <returns>A list of <see cref="GitHubPullRequest"/> wrapped in a <see cref="Result{T}"/> object</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the limit is not between 1 and 100</exception>
  public async Task<Result<IEnumerable<GitHubPullRequest>>> ListPulls(string owner, string repo, CancellationToken ct, int limit = 100)
  {
    ArgumentNullException.ThrowIfNull(owner);
    ArgumentNullException.ThrowIfNull(repo);
    if (limit is < 0 or > 100)
    {
      throw new ArgumentOutOfRangeException(nameof(limit), limit, LimitOutOfRangeMessage);
    }

    var client = new GitHubClient();
    var url = $"repos/{owner}/{repo}/pulls?state=closed&per_page={limit}";
    return await client.GetRequest<IEnumerable<GitHubPullRequest>>(url, ct);
  }

  /// <summary>
  /// List issues from a repository.
  /// </summary>
  /// <param name="owner">The owner of the repository</param>
  /// <param name="repo">The name of the repository</param>
  /// <param name="ct">The cancellation token</param>
  /// <param name="limit">Limits how many issues should be retrieved</param>
  /// <returns>A list of <see cref="GitHubIssue"/> wrapped in a <see cref="Result{T}"/> object</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the limit is not between 1 and 100</exception>
  public async Task<Result<IEnumerable<GitHubIssue>>> ListIssues(string owner, string repo, CancellationToken ct, int limit = 100)
  {
    ArgumentNullException.ThrowIfNull(owner);
    ArgumentNullException.ThrowIfNull(repo);
    if (limit is < 0 or > 100)
    {
      throw new ArgumentOutOfRangeException(nameof(limit), limit, LimitOutOfRangeMessage);
    }

    using var client = new GitHubClient();
    var url = $"repos/{owner}/{repo}/issues?state=closed&per_page={limit}";
    return await client.GetRequest<IEnumerable<GitHubIssue>>(url, ct);
  }

  /// <summary>
  /// List commits from a repository.
  /// </summary>
  /// <param name="owner">The owner of the repository</param>
  /// <param name="repo">The name of the repository</param>
  /// <param name="ct">The cancellation token</param>
  /// <param name="timestamp">If not null, only commits after this timestamp are returned</param>
  /// <param name="limit">Limits how many commits should be retrieved</param>
  /// <returns>A list of <see cref="GitHubCommit"/> wrapped in a <see cref="Result{T}"/> object</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the limit is not between 1 and 100</exception>
  public async Task<Result<IEnumerable<GitHubCommit>>> ListCommits(string owner, string repo, CancellationToken ct, DateTime? timestamp = null, int limit = 100)
  {
    ArgumentNullException.ThrowIfNull(owner);
    ArgumentNullException.ThrowIfNull(repo);
    if (limit is < 0 or > 100)
    {
      throw new ArgumentOutOfRangeException(nameof(limit), limit, LimitOutOfRangeMessage);
    }

    using var client = new GitHubClient();
    
    var url = $"repos/{owner}/{repo}/commits?per_page={limit}";
    if (timestamp != null)
    {
      url += "&since=" + timestamp.Value.ToString("o", CultureInfo.InvariantCulture);
    }
    
    return await client.GetRequest<IEnumerable<GitHubCommit>>(url, ct);
  }

  /// <summary>
  /// List issues from a repository.
  /// </summary>
  /// <param name="owner">The owner of the repository</param>
  /// <param name="repo">The name of the repository</param>
  /// <param name="ct">The cancellation token</param>
  /// <param name="timestamp">Only issues after this timestamp are returned</param>
  /// <param name="limit">Limits how many issues should be retrieved</param>
  /// <returns>A list of <see cref="GitHubIssue"/> wrapped in a <see cref="Result{T}"/> object</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the limit is not between 1 and 100</exception>
  public async Task<Result<IEnumerable<GitHubIssue>>> ListIssuesClosedAfter(string owner, string repo,
    DateTime timestamp, CancellationToken ct, int limit = 100)
  {
    ArgumentNullException.ThrowIfNull(timestamp);

    var issues = await ListIssues(owner, repo, ct, limit);
    if (!issues.IsSuccess) return issues;
    
    var result = issues.Value.Where(i => i.ClosedAt != null && i.ClosedAt > timestamp);
    return Result.Success(result);
  }

  /// <summary>
  /// List pull requests from a repository.
  /// </summary>
  /// <param name="owner">The owner of the repository</param>
  /// <param name="repo">The name of the repository</param>
  /// <param name="ct">The cancellation token</param>
  /// <param name="timestamp">Only pull requests after this timestamp are returned</param>
  /// <param name="limit">Limits how many pull requests should be retrieved</param>
  /// <returns>A list of <see cref="GitHubPullRequest"/> wrapped in a <see cref="Result{T}"/> object</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the limit is not between 1 and 100</exception>
  public async Task<Result<IEnumerable<GitHubPullRequest>>> ListPullsMergedAfter(string owner, string repo,
    DateTime timestamp, CancellationToken ct, int limit = 100)
  {
    ArgumentNullException.ThrowIfNull(timestamp);

    var pulls = await ListPulls(owner, repo, ct, limit);
    if (!pulls.IsSuccess) return pulls;
    
    var result = pulls.Value.Where(i => i.MergedAt != null && i.MergedAt > timestamp);
    return Result.Success(result);
  }

  /// <summary>
  /// List commits from a repository.
  /// </summary>
  /// <param name="owner">The owner of the repository</param>
  /// <param name="repo">The name of the repository</param>
  /// <param name="ct">The cancellation token</param>
  /// <param name="timestamp">Only commits after this timestamp are returned</param>
  /// <param name="limit">Limits how many commits should be retrieved</param>
  /// <returns>A list of <see cref="GitHubCommit"/> wrapped in a <see cref="Result{T}"/> object</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the limit is not between 1 and 100</exception>
  public async Task<Result<IEnumerable<GitHubCommit>>> ListCommitsAfter(string owner, string repo,
    DateTime timestamp, CancellationToken ct, int limit = 100)
  {
    ArgumentNullException.ThrowIfNull(timestamp);

    return await ListCommits(owner, repo, ct, timestamp, limit);
  }
}

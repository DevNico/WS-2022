using Ardalis.Result;
using ServiceReleaseManager.Core.GitHub.Entities;

namespace ServiceReleaseManager.Core.GitHub;

public interface IGitHubProxy
{
  /// <summary>
  /// List pull requests from a repository.
  /// </summary>
  /// <param name="owner">The owner of the repository</param>
  /// <param name="repo">The name of the repository</param>
  /// <param name="ct">The cancellation token</param>
  /// <param name="limit">Limits how many pull requests should be retrieved</param>
  /// <returns>A list of <see cref="GitHubPullRequest"/> wrapped in a <see cref="Result{T}"/> object</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the limit is not between 1 and 100</exception>
  Task<Result<IEnumerable<GitHubPullRequest>>> ListPulls(string owner, string repo,
    CancellationToken ct, int limit = 100);

  /// <summary>
  /// List issues from a repository.
  /// </summary>
  /// <param name="owner">The owner of the repository</param>
  /// <param name="repo">The name of the repository</param>
  /// <param name="ct">The cancellation token</param>
  /// <param name="limit">Limits how many issues should be retrieved</param>
  /// <returns>A list of <see cref="GitHubIssue"/> wrapped in a <see cref="Result{T}"/> object</returns>
  /// <exception cref="ArgumentOutOfRangeException">Thrown if the limit is not between 1 and 100</exception>
  Task<Result<IEnumerable<GitHubIssue>>> ListIssues(string owner, string repo, CancellationToken ct,
    int limit = 100);

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
  Task<Result<IEnumerable<GitHubCommit>>> ListCommits(string owner, string repo,
    CancellationToken ct, DateTime? timestamp = null, int limit = 100);

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
  Task<Result<IEnumerable<GitHubIssue>>> ListIssuesClosedAfter(string owner, string repo,
    DateTime timestamp, CancellationToken ct, int limit = 100);

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
  Task<Result<IEnumerable<GitHubPullRequest>>> ListPullsMergedAfter(string owner, string repo,
    DateTime timestamp, CancellationToken ct, int limit = 100);

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
  Task<Result<IEnumerable<GitHubCommit>>> ListCommitsAfter(string owner, string repo,
    DateTime timestamp, CancellationToken ct, int limit = 100);
}

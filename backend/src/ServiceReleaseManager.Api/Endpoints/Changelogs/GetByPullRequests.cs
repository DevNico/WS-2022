using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Core.GitHub;
using ServiceReleaseManager.Core.GitHub.Converters;
using ServiceReleaseManager.Core.GitHub.Entities;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Changelogs;

public class GetByPullRequests : EndpointBase
  .WithRequest<GetByPullRequestsRequest>
  .WithActionResult<string>
{
  private readonly IGitHubProxy _proxy;
  private readonly IChangeLogConverter _converter;

  public GetByPullRequests(IGitHubProxy proxy, IChangeLogConverter converter)
  {
    _proxy = proxy;
    _converter = converter;
  }

  [HttpPost(GetByPullRequestsRequest.Route)]
  [SwaggerOperation(
    Summary = "Build changelogs from pull requests",
    Description = "Generate changelogs from GitHub pull requests",
    OperationId = "Changelogs.PullRequests",
    Tags = new[] { "Changelogs" }
  )]
  [SwaggerResponse(200, "Changelog generated", typeof(string))]
  [SwaggerResponse(400, "The request was invalid")]
  [SwaggerResponse(404, "GitHub Repository not found")]
  public override async Task<ActionResult<string>> HandleAsync(
    [FromBody] GetByPullRequestsRequest request,
    CancellationToken cancellationToken = new CancellationToken())
  {
    // Validate input
    if (request.Limit is < 1 or > 100)
    {
      return BadRequest(new ErrorResponse(GitHubProxy.LimitOutOfRangeMessage));
    }
    
    // Fetch information from GitHub
    Result<IEnumerable<GitHubPullRequest>> pulls;
    if (request.MergedAfter == null)
    {
      pulls = await _proxy.ListPulls(request.Owner, request.Repo, cancellationToken, request.Limit);
    }
    else
    {
      pulls = await _proxy.ListPullsMergedAfter(request.Owner, request.Repo, request.MergedAfter.Value, cancellationToken, request.Limit);
    }

    // Check if repo exists
    if (pulls.Status == ResultStatus.NotFound)
    {
      return NotFound();
    }
    
    // Convert list to a changelog
    return _converter.Convert(pulls.Value, " - ");
  }
}

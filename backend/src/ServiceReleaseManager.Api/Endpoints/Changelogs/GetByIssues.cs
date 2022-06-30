using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Infrastructure.GitHub;
using ServiceReleaseManager.Infrastructure.GitHub.Converters;
using ServiceReleaseManager.Infrastructure.GitHub.Entities;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Changelogs;

public class GetByIssues : EndpointBase
  .WithRequest<GetByIssuesRequest>
  .WithActionResult<string>
{
  private readonly IGitHubProxy _proxy;
  private readonly IChangeLogConverter _converter;

  public GetByIssues(IGitHubProxy proxy, IChangeLogConverter converter)
  {
    _proxy = proxy;
    _converter = converter;
  }

  [HttpPost(GetByIssuesRequest.Route)]
  [SwaggerOperation(
    Summary = "Build changelogs from issues",
    Description = "Generate changelogs from closed GitHub issues",
    OperationId = "Changelogs.Issues",
    Tags = new[] { "Changelogs" }
  )]
  [SwaggerResponse(200, "Changelog generated", typeof(string))]
  [SwaggerResponse(400, "The request was invalid")]
  [SwaggerResponse(404, "GitHub Repository not found")]
  public override async Task<ActionResult<string>> HandleAsync(
    [FromBody] GetByIssuesRequest request,
    CancellationToken cancellationToken = new CancellationToken())
  {
    // Validate input
    if (request.Limit is < 1 or > 100)
    {
      return BadRequest(new ErrorResponse(GitHubProxy.LimitOutOfRangeMessage));
    }
    
    // Fetch information from GitHub
    Result<IEnumerable<GitHubIssue>> issues;
    if (request.ClosedAfter == null)
    {
      issues = await _proxy.ListIssues(request.Owner, request.Repo, cancellationToken, request.Limit);
    }
    else
    {
      issues = await _proxy.ListIssuesClosedAfter(request.Owner, request.Repo, request.ClosedAfter.Value, cancellationToken, request.Limit);
    }

    // Check if repo exists
    if (issues.Status == ResultStatus.NotFound)
    {
      return NotFound();
    }
    
    // Convert list to a changelog
    return _converter.Convert(issues.Value, " - ");
  }
}

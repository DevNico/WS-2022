using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using ServiceReleaseManager.Infrastructure.GitHub;
using ServiceReleaseManager.Infrastructure.GitHub.Converters;
using ServiceReleaseManager.SharedKernel;
using Swashbuckle.AspNetCore.Annotations;

namespace ServiceReleaseManager.Api.Endpoints.Changelogs;

public class GetByCommits : EndpointBase.WithRequest<GetByCommitsRequest>.WithActionResult<string>
{
  private readonly IChangeLogConverter _converter;
  private readonly IGitHubProxy _proxy;

  public GetByCommits(IGitHubProxy proxy, IChangeLogConverter converter)
  {
    _proxy = proxy;
    _converter = converter;
  }

  [HttpPost(GetByCommitsRequest.Route)]
  [SwaggerOperation(
    Summary = "Build changelogs from commits",
    Description = "Generate changelogs from GitHub commits",
    OperationId = "Changelogs.Commits",
    Tags = new[] { "Changelogs" }
  )]
  [SwaggerResponse(200, "Changelog generated", typeof(string))]
  [SwaggerResponse(400, "The request was invalid")]
  [SwaggerResponse(404, "GitHub Repository not found")]
  public override async Task<ActionResult<string>> HandleAsync(
    [FromBody] GetByCommitsRequest request,
    CancellationToken cancellationToken = new()
  )
  {
    // Validate input
    if (request.Limit is < 1 or > 100)
    {
      return BadRequest(new ErrorResponse(GitHubProxy.LimitOutOfRangeMessage));
    }

    // Fetch information from GitHub
    var commits = await _proxy.ListCommits(
      request.Owner,
      request.Repo,
      cancellationToken,
      request.ClosedAfter,
      request.Limit
    );

    // Check if repo exists
    if (commits.Status == ResultStatus.NotFound)
    {
      return NotFound();
    }

    // Convert list to a changelog
    return _converter.Convert(commits.Value, " - ");
  }
}

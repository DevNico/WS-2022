using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Ardalis.Result;
using ServiceReleaseManager.Infrastructure.GitHub.Exceptions;

namespace ServiceReleaseManager.Infrastructure.GitHub;

/// <summary>
/// A HttpClient for the GitHub API
/// </summary>
public sealed class GitHubClient : IDisposable
{
  private const string Prefix = "https://api.github.com/";

  private static readonly JsonSerializerOptions _options = new()
  {
    PropertyNamingPolicy = new LowerCaseNamingPolicy()
  };

  private readonly HttpClient _client;

  /// <summary>
  /// Initializes the HttpClient configures the required headers
  /// </summary>
  public GitHubClient(string token)
  {
    _client = new HttpClient();

    var headers = _client.DefaultRequestHeaders;
    headers.UserAgent.Add(new ProductInfoHeaderValue("ChangeLogSystem", "1.0"));
    headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
    headers.Authorization = new AuthenticationHeaderValue("token", token);
  }

  /// <summary>
  /// Send a GET request to the GitHub API and deserialize the result
  /// </summary>
  /// <param name="uri">The endpoint uri</param>
  /// <param name="ct">The cancellation token</param>
  /// <typeparam name="T">Type of the result</typeparam>
  /// <returns>The deserialized result</returns>
  /// <exception cref="GitHubException">Thrown if the GET request failed</exception>
  public async Task<Result<T>> GetRequest<T>(string uri, CancellationToken ct)
  {
    try
    {
      using var result = await _client.GetAsync(Prefix + uri, ct);
      if (result.StatusCode == HttpStatusCode.NotFound)
      {
        return Result<T>.NotFound();
      }
      
      var parsed = await result.Content.ReadFromJsonAsync<T>(_options, ct);

      return Result<T>.Success(parsed!);
    }
    catch (Exception e)
    {
      throw new GitHubException("GitHub Request failed.", e);
    }
  }

  public void Dispose()
  {
    _client.Dispose();
  }
}

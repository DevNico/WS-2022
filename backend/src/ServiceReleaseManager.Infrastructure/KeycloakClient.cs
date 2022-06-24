using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ServiceReleaseManager.Core.Interfaces;

namespace ServiceReleaseManager.Infrastructure;

public class KeycloakClient : IKeycloakClient
{
  private readonly HttpClient _httpClient;
  private readonly KeycloakOAuthClient _keycloakOAuthClient;
  private readonly ILogger _logger;
  private readonly string _realm;
  private readonly string _url;

  public KeycloakClient(IConfiguration config, ILogger logger, KeycloakOAuthClient _oAuthClient,
    HttpClient httpClient)
  {
    _logger = logger;
    _httpClient = httpClient;
    _keycloakOAuthClient = _oAuthClient;

    _url = config["Keycloak:Url"];
    _realm = config["Keycloak:Realm"];
  }

  public async Task CreateUser(KeycloakUserCreation userCreation)
  {
    await _request<string>("/users", HttpMethod.Post, userCreation, HttpStatusCode.Created);
  }

  public Task<KeycloakUserRecord> GetUser(string userId)
  {
    return _request<KeycloakUserRecord>($"/users/{userId}", HttpMethod.Get);
  }

  public async Task<KeycloakUserRecord?> GetUserByEmail(string email)
  {
    var res = await _request<List<KeycloakUserRecord>>($"/users?email={email}&exact=true",
      HttpMethod.Get);
    return res.Count > 0 ? res[0] : null;
  }

  public async Task UpdateUser(KeycloakUserRecord user)
  {
    await _request<string>($"/users/{user.Id}", HttpMethod.Put, user);
  }

  public async Task SetUserDisplayName(string userId, string firstname, string lastname)
  {
    var user = await GetUser(userId);
    await UpdateUser(user with { Firstname = firstname, Lastname = lastname });
  }

  public async Task SetUserEmailVerified(string userId, bool emailVerified)
  {
    var user = await GetUser(userId);
    await UpdateUser(user with { EmailVerified = emailVerified });
  }

  public async Task SetUserPassword(string userId, string password)
  {
    await _request<string>($"/users/{userId}/reset-password", HttpMethod.Put,
      new PasswordChangeRecord(password),
      HttpStatusCode.NoContent);
  }

  public async Task SetUserDisabled(string userId, bool disabled)
  {
    var user = await GetUser(userId);
    await UpdateUser(user with { Enabled = !disabled });
  }

  private async Task<T> _request<T>(string url, HttpMethod method, object? body = null,
    HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
  {
    _logger.LogDebug("Sending {Method} request to {Url}", method.Method, url);
    var token = await _keycloakOAuthClient.getToken();
    using (var requestMessage =
           new HttpRequestMessage(method, $"{_url}/admin/realms/{_realm}{url}"))
    {
      if (body != null)
      {
        requestMessage.Content = new StringContent(JsonConvert.SerializeObject(body));
        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      }

      requestMessage.Headers.Add("Authorization", $"Bearer {token}");
      var response = await _httpClient.SendAsync(requestMessage);
      var responseContent = await response.Content.ReadAsStringAsync();
      _logger.LogDebug("Keycloak returned a response with status {Code}",
        response.StatusCode.ToString());
      if (response.StatusCode != expectedStatusCode)
      {
        throw new HttpRequestException(
          $"Could not {method.Method.ToUpperInvariant()} to keycloak, error: {responseContent}",
          null,
          response.StatusCode);
      }

      if (typeof(T) == typeof(string))
      {
        return (T)Convert.ChangeType(responseContent, typeof(T));
      }

      T? parsed;
      try
      {
        parsed = JsonConvert.DeserializeObject<T>(responseContent);
      }
      catch (Exception e)
      {
        throw new ApplicationException(
          string.Format("Could not decode json message: {0}", responseContent), e);
      }

      if (parsed == null)
      {
        throw new ApplicationException(string.Format("Could not decode the response: {0}",
          responseContent));
      }

      return parsed;
    }
  }
}

internal record PasswordChangeRecord(
  string value,
  string type = "awPassword",
  bool temporary = false
);

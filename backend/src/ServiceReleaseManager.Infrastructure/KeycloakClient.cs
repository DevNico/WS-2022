using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ServiceReleaseManager.Core.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ServiceReleaseManager.Infrastructure;

public class KeycloakClient : IKeycloakClient
{
  private static readonly HttpClient _httpClient = new();
  private readonly KeycloakOAuthClient _keycloakOAuthClient;
  private readonly string _realm;
  private readonly string _url;

  public KeycloakClient(IConfiguration config)
  {
    _keycloakOAuthClient = new KeycloakOAuthClient(config);

    _url = config["Keycloak:Url"];
    _realm = config["Keycloak:Realm"];
  }

  public async Task<KeycloakUserRecord?> CreateUser(KeycloakUserCreation userCreation)
  {
    var res = await _request<List<KeycloakUserRecord>>("/users", HttpMethod.Post, userCreation,
      HttpStatusCode.Created);

    return res.Count > 0 ? res[0] : null;
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
    var token = await _keycloakOAuthClient.getToken();
    using (var requestMessage =
           new HttpRequestMessage(method, $"{_url}/admin/realms/{_realm}{url}"))
    {
      if (body != null)
      {
        requestMessage.Content = new StringContent(JsonSerializer.Serialize(body));
        requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
      }

      requestMessage.Headers.Add("Authorization", $"Bearer {token}");
      var response = await _httpClient.SendAsync(requestMessage);
      var responseContent = await response.Content.ReadAsStringAsync();
      if (response.StatusCode != expectedStatusCode)
      {
        throw new HttpRequestException(
          string.Format("Could not POST to keycloak, error: {0}", responseContent), null,
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

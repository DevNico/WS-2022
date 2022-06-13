using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using ServiceReleaseManager.Core;
using ServiceReleaseManager.Core.Interfaces;

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

  public Task<KeycloakUserRecord> CreateUser(KeycloakUserCreation userCreation)
  {
    return _request<KeycloakUserRecord>("/users", HttpMethod.Post, userCreation,
      HttpStatusCode.Created);
  }

  public Task<KeycloakUserRecord> GetUser(string userId)
  {
    return _request<KeycloakUserRecord>($"/users/{userId}", HttpMethod.Get);
  }

  public Task<KeycloakUserRecord> GetUserByEmail(string email)
  {
    return _request<KeycloakUserRecord>($"/users?email={email}&exact=true", HttpMethod.Get);
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
      }

      requestMessage.Headers.Add("Authentication", $"Bearer {token}");
      var response = await _httpClient.SendAsync(requestMessage);
      if (response.StatusCode != expectedStatusCode)
      {
        throw new HttpRequestException("Could not POST to keycloak", null, response.StatusCode);
      }

      if (typeof(T) == typeof(string))
      {
        return (T)Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(T));
      }

      var parsed = await response.Content.ReadFromJsonAsync<T>();
      if (parsed == null)
      {
        throw new ApplicationException("Could not decode the response");
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

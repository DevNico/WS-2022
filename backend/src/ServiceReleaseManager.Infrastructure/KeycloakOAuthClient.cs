using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace ServiceReleaseManager.Infrastructure;

public class KeycloakOAuthClient
{
  private static readonly HttpClient _httpClient = new();
  private readonly string _clientId;
  private readonly string _clientSecret;
  private readonly string _realm;
  private readonly string _url;

  private TokenCache? _token;

  public KeycloakOAuthClient(IConfiguration config)
  {
    _url = config["Keycloak:Url"];
    _realm = config["Keycloak:AuthRealm"];
    _clientId = config["Keycloak:ClientId"];
    _clientSecret = config["Keycloak:ClientSecret"];
  }

  public async Task<string> getToken()
  {
    if (_token?.IsExpired == false)
    {
      return _token.respone.access_token;
    }

    using (var content = new FormUrlEncodedContent(new Dictionary<string, string>
           {
             { "client_id", _clientId },
             { "client_secret", _clientSecret },
             { "grant_type", "client_credentials" }
           }))
    {
      content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
      var response =
        await _httpClient.PostAsync($"{_url}/realms/{_realm}/protocol/openid-connect/token",
          content);

      if (response.StatusCode != HttpStatusCode.OK)
      {
        throw new HttpRequestException("Could not get the keycloak oauth token", null,
          response.StatusCode);
      }

      var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
      if (token == null)
      {
        throw new ApplicationException("Could not decode the token");
      }

      _token = new TokenCache(token, DateTime.UtcNow.AddSeconds(token.expires_in));
      return token.access_token;
    }
  }
}

public record TokenResponse(
  string access_token,
  int expires_in
);

public record TokenCache(
  TokenResponse respone,
  DateTime expiresAt
)
{
  public bool IsExpired => DateTime.UtcNow.AddSeconds(30) >= expiresAt;
}

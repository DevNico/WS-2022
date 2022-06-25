using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ServiceReleaseManager.Infrastructure;

public class KeycloakOAuthClient
{
  private readonly string _clientId;
  private readonly string _clientSecret;
  private readonly HttpClient _httpClient;
  private readonly ILogger _logger;
  private readonly string _realm;
  private readonly string _url;

  private TokenCache? _token;

  public KeycloakOAuthClient(IConfiguration config, ILogger logger, HttpClient httpClient)
  {
    _logger = logger;
    _httpClient = httpClient;
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

    _logger.LogDebug("Retrieving a new keycloak oAuth token");
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

      var responseContent = await response.Content.ReadAsStringAsync();
      if (response.StatusCode != HttpStatusCode.OK)
      {
        _logger.LogError(
          "Could not get the keycloak oAuth token, status code {Status}, message; {Message}",
          response.StatusCode.ToString(), responseContent);
        throw new HttpRequestException(
          string.Format("Could not get the keycloak oauth token. Message from keycloak: {0}",
            responseContent), null, response.StatusCode);
      }

      var token = JsonConvert.DeserializeObject<TokenResponse>(responseContent);
      if (token == null)
      {
        _logger.LogError("Could not decode the response message {Message}", responseContent);
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

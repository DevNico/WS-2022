using Newtonsoft.Json;
using ServiceReleaseManager.SharedKernel;

namespace ServiceReleaseManager.Core.Interfaces;

public interface IKeycloakClient
{
  public Task CreateUser(KeycloakUserCreation userCreation);

  public Task<KeycloakUserRecord> GetUser(string userId);

  public Task<KeycloakUserRecord?> GetUserByEmail(string email);

  public Task UpdateUser(KeycloakUserRecord user);

  public Task SetUserDisplayName(string userId, string firstname, string lastname);

  public Task SetUserEmailVerified(string userId, bool emailVerified);

  public Task SetUserPassword(string userId, string password);

  public Task SetUserDisabled(string userId, bool disabled);
}

public record KeycloakUserRecord(
  string Id,
  string Username,
  string? Firstname,
  string? Lastname,
  string? Email,
  bool EmailVerified,
  bool Enabled,
  bool Totp,
  List<string> RequiredActions,
  [JsonConverter(typeof(UnixEpochTimeToDateTimeConverter))]
  DateTime NotBefore,
  [JsonConverter(typeof(UnixEpochTimeToDateTimeConverter))]
  DateTime CreatedTimestamp
);

public record KeycloakUserCreation(
  string username,
  string firstName,
  string lastName,
  string email,
  bool emailVerified = false,
  bool totp = false,
  bool enabled = true
);

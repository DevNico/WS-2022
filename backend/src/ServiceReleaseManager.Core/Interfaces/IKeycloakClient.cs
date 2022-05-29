namespace ServiceReleaseManager.Core.Interfaces;

public interface IKeycloakClient
{
  public Task<KeycloakUserRecord> CreateUser(
    String username,
    String firstname,
    String lastname,
    String email,
    bool enabled,
    bool emailVerified,
    bool totp);

  public Task<KeycloakUserRecord> GetUser(string userId);

  public Task<KeycloakUserRecord> GetUserByEmail(string email);
  
  public Task SetUserDisplayName(string userId, string firstname, string lastname);
  
  public Task SetUserEmailVerified(string userId, bool emailVerified);
  
  public Task SetUserPassword(string userId, string password);
  
  public Task SetUserDisabled(string userId, bool disabled);
}

public record KeycloakUserRecord(
  String Id,
  String Username,
  String? Firstname,
  String? Lastname,
  String? Email,
  bool EmailVerified,
  bool Enabled,
  bool Totp,
  List<String> RequiredActions,
  DateTime NotBefore,
  DateTime CreatedTimestamp
);

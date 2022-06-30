using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Serilog;

namespace ServiceReleaseManager.Api.Authorization;

public class KeycloakRolesClaimsTransformation : IClaimsTransformation
{
  private readonly string _roleClaimType;

  public KeycloakRolesClaimsTransformation(string roleClaimType)
  {
    _roleClaimType = roleClaimType;
  }

  public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
  {
    var result = principal.Clone();
    if (result.Identity is not ClaimsIdentity identity)
    {
      return Task.FromResult(result);
    }

    var realmAccessValue = principal.FindFirst("realm_access")?.Value;
    if (string.IsNullOrWhiteSpace(realmAccessValue))
    {
      return Task.FromResult(result);
    }

    using var realmAccess = JsonDocument.Parse(realmAccessValue);
    var clientRoles = realmAccess
                     .RootElement
                     .GetProperty("roles");

    // Log clientRoles
    var roles = clientRoles.EnumerateArray().Select(x => x.GetString()).ToList();
    Log.Information("Roles: {@Roles}", roles);

    foreach (var value in clientRoles
                         .EnumerateArray()
                         .Select(role => role.GetString())
                         .Where(value => !string.IsNullOrWhiteSpace(value))
                         .OfType<String>())
    {
      identity.AddClaim(new Claim(_roleClaimType, value));
    }

    return Task.FromResult(result);
  }
}

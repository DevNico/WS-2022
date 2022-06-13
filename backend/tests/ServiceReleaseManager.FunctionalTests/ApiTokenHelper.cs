using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ServiceReleaseManager.FunctionalTests;

public class ApiTokenHelper
{
  public static readonly byte[] AuthorizationKey =
    Encoding.ASCII.GetBytes("SecretKeyOfDoomThatMustBeAMinimumNumberOfBytes");

  public static string GetAdminUserToken()
  {
    const string userName = "admin@example.com";
    string[] roles = { "superAdmin" };

    return CreateToken(userName, roles);
  }

  public static string GetNormalUserToken()
  {
    const string userName = "user@example.com";
    return CreateToken(userName, Array.Empty<string>());
  }

  private static string CreateToken(string userName, IEnumerable<string> roles)
  {
    var claims = new List<Claim> { new(ClaimTypes.Name, userName) };

    claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(claims.ToArray()),
      Expires = DateTime.UtcNow.AddHours(1),
      SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(AuthorizationKey),
        SecurityAlgorithms.HmacSha256Signature)
    };
    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }
}

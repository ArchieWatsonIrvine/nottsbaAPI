using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using NottsBAAPI.Models.Auth;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NottsBAAPI.Service;

public class TokenService
{
    IConfiguration _config;
    private readonly SymmetricSecurityKey _key;
    IConfigurationSection _jwtOptions;

    public TokenService(IConfiguration configuration) 
    {
        _config = configuration;
        _jwtOptions = configuration.GetSection("JWTOptions");
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions["Secret"]));
    }
    public string GenerateToken(User user, string deviceId)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(JwtRegisteredClaimNames.NameId, Convert.ToString(user.Id)),
            new Claim("deviceId", deviceId)
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtOptions["AccessTokenExpirationMinutes"])),
            SigningCredentials = credentials,
            Audience = _jwtOptions["Audience"],
            Issuer = _jwtOptions["Issuer"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        try
        {
            // You must use the EXACT same TokenValidationParameters you use in Program.cs,
            // EXCEPT you tell it to ignore the expiration date for this specific check.
            var tokenValidationParameters = new TokenValidationParameters
            {
                // 1. Validate the Audience, and tell it WHAT the audience should be
                ValidateAudience = true,
                ValidAudience = _jwtOptions["Audience"],

                // 2. Validate the Issuer, and tell it WHAT the issuer should be
                ValidateIssuer = true,
                ValidIssuer = _jwtOptions["Issuer"],

                // 3. Validate the Signature (this ensures a hacker didn't tamper with the payload)
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key,

                // 4. Ignore the expiration date so we can crack it open
                ValidateLifetime = false,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.MapInboundClaims = false;
            ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (principal == null) return null;
            // Ensure the token is actually a valid JWT algorithm
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token format");
            }
            return principal;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string GenerateSecureRefreshToken()
    {
        // 1. Create 32 bytes of cryptographically strong random data
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        // 2. Hash it with SHA-256 to guarantee a uniform length
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(randomBytes);

        // 3. Convert to a Hex String. 
        // A 32-byte hash perfectly converts into exactly 64 hexadecimal characters!
        return Convert.ToHexString(hashBytes);
    }

}

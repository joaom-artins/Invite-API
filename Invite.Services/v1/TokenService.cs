using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Invite.Commons;
using Invite.Services.Interfaces.v1;
using Microsoft.IdentityModel.Tokens;

namespace Invite.Services.v1;

public class TokenService(
    AppSettings _appSettings
) : ITokenService
{
    public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = Encoding.UTF8.GetBytes(_appSettings.Jwt.SecretKey);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_appSettings.Jwt.Expiration),
            Audience = _appSettings.Jwt.Audience,
            Issuer = _appSettings.Jwt.Issuer,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return token;
    }
}

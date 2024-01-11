using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using POSWEB.Server.Authentication.OptionSetup;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POSWEB.Server.Authentication;

public sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string Generate(string Email)
    {
        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Email),
            new Claim(JwtRegisteredClaimNames.Email, Email),
        };

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            null,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: signingCredentials
            );
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}

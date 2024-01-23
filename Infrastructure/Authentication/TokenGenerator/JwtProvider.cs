using FluentEmail.Core;
using Infrastructure.Security.TokenGenerator;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Authentication.TokenGenerator;

public sealed class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    public string Generate(TokenUser user)
    {

        if (user == null) return string.Empty;


        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.email),
            new(JwtRegisteredClaimNames.Name, user.firstName),
            new(JwtRegisteredClaimNames.FamilyName, user.lastName),
            new(JwtRegisteredClaimNames.Email, user.email),
            new(CustomClaims.Id, user.id.ToString()),
        };

        user.roles?.ForEach(role => claims.Add(new(CustomClaims.Role, role.ToString())));
        user.permissions?.ForEach(permission => claims.Add(new(CustomClaims.Permissions, permission)));


        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
        claims: claims,
        null,
            expires: DateTime.UtcNow.AddMinutes(_options.TokenExpirationInMinutes),
            signingCredentials: signingCredentials
            );
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenValue;
    }
}

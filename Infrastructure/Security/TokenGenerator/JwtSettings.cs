
namespace Infrastructure.Security.TokenGenerator;

public class JwtSettings
{
    public const string Section = "Jwt";

    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string SecretKey { get; set; } = null!;
    public int TokenExpirationInMinutes { get; set; }
}

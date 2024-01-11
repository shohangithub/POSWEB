namespace POSWEB.Server.Authentication;

public record JwtOptions
{
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string SecretKey { get; init; }
}

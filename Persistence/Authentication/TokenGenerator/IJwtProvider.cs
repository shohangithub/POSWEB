namespace Persistence.Authentication.TokenGenerator;

public interface IJwtProvider
{
    string Generate(TokenUser user);
}

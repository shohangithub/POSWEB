


namespace POSWEB.Server.Authentication;

public interface IJwtProvider
{
    string Generate(User user);
}

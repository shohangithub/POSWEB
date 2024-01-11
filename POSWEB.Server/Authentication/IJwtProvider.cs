namespace POSWEB.Server.Authentication
{
    public interface IJwtProvider
    {
        string Generate(string Email);
    }
}

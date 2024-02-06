namespace Application.Contractors;

public interface IUserTokenService
{
    ValueTask<UserResponseForToken> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    ValueTask<TokenResponse> GetUserToken(string email, CancellationToken cancellationToken = default);
}

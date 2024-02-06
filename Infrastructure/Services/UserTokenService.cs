using Domain.Enums;
using Infrastructure.Authentication;
using Infrastructure.Authentication.TokenGenerator;

namespace Infrastructure.Services;

public class UserTokenService(IRepository<User, int> _repository, IJwtProvider _jwtProvider) : IUserTokenService
{
    public async ValueTask<UserResponseForToken> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var response = await _repository.Query()
            .Where(x => x.Email == email)
            .Select(x => new UserResponseForToken(x.Id, x.TenantId, x.UserName, x.Email, x.Role, x.IsActive, x.Status))
            .FirstOrDefaultAsync();
        return response;
    }

    public async ValueTask<TokenResponse> GetUserToken(string email, CancellationToken cancellationToken = default)
    {
        var user = await GetByEmailAsync(email, cancellationToken);
        if (user is null) throw new Exception("User not found !");

        var token = _jwtProvider.Generate(new TokenUser(
            id: user.Id,
            tenantId: user.TenantId,
            email: user.Email,
            firstName: user.UserName,
            lastName: user.UserName,
            roles: [ERoles.Admin, ERoles.Admin, ERoles.Standard],
            permissions: null
            ));
        return new TokenResponse(token, user.Email, user.UserName, user.UserName);
    }

}

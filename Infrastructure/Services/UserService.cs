
namespace Infrastructure.Services;

public class UserService : IUserService<int>
{
    public ValueTask<bool> AddAsync(UserRequest user)
    {
        throw new NotImplementedException();
    }

    public ValueTask<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<UserResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<Lookup<int>>> GetLookup(Expression<Func<User, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public ValueTask<string> GetUserToken(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> isExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public ValueTask<IEnumerable<UserListResponse>> ListAsync()
    {
        throw new NotImplementedException();
    }

    public ValueTask<bool> UpdateAsync(int id, UserRequest user)
    {
        throw new NotImplementedException();
    }
}

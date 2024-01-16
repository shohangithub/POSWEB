namespace Application.Contractors;

public interface IUserService<T>
{
    ValueTask<IEnumerable<UserListResponse>> ListAsync();
    ValueTask<UserResponse> GetByIdAsync(T id, CancellationToken cancellationToken = default);
    ValueTask<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    ValueTask<string> GetUserToken(string email, CancellationToken cancellationToken = default);
    ValueTask<bool> AddAsync(UserRequest user);
    ValueTask<bool> UpdateAsync(T id, UserRequest user);
    ValueTask<bool> isExistsAsync(T id);
    ValueTask<IEnumerable<Lookup<int>>> GetLookup(Expression<Func<User, bool>> predicate);
}

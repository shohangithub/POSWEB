namespace Application.Contractors;

public interface IUserService<T>
{
    ValueTask<IEnumerable<UserListResponse>> ListAsync();
    ValueTask<UserResponse> GetByIdAsync(T id, CancellationToken cancellationToken = default);
    ValueTask<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    ValueTask<string> GetUserToken(string email, CancellationToken cancellationToken = default);
    ValueTask<UserResponse> AddAsync(UserRequest user, CancellationToken cancellationToken = default);
    ValueTask<UserResponse> UpdateAsync(T id, UserRequest user, CancellationToken cancellationToken = default);
    ValueTask<bool> IsExistsAsync(T id, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Lookup<int>>> GetLookup(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);
}

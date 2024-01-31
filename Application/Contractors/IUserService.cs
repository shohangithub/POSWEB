

using Application.Framework;

namespace Application.Contractors;

public interface IUserService<T>
{
    ValueTask<IEnumerable<UserListResponse>> ListAsync(CancellationToken cancellationToken = default);
    ValueTask<PaginationResult<UserListResponse>> PaginationListAsync(PaginationQuery requestQuery, CancellationToken cancellationToken = default);
    ValueTask<UserResponse> GetByIdAsync(T id, CancellationToken cancellationToken = default);
    ValueTask<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    ValueTask<TokenResponse> GetUserToken(string email, CancellationToken cancellationToken = default);
    ValueTask<UserResponse> AddAsync(UserRequest user, CancellationToken cancellationToken = default);
    ValueTask<UserResponse> UpdateAsync(T id, UserRequest user, CancellationToken cancellationToken = default);
    ValueTask<bool> DeleteAsync(T id, CancellationToken cancellationToken = default);
    ValueTask<bool> IsExistsAsync(T id, CancellationToken cancellationToken = default);
    ValueTask<IEnumerable<Lookup<int>>> GetLookup(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);
}

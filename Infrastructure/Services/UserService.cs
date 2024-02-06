using Domain.Enums;
using Infrastructure.Authentication;
using Infrastructure.Authentication.TokenGenerator;
using Infrastructure.Security.CurrentUserProvider;
using Infrastructure.Validators;
using Persistence.Repositories;

namespace Infrastructure.Services;

public class UserService : IUserService<int>
{
    private readonly IRepository<User, int> _repository;
    private readonly CurrentUser _currentUser;
    public UserService(IRepository<User, int> repository, ICurrentUserProvider currentUserProvider)
    {
        _repository = repository;
        _currentUser = currentUserProvider.GetCurrentUser();
    }

    public async ValueTask<UserResponse> AddAsync(UserRequest user, CancellationToken cancellationToken = default)
    {
        UserValidator validator = new(_repository);
        await validator.ValidateAndThrowAsync(user, cancellationToken);

        var entity = user.Adapt<User>();
        entity.TenantId = _currentUser.TenantId;
        var result = await _repository.AddAsync(entity, cancellationToken);
        var response = result ? entity.Adapt<UserResponse>() : null;
        return response;
    }

    public async ValueTask<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var existingData = await _repository.GetByIdAsync(id, cancellationToken);
        if (existingData is null) throw new ArgumentNullException(nameof(existingData));
        return await _repository.DeleteAsync(existingData, cancellationToken);
    }

    public async ValueTask<UserResponse> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var response = await _repository.Query()
            .Where(x => x.Email == email)
            .Select(x => new UserResponse(x.Id, x.UserName, x.Email, x.Role, x.IsActive, x.Status))
            .FirstOrDefaultAsync();
        return response;
    }

    public async ValueTask<UserResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetByIdAsync(id, cancellationToken);
        var response = result is not null ? result.Adapt<UserResponse>() : null;
        return response;
    }

    public async ValueTask<IEnumerable<Lookup<int>>> GetLookup(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        var result = await _repository.Query().Where(predicate).Select(x => new Lookup<int>(x.Id, x.UserName)).ToListAsync();
        return result;
    }

    public async ValueTask<bool> IsExistsAsync(int id, CancellationToken cancellationToken = default)
        => await _repository.Query().AnyAsync(x => x.Id == id, cancellationToken);

    public async ValueTask<IEnumerable<UserListResponse>> ListAsync(CancellationToken cancellationToken = default)
    {
        var response = await _repository.Query()
           .Select(x => new UserListResponse(x.Id, x.UserName, x.Email, x.Role, x.Status))
           .ToListAsync(cancellationToken);
        return response;
    }

    public async ValueTask<PaginationResult<UserListResponse>> PaginationListAsync(PaginationQuery requestQuery, CancellationToken cancellationToken = default)
    {

        Expression<Func<User, bool>>? predicate = null;

        if (!string.IsNullOrEmpty(requestQuery.OpenText) && !string.IsNullOrWhiteSpace(requestQuery.OpenText))
        {
            predicate = obj => obj.UserName.ToLower().Contains(requestQuery.OpenText.ToLower())
                            || obj.Email.ToLower().Contains(requestQuery.OpenText.ToLower());
        }

        Expression<Func<User, UserListResponse>>? selector = x => new UserListResponse(x.Id, x.UserName, x.Email, x.Role, x.Status);

        return await _repository.PaginationQuery(paginationQuery: requestQuery, predicate: predicate, selector: selector, cancellationToken);
    }

    public async ValueTask<UserResponse> UpdateAsync(int id, UserRequest user, CancellationToken cancellationToken = default)
    {
        var entity = user.Adapt<User>();
        var result = await _repository.UpdateAsync(entity, cancellationToken);
        if (result is null) return null;


        var response = entity.Adapt<UserResponse>();
        return response;
    }
}

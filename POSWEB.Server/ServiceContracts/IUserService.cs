using POSWEB.Server.DataTransferObjets.Common;
using POSWEB.Server.Entitites;
using System.Linq.Expressions;

namespace POSWEB.Server.ServiceContracts
{
    public interface IUserService
    {
        ValueTask<List<User>> UserListAsync();
        ValueTask<User> GetByIdAsync(uint id, CancellationToken cancellationToken = default);
        ValueTask<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
        ValueTask<string> GetUserToken(string email, CancellationToken cancellationToken = default);
        ValueTask<bool> AddAsync(User user);
        ValueTask<bool> UpdateAsync(uint id, User user);
        ValueTask<bool> isExistsAsync(uint id);
        ValueTask<IEnumerable<LookupRecordStruct>> UsersLookup(Expression<Func<User, bool>> predicate);
    }
}

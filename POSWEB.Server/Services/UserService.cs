using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using POSWEB.Server.Authentication;
using POSWEB.Server.Context;
using POSWEB.Server.DataTransferObjets.Common;
using POSWEB.Server.Entitites;
using POSWEB.Server.ServiceContracts;
using System;
using System.Collections;
using System.Linq.Expressions;

namespace POSWEB.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtProvider _jwtProvider;
        public UserService(ApplicationDbContext context, IJwtProvider jwtProvider)
        {
            _context = context;
            _jwtProvider = jwtProvider;
        }

        public async ValueTask<List<User>> UserListAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async ValueTask<User> GetByIdAsync(uint id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.Where(ele => ele.Id == id).FirstAsync(cancellationToken);
        }
        public async ValueTask<User> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.FirstOrDefaultAsync(ele => ele.Email == email,cancellationToken);
        }

        public async ValueTask<string> GetUserToken(string email, CancellationToken cancellationToken = default)
        {
            var user = await GetByEmailAsync(email, cancellationToken);
            if (user is null) throw new Exception("User not found !");

            var token = _jwtProvider.Generate(email);
            return token;
        }
        public async ValueTask<bool> AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public async ValueTask<bool> UpdateAsync(uint id, User user)
        {
            var result = Task.Run(() =>
            {
                var result = _context.Users.Where(x => x.Id < id)
                   .ExecuteUpdate(setters => setters
                        .SetProperty(x => x.UserName, user.UserName)
                        .SetProperty(x => x.IsActive, user.IsActive)
           );

                return result > 0;
            });
            return await result;
        }

        public bool DeleteAsync(uint id)
        {
            var result = _context.Users.Where(x => x.Id == id).ExecuteDelete();
            return result > 0;
        }
        public async ValueTask<bool> DeleteAsync(params int[] ids)
        {
            var result = Task.Run(() =>
            {
                var result = _context.Users.Where(x => ids.Contains(x.Id)).ExecuteDelete();
                return result > 0;
            });
            return await result;
        }


        public async ValueTask<bool> isExistsAsync(uint id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }

        public async ValueTask<IEnumerable<LookupRecordStruct>> UsersLookup(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Where(predicate).Select(x => new LookupRecordStruct { Id = x.Id, Name = x.UserName }).ToArrayAsync();
        }
    }
}
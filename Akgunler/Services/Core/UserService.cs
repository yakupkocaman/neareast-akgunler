using Akgunler.Data;
using Akgunler.Models.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Akgunler.Services.Core
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> mUserRepository;
        private readonly IRepository<UserRole> mUserRoleRepository;

        public UserService(
            IRepository<User> userRepository,
            IRepository<UserRole> userRoleRepository
        ){
            mUserRepository = userRepository;
            mUserRoleRepository = userRoleRepository;
        }

        public void Insert(User user) 
        {
            mUserRepository.Add(user);
            mUserRepository.SaveChange();
        }

        public void Update(User user) 
        {
            mUserRepository.Update(user);
        }

        public User GetUserById(int id) {
            return mUserRepository.Query()
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefault(x=> x.Id == id);
        }

        public User GetUserByName(string username) {
            return mUserRepository.Query()
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefault(x=> x.Username == username);
        }

        public int AccessFailed(string username)
        {
            var sql = @"UPDATE CU SET CU.AccessFailedCount = ISNULL(CU.AccessFailedCount, 0) + 1,
						LockoutEnabled = CASE WHEN CU.AccessFailedCount + 1 > 3 THEN 1 ELSE CU.LockoutEnabled END,
						LockoutEndDate = CASE WHEN CU.AccessFailedCount + 1 > 3 THEN DATEADD(MINUTE, 5, GETDATE()) ELSE CU.LockoutEndDate END
						FROM [Core].[User] CU
						WHERE CU.UserName = @username";

            return User.Scalar<int>(sql, new { username });
        }

        public int Lockout(string username, bool isEnabled)
        {
            if (isEnabled)
            {
                var sql = @"UPDATE CU SET CU.LockoutEnabled = 1, CU.LockoutEndDate = DATEADD(MINUTE, 5, GETDATE()) FROM [Core].[User] CU WHERE CU.UserName = @username";
                return User.Scalar<int>(sql, new { username });
            }
            else
            {
                var sql = @"UPDATE CU SET CU.AccessFailedCount = 0, CU.LockoutEnabled = 0, CU.LockoutEndDate = NULL FROM [Core].[User] CU WHERE CU.UserName = @username";
                return User.Scalar<int>(sql, new { username });
            }
        }

        public List<UserRole> GetAllByUserId(int userId)
        {
            return mUserRoleRepository.Query()
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public List<Role> GetUserRoles(int userId)
        {
            return mUserRoleRepository.Query()
                .Include(x => x.Role)
                .Where(x => x.UserId == userId)
                .Select(x => x.Role)
                .ToList();
        }
    }

    public interface IUserService
    {
        void Insert(User user);
        void Update(User user);
        User GetUserById(int id);
        User GetUserByName(string username);
        int AccessFailed(string username);
        int Lockout(string username, bool isEnabled);
        List<UserRole> GetAllByUserId(int userId);
        List<Role> GetUserRoles(int userId);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;
using WcfApi.Users;

namespace WcfApi.UserLogins
{
    public class UserLoginsRepository
    {
        public UserLogin FindUserLogin(string userId, string password)
        {
            return new DataContext().DbUserLogins
               .Where(userLogin => userLogin.UserId.Equals(userId, StringComparison.InvariantCultureIgnoreCase) && userLogin.PasswordHash.Equals(password))
               .ToList()
               .Select(userLogin => new UserLogin(userLogin))
               .FirstOrDefault();
        }

        public List<UserLogin> GetAllUserLogins()
        {
            return new DataContext().DbUserLogins
                .ToList()
                .Select(userLogin => new UserLogin(userLogin))
                .ToList();
        }

        public List<RemovedUserLogin> GetAllRemovedUserLogins()
        {
            return new DataContext().DbRemovedUserLogins
                .ToList()
                .Select(removedUserLogin => new RemovedUserLogin(removedUserLogin))
                .ToList();
        }

        public List<UserLogin> GetAllActiveUserLogins()
        {
            return new DataContext().DbUserLogins
                .Where(userLogin => userLogin.DbRemovedUserLogin == null)
                .ToList()
                .Select(userLogin => new UserLogin(userLogin))
                .ToList();
        }



        public List<UserLogin> GetUserLogins(Guid userId)
        {
            return new DataContext().DbUserLogins
                .Where(userLogin => userLogin.DbUser.DbUserId == userId)
                .ToList()
                .Select(userLogin => new UserLogin(userLogin))
                .ToList();
        }

        public List<RemovedUserLogin> GetRemovedUserLogins(Guid userId)
        {
            return new DataContext().DbRemovedUserLogins
                .Where(removedUserLogin => removedUserLogin.DbUserLogin.DbUser.DbUserId == userId)
                .ToList()
                .Select(removedUserLogin => new RemovedUserLogin(removedUserLogin))
                .ToList();
        }

        public List<UserLogin> GetActiveUserLogins(Guid userId)
        {
            return new DataContext().DbUserLogins
                .Where(userLogin => userLogin.DbUser.DbUserId == userId)
                .Where(userLogin => userLogin.DbRemovedUserLogin == null)
                .ToList()
                .Select(userLogin => new UserLogin(userLogin))
                .ToList();
        }

        public UserLogin GetUserLogin(Guid userLoginId)
        {
            UserLogin userLogin = null;

            var dbUserLogin = GetDbUserLogin(new DataContext(), userLoginId);

            if (dbUserLogin != null)
            {
                userLogin = new UserLogin(dbUserLogin);
            }

            return userLogin;
        }


        public UserLogin AddUserLogin(UserLogin userLogin)
        {
            var dbContext = new DataContext();
            var dbUserLogin = new DbUserLogin();
            dbUserLogin.DbUserLoginId = userLogin.UserLoginId;
            dbUserLogin.UserId = userLogin.UserId;
            dbUserLogin.PasswordHash = userLogin.PasswordHash;
            dbUserLogin.DbUser = new UsersRepository().GetDbUser(dbContext, userLogin.User.UserId);

            dbUserLogin = dbContext.DbUserLogins.Add(dbUserLogin);
            dbContext.SaveChanges();

            return new UserLogin(dbUserLogin);
        }

        public UserLogin UpdateUserLogin(UserLogin userLogin)
        {
            var dbContext = new DataContext();
            var dbUserLogin = GetDbUserLogin(dbContext, userLogin.UserLoginId);

            if (dbUserLogin != null)
            {
                dbUserLogin.UserId = userLogin.UserId;
                dbUserLogin.PasswordHash = userLogin.PasswordHash;
                dbContext.SaveChanges();
            }

            return new UserLogin(dbUserLogin);
        }

        public RemovedUserLogin RemoveUserLogin(RemovedUserLogin removedUserLogin)
        {
            var dbContext = new DataContext();

            var dbRemovedUserLoginFound = dbContext.DbRemovedUserLogins.FirstOrDefault(dbRemovedUserLogin => dbRemovedUserLogin.DbUserLogin.DbUserLoginId == removedUserLogin.UserLogin.UserLoginId);

            if (dbRemovedUserLoginFound == null)
            {
                dbRemovedUserLoginFound = new DbRemovedUserLogin();
                dbRemovedUserLoginFound.DateRemoved = removedUserLogin.DateRemoved;
                dbRemovedUserLoginFound.DbRemovedUserLoginId = removedUserLogin.RemovedUserLoginId;
                dbRemovedUserLoginFound.DbUserLogin = GetDbUserLogin(dbContext, removedUserLogin.UserLogin.UserLoginId);
                dbRemovedUserLoginFound.DbRemovedByUser = new UsersRepository().GetDbUser(dbContext, removedUserLogin.RemovedByUser.UserId);

                dbRemovedUserLoginFound = dbContext.DbRemovedUserLogins.Add(dbRemovedUserLoginFound);
                dbContext.SaveChanges();
            }


            return new RemovedUserLogin(dbRemovedUserLoginFound);
        }


        public DbUserLogin FindDbUserLogin(DataContext dbContext, Guid userId)
        {
            return dbContext.DbUserLogins.FirstOrDefault(dbUserLogin => dbUserLogin.DbUser.DbUserId == userId);
        }

        public DbUserLogin GetDbUserLogin(DataContext dbContext, Guid dbUserLoginId)
        {
            return dbContext.DbUserLogins.FirstOrDefault(dbUserLogin => dbUserLogin.DbUserLoginId == dbUserLoginId);
        }


    }
}
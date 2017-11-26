using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.Users
{
    public class UsersRepository
    {
        public List<User> GetAllUsers()
        {
            return new DataContext().DbUsers.ToList().Select(user => new User(user)).ToList();
        }

        public List<RemovedUser> GetAllRemovedUsers()
        {
            return new DataContext().DbRemovedUsers.ToList().Select(removedUser => new RemovedUser(removedUser)).ToList();
        }

        public List<User> GetAllActiveUsers()
        {
            return new DataContext().DbUsers.Where(user => user.DbRemovedUser == null).ToList().Select(user => new User(user)).ToList();
        }
        public User AddUser(User user)
        {
            var dbContext = new DataContext();
            var dbUser = new DbUser();
            dbUser.DbUserId = user.UserId;
            dbUser.DbGender = new Genders.Genders().GetDbGender(dbContext, user.Gender.GenderId);
            dbUser.FirstName = user.FirstName;
            dbUser.LastName = user.LastName;

            var addedUser = dbContext.DbUsers.Add(dbUser);
            dbContext.SaveChanges();

            return new User(dbUser);
        }

        public User UpdateUser(User user)
        {
            var dbContext = new DataContext();
            var dbUser = GetDbUser(dbContext, user.UserId);

            if (dbUser != null)
            {
                dbUser.DbGender = new Genders.Genders().GetDbGender(dbContext, user.Gender.GenderId);
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                dbContext.SaveChanges();
            }

            return new User(dbUser);
        }

        public RemovedUser RemoveUser(RemovedUser removedUser)
        {
            var dbContext = new DataContext();

            var dbRemovedUserFound = dbContext.DbRemovedUsers.FirstOrDefault(dbRemovedUser => dbRemovedUser.DbUser.DbUserId == removedUser.User.UserId);

            if (dbRemovedUserFound == null)
            {
                dbRemovedUserFound = new DbRemovedUser();
                dbRemovedUserFound.DateRemoved = removedUser.DateRemoved;
                dbRemovedUserFound.DbRemovedUserId = removedUser.RemovedUserId;
                dbRemovedUserFound.DbUser = GetDbUser(dbContext, removedUser.User.UserId);
                dbRemovedUserFound.DbRemovedByUser = GetDbUser(dbContext, removedUser.RemovedByUser.UserId);

                dbRemovedUserFound = dbContext.DbRemovedUsers.Add(dbRemovedUserFound);
                dbContext.SaveChanges();
            }

       
            return new RemovedUser(dbRemovedUserFound);
        }


        public DbUser GetDbUser(DataContext dbContext, Guid dbUserId)
        {
            return dbContext.DbUsers.FirstOrDefault(dbUser => dbUser.DbUserId == dbUserId);
        }
    }
}

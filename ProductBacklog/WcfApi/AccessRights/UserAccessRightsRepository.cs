using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.AccessRights;
using WcfApi.DataAccessLayer;
using WcfApi.Users;

namespace WcfApi.UserAccessRights
{
    public class UserAccessRightsRepository
    {
        public List<UserAccessRight> GetAllUserAccessRights()
        {
            return new DataContext().DbUserAccessRights
                .ToList()
                .Select(userAccessRight => new UserAccessRight(userAccessRight))
                .ToList();
        }

        public List<RemovedUserAccessRight> GetAllRemovedUserAccessRights()
        {
            return new DataContext().DbRemovedUserAccessRights
                .ToList()
                .Select(removedUserAccessRight => new RemovedUserAccessRight(removedUserAccessRight))
                .ToList();
        }

        public List<UserAccessRight> GetAllActiveUserAccessRights()
        {
            return new DataContext().DbUserAccessRights
                .Where(userAccessRight => userAccessRight.DbRemovedUserAccessRight == null)
                .ToList()
                .Select(userAccessRight => new UserAccessRight(userAccessRight))
                .ToList();
        }



        public List<UserAccessRight> GetUserAccessRights(Guid userId)
        {
            return new DataContext().DbUserAccessRights
                .Where(userAccessRight => userAccessRight.DbUser.DbUserId == userId)
                .ToList()
                .Select(userAccessRight => new UserAccessRight(userAccessRight))
                .ToList();
        }

        public List<RemovedUserAccessRight> GetRemovedUserAccessRights(Guid userId)
        {
            return new DataContext().DbRemovedUserAccessRights
                .Where(removedUserAccessRight => removedUserAccessRight.DbUserAccessRight.DbUser.DbUserId == userId)
                .ToList()
                .Select(removedUserAccessRight => new RemovedUserAccessRight(removedUserAccessRight))
                .ToList();
        }

        public List<UserAccessRight> GetActiveUserAccessRights(Guid userId)
        {
            return new DataContext().DbUserAccessRights
                .Where(userAccessRight => userAccessRight.DbUser.DbUserId == userId)
                .Where(userAccessRight => userAccessRight.DbRemovedUserAccessRight == null)
                .ToList()
                .Select(userAccessRight => new UserAccessRight(userAccessRight))
                .ToList();
        }


        public UserAccessRight AddUserAccessRight(UserAccessRight userAccessRight)
        {
            var dbContext = new DataContext();
            var dbUserAccessRight = new DbUserAccessRight();
            dbUserAccessRight.DbUserAccessRightId = userAccessRight.UserAccessRightId;
            dbUserAccessRight.DbAccessRight = new AccessRightsRepository().GetDbAccessRight(dbContext, userAccessRight.AccessRight.AccessRightId);
            dbUserAccessRight.DbUser = new UsersRepository().GetDbUser(dbContext, userAccessRight.User.UserId);

            var addedUserAccessRight = dbContext.DbUserAccessRights.Add(dbUserAccessRight);
            dbContext.SaveChanges();

            return new UserAccessRight(dbUserAccessRight);
        }

        public UserAccessRight UpdateUserAccessRight(UserAccessRight userAccessRight)
        {
            var dbContext = new DataContext();
            var dbUserAccessRight = GetDbUserAccessRight(dbContext, userAccessRight.UserAccessRightId);

            if (dbUserAccessRight != null)
            {
                dbUserAccessRight.DbAccessRight = new AccessRightsRepository().GetDbAccessRight(dbContext, userAccessRight.AccessRight.AccessRightId);
                dbContext.SaveChanges();
            }

            return new UserAccessRight(dbUserAccessRight);
        }

        public RemovedUserAccessRight RemoveUserAccessRight(RemovedUserAccessRight removedUserAccessRight)
        {
            var dbContext = new DataContext();

            var dbRemovedUserAccessRightFound = dbContext.DbRemovedUserAccessRights.FirstOrDefault(dbRemovedUserAccessRight => dbRemovedUserAccessRight.DbUserAccessRight.DbUserAccessRightId == removedUserAccessRight.UserAccessRight.UserAccessRightId);

            if (dbRemovedUserAccessRightFound == null)
            {
                dbRemovedUserAccessRightFound = new DbRemovedUserAccessRight();
                dbRemovedUserAccessRightFound.DateRemoved = removedUserAccessRight.DateRemoved;
                dbRemovedUserAccessRightFound.DbRemovedUserAccessRightId = removedUserAccessRight.RemovedUserAccessRightId;
                dbRemovedUserAccessRightFound.DbUserAccessRight = GetDbUserAccessRight(dbContext, removedUserAccessRight.UserAccessRight.UserAccessRightId);
                dbRemovedUserAccessRightFound.DbRemovedByUser = new UsersRepository().GetDbUser(dbContext, removedUserAccessRight.RemovedByUser.UserId);

                dbRemovedUserAccessRightFound = dbContext.DbRemovedUserAccessRights.Add(dbRemovedUserAccessRightFound);
                dbContext.SaveChanges();
            }


            return new RemovedUserAccessRight(dbRemovedUserAccessRightFound);
        }


        public DbUserAccessRight GetDbUserAccessRight(DataContext dbContext, Guid dbUserAccessRightId)
        {
            return dbContext.DbUserAccessRights.FirstOrDefault(dbUserAccessRight => dbUserAccessRight.DbUserAccessRightId == dbUserAccessRightId);
        }
    }
}

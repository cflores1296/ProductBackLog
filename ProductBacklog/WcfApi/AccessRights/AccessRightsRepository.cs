using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;
using WcfApi.Users;

namespace WcfApi.AccessRights
{
    public class AccessRightsRepository
    {
        public List<AccessRight> GetAllAccessRights()
        {
            return new DataContext().DbAccessRights.ToList().Select(accessRight => new AccessRight(accessRight)).ToList();
        }

        public List<RemovedAccessRight> GetAllRemovedAccessRights()
        {
            return new DataContext().DbRemovedAccessRights.ToList().Select(removedAccessRight => new RemovedAccessRight(removedAccessRight)).ToList();
        }

        public List<AccessRight> GetAllActiveAccessRights()
        {
            return new DataContext().DbAccessRights.Where(accessRight => accessRight.DbRemovedAccessRight==null).ToList().Select(accessRight => new AccessRight(accessRight)).ToList();
        }


        public AccessRight AddAccessRight(AccessRight accessRight)
        {
            var dbContext = new DataContext();
            var dbAccessRight = new DbAccessRight();
            dbAccessRight.DbAccessRightId = accessRight.AccessRightId;
            dbAccessRight.Name = accessRight.Name;

            var addedAccessRight = dbContext.DbAccessRights.Add(dbAccessRight);
            dbContext.SaveChanges();

            return new AccessRight(dbAccessRight);
        }

        public AccessRight UpdateAccessRight(AccessRight accessRight)
        {
            var dbContext = new DataContext();
            var dbAccessRight = GetDbAccessRight(dbContext, accessRight.AccessRightId);

            if (dbAccessRight != null)
            {
                dbAccessRight.Name = accessRight.Name;
                dbContext.SaveChanges();
            }

            return new AccessRight(dbAccessRight);
        }

        public RemovedAccessRight RemoveAccessRight(RemovedAccessRight removedAccessRight)
        {
            var dbContext = new DataContext();

            var dbRemovedAccessRightFound = dbContext.DbRemovedAccessRights.FirstOrDefault(dbRemovedAccessRight => dbRemovedAccessRight.DbAccessRight.DbAccessRightId == removedAccessRight.AccessRight.AccessRightId);

            if (dbRemovedAccessRightFound == null)
            {
                dbRemovedAccessRightFound = new DbRemovedAccessRight();
                dbRemovedAccessRightFound.DateRemoved = removedAccessRight.DateRemoved;
                dbRemovedAccessRightFound.DbRemovedAccessRightId = removedAccessRight.RemovedAccessRightId;
                dbRemovedAccessRightFound.DbAccessRight = GetDbAccessRight(dbContext, removedAccessRight.AccessRight.AccessRightId);
                dbRemovedAccessRightFound.DbRemovedByUser = new UsersRepository().GetDbUser(dbContext, removedAccessRight.RemovedByUser.UserId);

                dbRemovedAccessRightFound = dbContext.DbRemovedAccessRights.Add(dbRemovedAccessRightFound);
                dbContext.SaveChanges();
            }


            return new RemovedAccessRight(dbRemovedAccessRightFound);
        }


        public DbAccessRight GetDbAccessRight(DataContext dbContext, Guid dbAccessRightId)
        {
            return dbContext.DbAccessRights.FirstOrDefault(dbAccessRight => dbAccessRight.DbAccessRightId == dbAccessRightId);
        }
    }
}

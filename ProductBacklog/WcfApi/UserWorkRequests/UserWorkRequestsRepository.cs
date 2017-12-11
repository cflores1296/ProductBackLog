using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;
using WcfApi.Users;
using WcfApi.WorkRequests;

namespace WcfApi.UserWorkRequests
{
    public class UserWorkRequestsRepository
    {
        public void UpdateDbWorkRequest(DataContext dbContext, DbWorkRequest dbWorkRequest, WorkRequest workRequest)
        {
            // Delete users in dbWorkRequest that are not in workRequest
            var dbUsersWorkRequestsToRemove = dbWorkRequest.DbUserWorkRequests.Where(dbRequest => !workRequest.UsersAssigned.Select(u => u.UserId).Contains(dbRequest.DbUser.DbUserId)).ToList();

            foreach (var dbUsersWorkRequestToRemove in dbUsersWorkRequestsToRemove)
            {
                dbWorkRequest.DbUserWorkRequests.Remove(dbUsersWorkRequestToRemove);
            }

            // Add users in workRequest that are not in dbWorkRequest
            var usersToAdd = workRequest.UsersAssigned.Where(request => !dbWorkRequest.DbUserWorkRequests.Select(u => u.DbUser.DbUserId).Contains(request.UserId)).ToList();

            foreach (var userToAdd in usersToAdd)
            {
                var dbUser = new UsersRepository().GetDbUser(dbContext, userToAdd.UserId);
                if (dbUser != null)
                {
                    dbWorkRequest.DbUserWorkRequests.Add(new DbUserWorkRequest { DbUserWorkRequestId = Guid.NewGuid(), DbUser = dbUser, DbWorkRequest = dbWorkRequest });
                }
            }

            dbContext.SaveChanges();
        }


    }
}

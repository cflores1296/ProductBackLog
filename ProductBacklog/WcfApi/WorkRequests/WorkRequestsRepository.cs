using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.Customers;
using WcfApi.DataAccessLayer;
using WcfApi.SoftwareTypes;
using WcfApi.Users;
using WcfApi.UserWorkRequests;
using WcfApi.WorkStatuses;
using WcfApi.WorkTypes;

namespace WcfApi.WorkRequests
{
    public class WorkRequestsRepository
    {
        public List<WorkRequest> GetAllWorkRequests()
        {
            return new DataContext().DbWorkRequests.ToList().Select(workRequest => new WorkRequest(workRequest)).ToList();
        }

        public List<RemovedWorkRequest> GetAllRemovedWorkRequests()
        {
            return new DataContext().DbRemovedWorkRequests.ToList().Select(removedWorkRequest => new RemovedWorkRequest(removedWorkRequest)).ToList();
        }

        public List<WorkRequest> GetAllActiveWorkRequests()
        {
            return new DataContext().DbWorkRequests.Where(workRequest => workRequest.DbRemovedWorkRequest == null).ToList().Select(workRequest => new WorkRequest(workRequest)).ToList();
        }

        public WorkRequest GetWorkRequest(Guid workRequestId)
        {
            WorkRequest workRequest = null;

            var dbWorkRequest = GetDbWorkRequest(new DataContext(), workRequestId);

            if (dbWorkRequest != null)
            {
                workRequest = new WorkRequest(dbWorkRequest);
            }

            return workRequest;
        }

        public WorkRequest AddWorkRequest(WorkRequest workRequest)
        {
            var dbContext = new DataContext();
            var dbWorkRequest = new DbWorkRequest();
            dbWorkRequest.DbWorkRequestId = workRequest.WorkRequestId;
            dbWorkRequest.RequestDate = workRequest.RequestDate;
            dbWorkRequest.RequestNumber = new WorkRequestNumbersRepository().GetNextNumber();
            dbWorkRequest.Description = workRequest.Description;
            dbWorkRequest.DbCreatedByUser = new UsersRepository().GetDbUser(dbContext, workRequest.CreatedByUser.UserId);
            dbWorkRequest.DbSoftwareType = new SoftwareTypesRepository().GetDbSoftwareType(dbContext, workRequest.SoftwareType.SoftwareTypeId);
            dbWorkRequest.DbWorkStatus = new WorkStatusRepository().GetDbWorkStatus(dbContext, workRequest.WorkStatus.WorkStatusId);
            dbWorkRequest.DbWorkType = new WorkTypesRepository().GetDbWorkType(dbContext, workRequest.WorkType.WorkTypeId);
            dbWorkRequest.DbCustomer = new CustomersRepository().GetDbCustomer(dbContext, workRequest.Customer.CustomerId);

            dbWorkRequest = dbContext.DbWorkRequests.Add(dbWorkRequest);
            dbContext.SaveChanges();

            dbContext = new DataContext();
            dbWorkRequest = GetDbWorkRequest(dbContext, dbWorkRequest.DbWorkRequestId);

            new UserWorkRequestsRepository().UpdateDbWorkRequest(dbContext, dbWorkRequest, workRequest);

            return new WorkRequest(dbWorkRequest);
        }

        public WorkRequest UpdateWorkRequest(WorkRequest workRequest)
        {
            var dbContext = new DataContext();
            var dbWorkRequest = GetDbWorkRequest(dbContext, workRequest.WorkRequestId);

            if (dbWorkRequest != null)
            {
                dbWorkRequest.RequestDate = workRequest.RequestDate;
                dbWorkRequest.Description = workRequest.Description;
                dbWorkRequest.DbSoftwareType = new SoftwareTypesRepository().GetDbSoftwareType(dbContext, workRequest.SoftwareType.SoftwareTypeId);
                dbWorkRequest.DbWorkStatus = new WorkStatusRepository().GetDbWorkStatus(dbContext, workRequest.WorkStatus.WorkStatusId);
                dbWorkRequest.DbWorkType = new WorkTypesRepository().GetDbWorkType(dbContext, workRequest.WorkType.WorkTypeId);
                dbWorkRequest.DbCustomer = new CustomersRepository().GetDbCustomer(dbContext, workRequest.Customer.CustomerId);


                new UserWorkRequestsRepository().UpdateDbWorkRequest(dbContext, dbWorkRequest, workRequest);

                dbContext.SaveChanges();
            }

            return new WorkRequest(dbWorkRequest);
        }

        public RemovedWorkRequest RemoveWorkRequest(RemovedWorkRequest removedWorkRequest)
        {
            var dbContext = new DataContext();

            var dbRemovedWorkRequestFound = dbContext.DbRemovedWorkRequests.FirstOrDefault(dbRemovedWorkRequest => dbRemovedWorkRequest.DbWorkRequest.DbWorkRequestId == removedWorkRequest.WorkRequest.WorkRequestId);

            if (dbRemovedWorkRequestFound == null)
            {
                dbRemovedWorkRequestFound = new DbRemovedWorkRequest();
                dbRemovedWorkRequestFound.DateRemoved = removedWorkRequest.DateRemoved;
                dbRemovedWorkRequestFound.DbRemovedWorkRequestId = removedWorkRequest.RemovedWorkRequestId;
                dbRemovedWorkRequestFound.DbWorkRequest = GetDbWorkRequest(dbContext, removedWorkRequest.WorkRequest.WorkRequestId);
                dbRemovedWorkRequestFound.DbRemovedByUser = new UsersRepository().GetDbUser(dbContext, removedWorkRequest.RemovedByUser.UserId);

                dbRemovedWorkRequestFound = dbContext.DbRemovedWorkRequests.Add(dbRemovedWorkRequestFound);
                dbContext.SaveChanges();
            }


            return new RemovedWorkRequest(dbRemovedWorkRequestFound);
        }

        public RemovedWorkRequest GetRemovedWorkRequest(Guid removedWorkRequestId)
        {
            RemovedWorkRequest removedWorkRequest = null;
            var dbRemovedWorkRequestFound = new DataContext().DbRemovedWorkRequests.FirstOrDefault(dbRemovedWorkRequest => dbRemovedWorkRequest.DbRemovedWorkRequestId == removedWorkRequestId);

            if (dbRemovedWorkRequestFound != null)
            {
                removedWorkRequest = new RemovedWorkRequest(dbRemovedWorkRequestFound);
            }

            return removedWorkRequest;
        }


        public DbWorkRequest GetDbWorkRequest(DataContext dbContext, Guid dbWorkRequestId)
        {
            return dbContext.DbWorkRequests.FirstOrDefault(dbWorkRequest => dbWorkRequest.DbWorkRequestId == dbWorkRequestId);
        }
    }
}

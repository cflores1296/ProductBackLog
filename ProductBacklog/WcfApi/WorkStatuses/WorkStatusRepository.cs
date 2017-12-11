using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.WorkStatuses
{
    public class WorkStatusRepository
    {
        public List<WorkStatus> GetAllWorkStatuses()
        {
            return new DataContext().DbWorkStatuses.ToList().Select(workStatus => new WorkStatus(workStatus)).ToList();
        }

        public WorkStatus FindWorkStatus(string name)
        {
            return new DataContext().DbWorkStatuses.Where(workStatus => workStatus.Name == name).ToList().Select(workStatus => new WorkStatus(workStatus)).FirstOrDefault();
        }

        public DbWorkStatus GetDbWorkStatus(DataContext dbContext, Guid workStatusId)
        {
            return dbContext.DbWorkStatuses.FirstOrDefault(dbWorkStatus => dbWorkStatus.DbWorkStatusId == workStatusId);
        }
    }
}

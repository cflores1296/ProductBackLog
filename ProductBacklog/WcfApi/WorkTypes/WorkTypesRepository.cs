using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.WorkTypes
{
    public class WorkTypesRepository
    {
        public List<WorkType> GetAllWorkTypes()
        {
            return new DataContext().DbWorkTypes.ToList().Select(workType => new WorkType(workType)).ToList();
        }

        public WorkType FindWorkType(string name)
        {
            return new DataContext().DbWorkTypes.Where(workType => workType.Name == name).ToList().Select(workType => new WorkType(workType)).FirstOrDefault();
        }

        public DbWorkType GetDbWorkType(DataContext dbContext, Guid workTypeId)
        {
            return dbContext.DbWorkTypes.FirstOrDefault(dbWorkType => dbWorkType.DbWorkTypeId == workTypeId);
        }
    }
}

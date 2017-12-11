using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.SoftwareTypes
{
    public class SoftwareTypesRepository
    {
        public List<SoftwareType> GetAllSoftwareTypes()
        {
            return new DataContext().DbSoftwareTypes.ToList().Select(softwareType => new SoftwareType(softwareType)).ToList();
        }

        public SoftwareType FindSoftwareType(string name)
        {
            return new DataContext().DbSoftwareTypes.Where(softwareType => softwareType.Name == name).ToList().Select(softwareType => new SoftwareType(softwareType)).FirstOrDefault();
        }

        public DbSoftwareType GetDbSoftwareType(DataContext dbContext, Guid woftwareTypeId)
        {
            return dbContext.DbSoftwareTypes.FirstOrDefault(dbSoftwareType => dbSoftwareType.DbSoftwareTypeId == woftwareTypeId);
        }
    }


}

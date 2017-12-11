using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.WorkRequests
{
    public class WorkRequestNumbersRepository
    {
        public long GetNextNumber()
        {
            long number = 1;

            var dbContext = new DataContext();

            var dbCurrentRequestNumber = FindDbCurrentRequestNumber(dbContext);

            if (dbCurrentRequestNumber == null)
            {
                AddDbCurrentRequestNumber(number);
            }
            else
            {
                number = dbCurrentRequestNumber.Number += 1;
                dbContext.SaveChanges();
            }

            return number;
        }

        
        public DbCurrentRequestNumber AddDbCurrentRequestNumber(long number)
        {
            var dbContext = new DataContext();

            var dbCurrentRequestNumber = new DbCurrentRequestNumber();
            dbCurrentRequestNumber.DbCurrentRequestNumberId = Guid.NewGuid();
            dbCurrentRequestNumber.Number = number;

            dbCurrentRequestNumber = dbContext.DbCurrentRequestNumbers.Add(dbCurrentRequestNumber);
            dbContext.SaveChanges();

            return dbCurrentRequestNumber;
        }

        public DbCurrentRequestNumber FindDbCurrentRequestNumber(DataContext dbContext)
        {
            return dbContext.DbCurrentRequestNumbers.FirstOrDefault();
        }
    }
}

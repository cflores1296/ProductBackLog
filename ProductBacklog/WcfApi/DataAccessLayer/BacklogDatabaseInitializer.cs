using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class BacklogDbInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            if (context.Users.Count() == 0)
            {
                Debug.WriteLine("Adding Users...");
                context.Users.Add(new DbUser { UserId = Guid.NewGuid(), FirstName = "Charlie", LastName = "Flores Paz", Comment="Hey" });
                context.Users.Add(new DbUser { UserId = Guid.NewGuid(), FirstName = "Arin", LastName = "Avastazarian", Comment="Hey2" });
            }

            base.Seed(context);
        }
    }
}

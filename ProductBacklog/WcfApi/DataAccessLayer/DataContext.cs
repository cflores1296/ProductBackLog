using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.Properties;
using WcfApi.Users;

namespace WcfApi.DataAccessLayer
{
    public class DataContext : DbContext
    {
        public DataContext() :base("name=DefaultConnectionString")
        {
            Database.SetInitializer(new BacklogDbInitializer());
        }
        public DbSet<DbUser> Users { get; set; }
    }
}

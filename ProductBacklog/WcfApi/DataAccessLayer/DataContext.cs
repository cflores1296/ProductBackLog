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
        public DataContext() :base("name=DefaultConnectionString") { }
        public DbSet<DbUser> DbUsers { get; set; }
        public DbSet<DbGender> DbGenders { get; set; }
        public DbSet<DbRemovedUser> DbRemovedUsers { get; set; }
        public DbSet<DbAccessRight> DbAccessRights { get; set; }
        public DbSet<DbRemovedAccessRight> DbRemovedAccessRights { get; set; }
        public DbSet<DbUserAccessRight> DbUserAccessRights { get; set; }
        public DbSet<DbRemovedUserAccessRight> DbRemovedUserAccessRights { get; set; }
        public DbSet<DbUserLogin> DbUserLogins { get; set; }
        public DbSet<DbRemovedUserLogin> DbRemovedUserLogins { get; set; }
        public DbSet<DbCustomer> DbCustomers { get; set; }
        public DbSet<DbRemovedCustomer> DbRemovedCustomers { get; set; }
        public DbSet<DbWorkType> DbWorkTypes { get; set; }
        public DbSet<DbWorkStatus> DbWorkStatuses { get; set; }
        public DbSet<DbSoftwareType> DbSoftwareTypes { get; set; }
        public DbSet<DbCurrentRequestNumber> DbCurrentRequestNumbers { get; set; }
        public DbSet<DbWorkRequest> DbWorkRequests { get; set; }
        public DbSet<DbRemovedWorkRequest> DbRemovedWorkRequests { get; set; }
    }
}

namespace WcfApi.Migrations
{
    using DataAccessLayer;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WcfApi.DataAccessLayer.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            DbGender maleGender = context.DbGenders.FirstOrDefault(g => g.Name == "Male");
            DbGender femaleGender = context.DbGenders.FirstOrDefault(g => g.Name == "Female");

            if (maleGender == null)
            {
                maleGender = new DbGender { DbGenderId = Guid.NewGuid(), Name = "Male" };
                context.DbGenders.Add(maleGender);
                context.SaveChanges();
            }

            if (femaleGender == null)
            {
                femaleGender = new DbGender { DbGenderId = Guid.NewGuid(), Name = "Female" };
                context.DbGenders.Add(femaleGender);
                context.SaveChanges();
            }

            // Access Rights
            if (context.DbAccessRights.Count() == 0)
            {
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Login" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit His/Her Own Personal User Information" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit His/Her Own Login Information" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit His/Her Own Access Rights" });

                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can View Users" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Print Users" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Email Users" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Export Users" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Import Users" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Add Users" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Remove Users" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Users" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Other Users' Personal Information" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Other Users' Login Information" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Other Users' Access Rights" });


                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can View Work Requests" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Print Work Requests" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Email Work Requests" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Export Work Requests" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Import Work Requests" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Add Work Requests" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Work Requests" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Work Request Status" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Work Request Assigned To" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Remove Work Requests" });

                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can View Customers" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Print Customers" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Email Customers" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Export Customers" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Import Customers" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Add Customers" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Customers" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Remove Customers" });

                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can View Access Rights" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Print Rights" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Email Rights" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Export Rights" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Import Rights" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Add Access Rights" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Edit Access Rights" });
                context.DbAccessRights.Add(new DbAccessRight { DbAccessRightId = Guid.NewGuid(), Name = "Can Remove Access Rights" });
                context.SaveChanges();
            }

            if (context.DbUsers.Count() == 0)
            {
                context.DbUsers.Add(new DbUser { DbUserId = Guid.NewGuid(), FirstName = "Charlie", LastName = "Flores Paz", DbGender = maleGender });
                context.DbUsers.Add(new DbUser { DbUserId = Guid.NewGuid(), FirstName = "Arin", LastName = "Avastazarian", DbGender = maleGender });
                context.DbUsers.Add(new DbUser { DbUserId = Guid.NewGuid(), FirstName = "Ana", LastName = "None", DbGender = femaleGender });
                context.DbUsers.Add(new DbUser { DbUserId = Guid.NewGuid(), FirstName = "Sareen", LastName = "None", DbGender = femaleGender });
                context.SaveChanges();
            
                var dbUsers = context.DbUsers.ToList();

                // User Access Rights
                foreach (var dbUser in dbUsers)
                {
                    foreach (var dbAccessRight in context.DbAccessRights)
                    {
                        context.DbUserAccessRights.Add(new DbUserAccessRight { DbUserAccessRightId = Guid.NewGuid(), DbUser = dbUser, DbAccessRight = dbAccessRight });
                    }
                }

                // User Logins
                foreach (var dbUser in context.DbUsers)
                {
                    context.DbUserLogins.Add(new DbUserLogin { DbUserLoginId = Guid.NewGuid(), UserId = dbUser.FirstName, PasswordHash = "lmi", DbUser = dbUser });
                }

                context.SaveChanges();
            }




            // Work status
            if (context.DbWorkStatuses.Count() == 0)
            {
                context.DbWorkStatuses.Add(new DbWorkStatus { DbWorkStatusId = Guid.NewGuid(), Name = "Development" });
                context.DbWorkStatuses.Add(new DbWorkStatus { DbWorkStatusId = Guid.NewGuid(), Name = "Testing" });
                context.DbWorkStatuses.Add(new DbWorkStatus { DbWorkStatusId = Guid.NewGuid(), Name = "Complete" });
                context.SaveChanges();
            }

            // Work Types
            if (context.DbWorkTypes.Count() == 0)
            {
                context.DbWorkTypes.Add(new DbWorkType { DbWorkTypeId = Guid.NewGuid(), Name = "Bug" });
                context.DbWorkTypes.Add(new DbWorkType { DbWorkTypeId = Guid.NewGuid(), Name = "Feature" });
                context.DbWorkTypes.Add(new DbWorkType { DbWorkTypeId = Guid.NewGuid(), Name = "Customization" });
                context.SaveChanges();
            }

            // Software Types
            if (context.DbSoftwareTypes.Count() == 0)
            {
                context.DbSoftwareTypes.Add(new DbSoftwareType { DbSoftwareTypeId = Guid.NewGuid(), Name = "JM Retail" });
                context.DbSoftwareTypes.Add(new DbSoftwareType { DbSoftwareTypeId = Guid.NewGuid(), Name = "JM Wholesale" });
                context.SaveChanges();
            }



        }

    }
}

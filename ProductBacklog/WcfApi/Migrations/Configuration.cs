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


            if (context.DbUsers.Count() == 0)
            {
                context.DbUsers.Add(new DbUser { DbUserId = Guid.NewGuid(), FirstName = "Charlie", LastName = "Flores Paz", DbGender = maleGender });
                context.DbUsers.Add(new DbUser { DbUserId = Guid.NewGuid(), FirstName = "Arin", LastName = "Avastazarian", DbGender = maleGender });
                context.DbUsers.Add(new DbUser { DbUserId = Guid.NewGuid(), FirstName = "Ana", LastName = "None", DbGender = femaleGender });
                context.DbUsers.Add(new DbUser { DbUserId = Guid.NewGuid(), FirstName = "Sareen", LastName = "None", DbGender = femaleGender });
                context.SaveChanges();
            }
        }

    }
}

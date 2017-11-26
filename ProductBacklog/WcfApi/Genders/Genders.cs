using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.Genders
{
    public class Genders
    {
        public List<Gender> GetAllGenders()
        {
            return new DataContext().DbGenders.ToList().Select(gender => new Gender(gender)).ToList();
        }

        public Gender GetGender(string name)
        {
            return new DataContext().DbGenders.Where(gender => gender.Name == name).ToList().Select(gender => new Gender(gender)).FirstOrDefault();
        }

        public DbGender GetDbGender(DataContext dbContext, Guid dbGenderId)
        {
            return dbContext.DbGenders.FirstOrDefault(gender => gender.DbGenderId == dbGenderId);
        }
    }
}

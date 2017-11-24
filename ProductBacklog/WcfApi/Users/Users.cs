using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.Users
{
    public class Users
    {
        public List<User> GetAllUsers()
        {
            var dbContext = new DataContext();

            return( from u in dbContext.Users.ToList()
                           select new User(u))
                           .ToList();
        }
    }
}

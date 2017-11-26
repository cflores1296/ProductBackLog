using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.UserLogins
{
   [DataContract]
    public class UserLogin
    {
        public UserLogin() { }

        public UserLogin(DbUserLogin dbUserLogin)
        {
            UserLoginId = dbUserLogin.DbUserLoginId;
            User = new User(dbUserLogin.DbUser);
        }

        [DataMember]
        public Guid UserLoginId { set; get; }

        [DataMember]
        public string UserId { set; get; }

        [DataMember]
        public string PasswordHash { set; get; }

        [DataMember]
        public User User { set; get; }

    }
}

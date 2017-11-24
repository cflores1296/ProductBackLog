using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.Users
{
    [DataContract]
    public class User
    {
        public User() {}

        public User(DbUser dbUser)
        {
            UserId = dbUser.UserId;
            FirstName = dbUser.FirstName;
            LastName = dbUser.LastName;
        }

        [DataMember]
        public Guid UserId { set; get; }

        [DataMember]
        public string FirstName { set; get; }

        [DataMember]
        public string LastName { set; get; }

    }


}

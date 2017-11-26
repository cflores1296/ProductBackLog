using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    [DataContract]
    public class User
    {
        public User() {}

        public User(DbUser dbUser)
        {
            UserId = dbUser.DbUserId;
            FirstName = dbUser.FirstName;
            LastName = dbUser.LastName;
            Gender = new Gender(dbUser.DbGender);
        }

        [DataMember]
        public Guid UserId { set; get; }

        [DataMember]
        public string FirstName { set; get; }

        [DataMember]
        public string LastName { set; get; }

        [DataMember]
        public Gender Gender { set; get; }
    }
}

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
    public class RemovedUserLogin
    {
        public RemovedUserLogin() { }

        public RemovedUserLogin(DbRemovedUserLogin dbRemovedUserLogin)
        {
            RemovedUserLoginId = dbRemovedUserLogin.DbRemovedUserLoginId;
            DateRemoved = dbRemovedUserLogin.DateRemoved;
            RemovedByUser = new User(dbRemovedUserLogin.DbRemovedByUser);
            UserLogin = new UserLogin(dbRemovedUserLogin.DbUserLogin);
        }

        [DataMember]
        public Guid RemovedUserLoginId { set; get; }

        [DataMember]
        public DateTime DateRemoved { set; get; }

        [DataMember]
        public User RemovedByUser { set; get; }

        [DataMember]
        public UserLogin UserLogin { set; get; }
    }
}

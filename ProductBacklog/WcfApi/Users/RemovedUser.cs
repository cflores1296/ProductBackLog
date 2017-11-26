using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WcfApi.DataAccessLayer
{
    [DataContract]
    public class RemovedUser
    {
        public RemovedUser() { }

        public RemovedUser(DbRemovedUser dbRemovedUser)
        {
            RemovedUserId = dbRemovedUser.DbRemovedUserId;
            DateRemoved = dbRemovedUser.DateRemoved;
            RemovedByUser = new User(dbRemovedUser.DbRemovedByUser);
            User = new User(dbRemovedUser.DbUser);
        }

        [DataMember]
        public Guid RemovedUserId { set; get; }
        
        [DataMember]
        public DateTime DateRemoved { set; get; }

        [DataMember]
        public User RemovedByUser { set; get; }

        [DataMember]
        public User User { set; get; }
    }
}

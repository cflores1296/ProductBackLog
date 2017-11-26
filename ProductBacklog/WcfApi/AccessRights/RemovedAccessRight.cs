using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.AccessRights
{
    public class RemovedAccessRight
    {
        public RemovedAccessRight() { }

        public RemovedAccessRight(DbRemovedAccessRight dbRemovedAccessRight)
        {
            RemovedAccessRightId = dbRemovedAccessRight.DbRemovedAccessRightId;
            DateRemoved = dbRemovedAccessRight.DateRemoved;
            RemovedByUser = new User(dbRemovedAccessRight.DbRemovedByUser);
            AccessRight = new AccessRight(dbRemovedAccessRight.DbAccessRight);
        }

        [DataMember]
        public Guid RemovedAccessRightId { set; get; }

        [DataMember]
        public DateTime DateRemoved { set; get; }

        [DataMember]
        public User RemovedByUser { set; get; }

        [DataMember]
        public AccessRight AccessRight { set; get; }
    }
}

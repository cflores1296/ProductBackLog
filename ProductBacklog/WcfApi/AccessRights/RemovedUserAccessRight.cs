using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.AccessRights
{
    [DataContract]
    public class RemovedUserAccessRight
    {
        public RemovedUserAccessRight() { }

        public RemovedUserAccessRight(DbRemovedUserAccessRight dbRemovedUserAccessRight)
        {
            RemovedUserAccessRightId = dbRemovedUserAccessRight.DbRemovedUserAccessRightId;
            DateRemoved = dbRemovedUserAccessRight.DateRemoved;
            RemovedByUser = new User(dbRemovedUserAccessRight.DbRemovedByUser);
            UserAccessRight = new UserAccessRight(dbRemovedUserAccessRight.DbUserAccessRight);
        }

        [DataMember]
        public Guid RemovedUserAccessRightId { set; get; }

        [DataMember]
        public DateTime DateRemoved { set; get; }

        [DataMember]
        public User RemovedByUser { set; get; }

        [DataMember]
        public UserAccessRight UserAccessRight { set; get; }
    }
}


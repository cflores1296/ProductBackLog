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
    public class UserAccessRight
    {
        public UserAccessRight() { }

        public UserAccessRight(DbUserAccessRight dbUserAccessRight)
        {
            UserAccessRightId = dbUserAccessRight.DbUserAccessRightId;
            AccessRight = new AccessRight(dbUserAccessRight.DbAccessRight);
            User = new User(dbUserAccessRight.DbUser);
        }

        [DataMember]
        public Guid UserAccessRightId { set; get; }

        [DataMember]
        public User User { set; get; }

        [DataMember]
        public AccessRight AccessRight { set; get; }
    }
}

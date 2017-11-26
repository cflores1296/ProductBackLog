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
    public class AccessRight
    {
        public AccessRight() { }

        public AccessRight(DbAccessRight dbAccessRight)
        {
            AccessRightId = dbAccessRight.DbAccessRightId;
            Name = dbAccessRight.Name;
        }

        [DataMember]
        public Guid AccessRightId { set; get; }

        [DataMember]
        public string Name { set; get; }

    }
}

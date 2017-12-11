using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.SoftwareTypes
{
    [DataContract]
    public class SoftwareType
    {
        public SoftwareType() { }

        public SoftwareType(DbSoftwareType dbSoftwareType)
        {
            SoftwareTypeId = dbSoftwareType.DbSoftwareTypeId;
            Name = dbSoftwareType.Name;
        }


        [DataMember]
        public Guid SoftwareTypeId { set; get; }

        [DataMember]
        public string Name { set; get; }
    }
}

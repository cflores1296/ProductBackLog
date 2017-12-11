using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.WorkTypes
{
    [DataContract]
    public class WorkType
    {
        public WorkType() { }

        public WorkType(DbWorkType dbWorkType)
        {
            WorkTypeId = dbWorkType.DbWorkTypeId;
            Name = dbWorkType.Name;
        }


        [DataMember]
        public Guid WorkTypeId { set; get; }

        [DataMember]
        public string Name { set; get; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.WorkStatuses
{

    [DataContract]
    public class WorkStatus
    {
        public WorkStatus() { }

        public WorkStatus(DbWorkStatus dbWorkStatus)
        {
            WorkStatusId = dbWorkStatus.DbWorkStatusId;
            Name = dbWorkStatus.Name;
        }


        [DataMember]
        public Guid WorkStatusId { set; get; }

        [DataMember]
        public string Name { set; get; }
    }
}
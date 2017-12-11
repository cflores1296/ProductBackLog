using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;
using WcfApi.Users;

namespace WcfApi.WorkRequests
{
    [DataContract]
    public class RemovedWorkRequest
    {
        public RemovedWorkRequest() { }

        public RemovedWorkRequest(DbRemovedWorkRequest dbRemovedWorkRequest)
        {
            RemovedWorkRequestId = dbRemovedWorkRequest.DbRemovedWorkRequestId;
            DateRemoved = dbRemovedWorkRequest.DateRemoved;
            RemovedByUser = new User(dbRemovedWorkRequest.DbRemovedByUser);
            WorkRequest = new WorkRequest(dbRemovedWorkRequest.DbWorkRequest);
        }

        [DataMember]
        public Guid RemovedWorkRequestId { set; get; }

        [DataMember]
        public DateTime DateRemoved { set; get; }

        [DataMember]
        public User RemovedByUser { set; get; }

        [DataMember]
        public WorkRequest WorkRequest { set; get; }
    }
}

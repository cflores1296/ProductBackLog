using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.Customers;
using WcfApi.DataAccessLayer;
using WcfApi.SoftwareTypes;
using WcfApi.Users;
using WcfApi.WorkStatuses;
using WcfApi.WorkTypes;

namespace WcfApi.WorkRequests
{
    [DataContract]
    public class WorkRequest
    {
        public WorkRequest() { }

        public WorkRequest(DbWorkRequest dbWorkRequest)
        {
            WorkRequestId = dbWorkRequest.DbWorkRequestId;
            RequestNumber = dbWorkRequest.RequestNumber;
            RequestDate = dbWorkRequest.RequestDate;
            Description = dbWorkRequest.Description;
            WorkType = new WorkType(dbWorkRequest.DbWorkType);
            WorkStatus = new WorkStatus(dbWorkRequest.DbWorkStatus);
            SoftwareType = new SoftwareType(dbWorkRequest.DbSoftwareType);
            RequestNumber = dbWorkRequest.RequestNumber;
            CreatedByUser = new User(dbWorkRequest.DbCreatedByUser);
            UsersAssigned = dbWorkRequest.DbUserWorkRequests.Select(dbUserWorkRequest => dbUserWorkRequest.DbUser).ToList().Select(dbUser => new User(dbUser)).ToList();
            Customer = new Customer(dbWorkRequest.DbCustomer);
        }


        [DataMember]
        public Guid WorkRequestId { set; get; }

        [DataMember]
        public long RequestNumber { set; get; }

        [DataMember]
        public DateTime RequestDate { set; get; }

        [DataMember]
        public string Description { set; get; }

        [DataMember]
        public WorkType WorkType { set; get; }

        [DataMember]
        public WorkStatus WorkStatus { set; get; }

        [DataMember]
        public SoftwareType SoftwareType { set; get; }

        [DataMember]
        public User CreatedByUser { set; get; }

        [DataMember]
        public Customer Customer { set; get; }

        [DataMember]
        public List<User> UsersAssigned { set; get; }
    }
}
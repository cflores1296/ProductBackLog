using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;
using WcfApi.Users;

namespace WcfApi.Customers
{
    public class RemovedCustomer
    {
        public RemovedCustomer() { }

        public RemovedCustomer(DbRemovedCustomer dbRemovedCustomer)
        {
            RemovedCustomerId = dbRemovedCustomer.DbRemovedCustomerId;
            DateRemoved = dbRemovedCustomer.DateRemoved;
            RemovedByUser = new User(dbRemovedCustomer.DbRemovedByUser);
            Customer = new Customer(dbRemovedCustomer.DbCustomer);
        }

        [DataMember]
        public Guid RemovedCustomerId { set; get; }

        [DataMember]
        public DateTime DateRemoved { set; get; }

        [DataMember]
        public User RemovedByUser { set; get; }

        [DataMember]
        public Customer Customer { set; get; }
    }
}

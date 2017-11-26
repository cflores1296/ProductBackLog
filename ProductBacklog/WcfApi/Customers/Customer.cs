using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.Customers
{
 
        [DataContract]
        public class Customer
        {
            public Customer() { }

            public Customer(DbCustomer dbCustomer)
            {
                CustomerId = dbCustomer.DbCustomerId;
                Name = dbCustomer.Name;
            }

            [DataMember]
            public Guid CustomerId { set; get; }

            [DataMember]
            public string Name { set; get; }

        }
    }

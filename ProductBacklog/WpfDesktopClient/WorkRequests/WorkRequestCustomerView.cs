using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.Customers;

namespace WpfDesktopClient.WorkRequests
{
    public class WorkRequestCustomerView
    {
        public Customer customer;
        public WorkRequestCustomerView(Customer customer)
        {
            this.customer = customer;
        }

        public string Customer { get { return customer.Name; } }

        public override string ToString()
        {
            return Customer;
        }
    }
}

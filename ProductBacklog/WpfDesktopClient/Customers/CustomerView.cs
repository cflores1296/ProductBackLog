using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.Customers;
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.Customers
{
    public class CustomerView
    {
        public Customer customer;
        public CustomerView(Customer customer)
        {
            this.customer = customer;
        }

        public string Name { get { return customer.Name; } }

    }
}

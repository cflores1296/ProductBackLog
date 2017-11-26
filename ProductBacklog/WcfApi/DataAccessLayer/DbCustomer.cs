using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbCustomer
    {
        public Guid DbCustomerId { set; get; }

        public string Name { set; get; }

        public virtual DbRemovedCustomer DbRemovedCustomer { set; get; }
    }
}

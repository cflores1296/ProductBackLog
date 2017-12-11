using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbCurrentRequestNumber
    {
        public Guid DbCurrentRequestNumberId { set; get; }

        public long Number { set; get; }
    }
}

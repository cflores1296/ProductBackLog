using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbWorkStatus
    {
        public Guid DbWorkStatusId { set; get; }

        public string Name { set; get; }

        public virtual ICollection<DbWorkRequest> DbWorkRequests { set; get; }
    }
}

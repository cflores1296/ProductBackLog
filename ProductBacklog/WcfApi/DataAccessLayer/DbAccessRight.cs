using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbAccessRight
    {
        public Guid DbAccessRightId { set; get; }
        public string Name { set; get; }
        public virtual ICollection<DbUserAccessRight> DbUserAccessRights { set; get; }
        public virtual DbRemovedAccessRight DbRemovedAccessRight { set; get; }
    }
}

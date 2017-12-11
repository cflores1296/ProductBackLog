using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbWorkRequest
    {
        public Guid DbWorkRequestId { set; get; }

        public long RequestNumber { set; get; }

        public DateTime RequestDate { set; get; }

        public string Description { set; get; }

        [Required]
        public virtual DbWorkType DbWorkType { set; get; }

        [Required]
        public virtual DbWorkStatus DbWorkStatus { set; get; }

        [Required]
        public virtual DbSoftwareType DbSoftwareType { set; get; }

        [Required]
        public virtual DbUser DbCreatedByUser { set; get; }

        public virtual ICollection<DbUserWorkRequest> DbUserWorkRequests { set; get; }

        public virtual DbRemovedWorkRequest DbRemovedWorkRequest { set; get; }

        
        public virtual DbCustomer DbCustomer { set; get; }
    }
}

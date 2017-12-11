using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbWorkRequestApproval
    {
        public Guid DbWorkRequestApprovalId { set; get; }
    
        public DateTime ApprovalDate { set; get; }

        [Required]
        public virtual DbUser DbUser { set; get; }

        [Required]
        public virtual DbWorkRequest DbWorkRequest { set; get; }
    }
}

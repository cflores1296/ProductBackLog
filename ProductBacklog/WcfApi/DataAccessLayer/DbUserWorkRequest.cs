using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbUserWorkRequest
    {
        public Guid DbUserWorkRequestId { set; get; }

        [Required]
        public virtual DbUser DbUser { set; get; }

        public virtual DbWorkRequest DbWorkRequest { set; get; }
    }
}

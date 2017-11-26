using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbRemovedCustomer
    {
        public Guid DbRemovedCustomerId { set; get; }

        public DateTime DateRemoved { set; get; }

        [Required]
        public virtual DbUser DbRemovedByUser { set; get; }

        [Required]
        public virtual DbCustomer DbCustomer { set; get; }
    }
}

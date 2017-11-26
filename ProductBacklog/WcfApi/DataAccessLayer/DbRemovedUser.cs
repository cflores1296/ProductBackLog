using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WcfApi.DataAccessLayer
{

    public class DbRemovedUser
    {
        public Guid DbRemovedUserId { set; get; }

        public DateTime DateRemoved { set; get; }


        public virtual DbUser DbRemovedByUser { set; get; }

        [Required]
        public virtual DbUser DbUser { set; get; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbRemovedUserLogin
    {
        public Guid DbRemovedUserLoginId { set; get; }

        public DateTime DateRemoved { set; get; }

        [Required]
        public virtual DbUser DbRemovedByUser { set; get; }
        [Required]
        public virtual DbUserLogin DbUserLogin { set; get; }
    }
}

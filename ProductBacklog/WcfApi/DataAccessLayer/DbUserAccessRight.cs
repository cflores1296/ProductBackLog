using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbUserAccessRight
    {
        public Guid DbUserAccessRightId { set; get; }
        [Required]
        public virtual DbAccessRight DbAccessRight { set; get; }
        [Required]
        public virtual DbUser DbUser { set; get; }
        public virtual DbRemovedUserAccessRight DbRemovedUserAccessRight { set; get; }
    }
}

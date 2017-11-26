using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbUserLogin
    {
        public Guid DbUserLoginId { set; get; }

        public string UserId { set; get; }
        public string PasswordHash { set; get; }

        [Required]
        public virtual DbUser DbUser { set; get; }
        public virtual DbRemovedUserLogin DbRemovedUserLogin { set; get; }
    }
}

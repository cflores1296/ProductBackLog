using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{

    public class DbUser
    {
        public Guid DbUserId { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        [Required]
        public virtual DbGender DbGender { set; get; }

        public virtual DbRemovedUser DbRemovedUser { set; get; }
    }
}

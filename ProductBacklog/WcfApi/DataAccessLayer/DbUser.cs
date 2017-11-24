using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{
    public class DbUser
    {
        [Key]
        public Guid UserId { set; get; }

        public string FirstName { set; get; }

        public string LastName { set; get; }

        public string Comment { set; get; }
    }
}

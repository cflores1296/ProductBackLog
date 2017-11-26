using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.DataAccessLayer
{

    public class DbGender
    {
        public Guid DbGenderId { set; get; }

        public string Name { set; get; }

        public virtual ICollection<User> Users { set; get; }
    }
}

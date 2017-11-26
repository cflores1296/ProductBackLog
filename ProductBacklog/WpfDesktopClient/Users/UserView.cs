using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.Users
{
    public class UserView
    {
        public User user;
        public UserView(User user)
        {
            this.user = user;
        }

        [DisplayName("First Name")]
        public string FirstName { get { return user.FirstName; } }

        [DisplayName("Last Name")]
        public string LastName { get { return user.LastName; } }

        public string Gender { get { return user.Gender.Name; } }
    }
}

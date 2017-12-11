using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.Users;

namespace WpfDesktopClient.WorkRequests
{
    public class WorkRequestUserView
    {
        public User user;
        public WorkRequestUserView(User user)
        {
            this.user = user;
        }

        public string FirstName { get { return user.FirstName; } }

        public string LastName { get { return user.LastName; } }
    }
}

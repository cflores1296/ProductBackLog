using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.UserAccessRights
{
    public class AccessRightView
    {
        public AccessRight accessRight;
        public AccessRightView(AccessRight accessRight)
        {
            this.accessRight = accessRight;
        }

        public string AccessRightName
        {
            get { return accessRight.Name; }
        }
    }
}

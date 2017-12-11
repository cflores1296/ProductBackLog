using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.SoftwareTypes;

namespace WpfDesktopClient.WorkRequests
{
    public class WorkRequestSoftwareTypeView
    {
        public SoftwareType softwareType;
        public WorkRequestSoftwareTypeView(SoftwareType softwareType)
        {
            this.softwareType = softwareType;
        }

        public string SoftwareType { get { return softwareType.Name; } }

        public override string ToString()
        {
            return SoftwareType;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.WorkStatuses;

namespace WpfDesktopClient.WorkRequests
{
    public class WorkRequestWorkStatusView
    {
        public WorkStatus workStatus;
        public WorkRequestWorkStatusView(WorkStatus workStatus)
        {
            this.workStatus = workStatus;
        }

        public string WorkStatus { get { return workStatus.Name; } }

        public override string ToString()
        {
            return WorkStatus;
        }
    }
}

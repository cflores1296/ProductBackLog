using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.WorkTypes;

namespace WpfDesktopClient.WorkRequests
{
    public class WorkRequestWorkTypeView
    {
        public WorkType workType;
        public WorkRequestWorkTypeView(WorkType workType)
        {
            this.workType = workType;
        }

        public string WorkType { get { return workType.Name; } }

        public override string ToString()
        {
            return WorkType;
        }
    }
}

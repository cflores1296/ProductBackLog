using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.WorkRequests;

namespace WpfDesktopClient.WorkRequests
{
    public class WorkRequestView
    {
        public WorkRequest workRequest;
        public WorkRequestView(WorkRequest workRequest)
        {
            this.workRequest = workRequest;
        }


        public string RequestNumber { get { return workRequest.RequestNumber.ToString(); } }

        public string RequestDate { get { return workRequest.RequestDate.ToString("M/d/yyyy"); } }

        public string WorkType { get { return workRequest.WorkType.Name; } }

        public string Status { get { return workRequest.WorkStatus.Name; } }

        public string Software { get { return workRequest.SoftwareType.Name; } }

        public string Customer { get { return workRequest.Customer.Name; } }

        public string Description { get { return workRequest.Description; } }

        public string AssignedTo { get { return string.Join(", ", workRequest.UsersAssigned.Select(user => user.FirstName + " " + user.LastName).ToList()); } }

        public string CreatedBy { get { return workRequest.CreatedByUser.FirstName + " " + workRequest.CreatedByUser.LastName; } }
    }
}

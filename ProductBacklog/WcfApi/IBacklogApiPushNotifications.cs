using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi
{
    public interface IBacklogApiPushNotifications
    {
        // Customers
        [OperationContract(IsOneWay = true)]
        void CustomerWasAddedNotification(Guid customerId);

        [OperationContract(IsOneWay = true)]
        void CustomerWasUpdatedNotification(Guid customerId);

        [OperationContract(IsOneWay = true)]
        void CustomerWasRemovedNotification(Guid removedCustomerId);


        // Users
        [OperationContract(IsOneWay = true)]
        void UserWasAddedNotification(Guid userId);

        [OperationContract(IsOneWay = true)]
        void UserWasUpdatedNotification(Guid userId);

        [OperationContract(IsOneWay = true)]
        void UserWasRemovedNotification(Guid removedUserId);


        // Work Requests
        [OperationContract(IsOneWay = true)]
        void WorkRequestWasAddedNotification(Guid workRequestId);

        [OperationContract(IsOneWay = true)]
        void WorkRequestWasUpdatedNotification(Guid workRequestId);

        [OperationContract(IsOneWay = true)]
        void WorkRequestWasRemovedNotification(Guid removedWorkRequestId);
    }
}

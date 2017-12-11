using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDesktopClient.PushNotifications
{
    // Work Requests
    public delegate void WorkRequestWasAddedPushNotificationEventHandler(Guid workRequestId);
    public delegate void WorkRequestWasUpdatedPushNotificationEventHandler(Guid workRequestId);
    public delegate void WorkRequestWasRemovedPushNotificationEventHandler(Guid removedWorkRequestId);

    // Customers
    public delegate void CustomerWasAddedPushNotificationEventHandler(Guid customerId);
    public delegate void CustomerWasUpdatedPushNotificationEventHandler(Guid customerId);
    public delegate void CustomerWasRemovedPushNotificationEventHandler(Guid customerId);

    // Users
    public delegate void UserWasAddedPushNotificationEventHandler(Guid userId);
    public delegate void UserWasUpdatedPushNotificationEventHandler(Guid userId);
    public delegate void UserWasRemovedPushNotificationEventHandler(Guid removedUserId);

}

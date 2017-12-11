using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.PushNotifications
{
    [CallbackBehavior(UseSynchronizationContext = true)]
    public class BacklogNotifications : IBackLogAPICallback
    {
        Guid instacanceId = Guid.NewGuid();

        SynchronizationContext a = AsyncOperationManager.SynchronizationContext;

        // Events
        public static event CustomerWasAddedPushNotificationEventHandler CustomerWasAddedPushNotification;
        public static event CustomerWasUpdatedPushNotificationEventHandler CustomerWasUpdatedPushNotification;
        public static event CustomerWasRemovedPushNotificationEventHandler CustomerWasRemovedPushNotification;

        public static event WorkRequestWasAddedPushNotificationEventHandler WorkRequestWasAddedPushNotification;
        public static event WorkRequestWasUpdatedPushNotificationEventHandler WorkRequestWasUpdatedPushNotification;
        public static event WorkRequestWasRemovedPushNotificationEventHandler WorkRequestWasRemovedPushNotification;

        public static event UserWasAddedPushNotificationEventHandler UserWasAddedPushNotification;
        public static event UserWasUpdatedPushNotificationEventHandler UserWasUpdatedPushNotification;
        public static event UserWasRemovedPushNotificationEventHandler UserWasRemovedPushNotification;


        public void CustomerWasAddedNotification(Guid customerId)
        {
            CustomerWasAddedPushNotification?.Invoke(customerId);

            Console.WriteLine("Customer was added: " + customerId + " instance Id: " + instacanceId.ToString());

            //OnNewCustomerWasAddedPushNotification(new NewCustomerWasAddedPushNotificationEventArgs(consumerId));
        }

        public void CustomerWasRemovedNotification(Guid removedCustomerId)
        {
            CustomerWasRemovedPushNotification?.Invoke(removedCustomerId);
            Console.WriteLine("Customer was removed: " + removedCustomerId + " instance Id: " + instacanceId.ToString());
        }

        public void CustomerWasUpdatedNotification(Guid customerId)
        {
            CustomerWasUpdatedPushNotification?.Invoke(customerId);
            Console.WriteLine("Customer was updated: " + customerId + " instance Id: " + instacanceId.ToString());
        }

        public void UserWasAddedNotification(Guid userId)
        {
            UserWasAddedPushNotification?.Invoke(userId);
        }

        public void UserWasUpdatedNotification(Guid userId)
        {
            UserWasUpdatedPushNotification?.Invoke(userId);
        }

        public void UserWasRemovedNotification(Guid removedUserId)
        {
            UserWasRemovedPushNotification?.Invoke(removedUserId);
        }

        public void WorkRequestWasAddedNotification(Guid workRequestId)
        {
            WorkRequestWasAddedPushNotification?.Invoke(workRequestId);
        }

        public void WorkRequestWasUpdatedNotification(Guid workRequestId)
        {
            WorkRequestWasUpdatedPushNotification?.Invoke(workRequestId);
        }

        public void WorkRequestWasRemovedNotification(Guid removedWorkRequestId)
        {
            WorkRequestWasRemovedPushNotification?.Invoke(removedWorkRequestId);
        }



        //protected virtual void OnNewCustomerWasAddedPushNotification(NewCustomerWasAddedPushNotificationEventArgs e)
        //{
        //    if (NewCustomerWasAddedPushNotification != null)
        //        NewCustomerWasAddedPushNotification(this, e);
        //}
    }


    //public class NewCustomerWasAddedPushNotificationEventArgs : EventArgs
    //{
    //    public NewCustomerWasAddedPushNotificationEventArgs(Guid customerId)
    //    {
    //        CustomerId = customerId;
    //    }

    //    public Guid CustomerId { set; get; }
    //}
}

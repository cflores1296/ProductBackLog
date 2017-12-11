using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfApi.PushNotifications
{
    public class PushNotificationsRepository
    {
        List<IBacklogApiPushNotifications> notificationSubscribers = new List<IBacklogApiPushNotifications>();
        readonly object lockingObject = new object();

        // Notifications
        public void Subscribe(OperationContext operationContext)
        {
            try
            {
                var subscriber = operationContext.GetCallbackChannel<IBacklogApiPushNotifications>();

                if (subscriber != null)
                {
                    lock (lockingObject)
                    {
                        if (!notificationSubscribers.Contains(subscriber))
                        {
                            notificationSubscribers.Add(subscriber);
                            Console.WriteLine("Subscribed: {0}", subscriber.GetHashCode());
                        }
                    }
                }
            }
            catch
            {
            }
        }

        public void UnSubscribe(OperationContext operationContext)
        {
            try
            {
                var subscriber = operationContext.GetCallbackChannel<IBacklogApiPushNotifications>();

                if (subscriber != null)
                {
                    lock (lockingObject)
                    {
                        notificationSubscribers.Remove(subscriber);
                        Console.WriteLine("Unsubscribed: {0}", subscriber.GetHashCode());
                    }
                }
            }
            catch
            {
            }
        }

        
        public Task NotifyAsync(OperationContext operationContext, Action<IBacklogApiPushNotifications> notifyAction)
        {
            return Task.Run(() => {
                Notify(operationContext, notifyAction);
            });
        }


        public void Notify(OperationContext operationContext, Action<IBacklogApiPushNotifications> notifyAction)
        {
            var subscriberToIgnore = operationContext.GetCallbackChannel<IBacklogApiPushNotifications>();
            
            lock (lockingObject)
            {
                List<IBacklogApiPushNotifications> subscribersToRemove = new List<IBacklogApiPushNotifications>();

                foreach (var subscriber in notificationSubscribers)
                {
                    if (subscriber != null)
                    {
                        if (subscriber == subscriberToIgnore)
                        {
                            continue;
                        }
                    }

                    var iCommunicationObject = subscriber as ICommunicationObject;

                    if (iCommunicationObject != null)
                    {
                        if (iCommunicationObject.State ==  CommunicationState.Opened)
                        {
                            try
                            {
                                notifyAction(subscriber);
                                Console.WriteLine("Pushed Notification To: {0}", subscriber.GetHashCode());
                            }
                            catch
                            {
                                Console.WriteLine("Failed to Push Notification To: {0}", subscriber.GetHashCode());
                                subscribersToRemove.Add(subscriber);
                            }
                        }
                        else
                        {
                            subscribersToRemove.Add(subscriber);
                        }
                    }
                }

                foreach (var subscriber in subscribersToRemove)
                {
                    notificationSubscribers.Remove(subscriber);
                    Console.WriteLine("Removed Subscriber: ", subscriber.GetHashCode());
                }
            }
        }

    }
}

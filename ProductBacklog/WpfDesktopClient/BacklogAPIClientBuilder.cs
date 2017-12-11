using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfDesktopClient.BacklogApi;
using WpfDesktopClient.PushNotifications;

namespace WpfDesktopClient
{
    public static class BacklogAPIClientBuilder
    {
        public static BacklogNotifications notifications;
        static BackLogAPIClient client;

        public static Task<BackLogAPIClient> GetBackLogAPIClientAsync()
        {
            return Task.Run(() => {

                if (client != null)
                {
                    if (client.State == System.ServiceModel.CommunicationState.Faulted)
                    {
                        client = null;
                    }
                }

                if (client == null)
                {
                    notifications = new BacklogNotifications();
                    //notifications.NewCustomerWasAddedPushNotification += Notifications_NewCustomerWasAddedPushNotification;
                    client = new BackLogAPIClient(new System.ServiceModel.InstanceContext(notifications));
                    client.SubscribeToNotificationsAsync();
                }

                return client;
            });
        }
    }
}

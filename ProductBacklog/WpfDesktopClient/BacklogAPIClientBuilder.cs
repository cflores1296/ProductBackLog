using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient
{
    public static class BacklogAPIClientBuilder
    {
        public static Task<BackLogAPIClient> GetBackLogAPIClientAsync()
        {
            return Task.Run(() => {
                return new BackLogAPIClient();
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfApi;

namespace ApiWpfHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost host;
        Uri baseAddress = new Uri("http://localhost:8080/Backlog");

        public MainWindow()
        {
            InitializeComponent();
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartService();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            CloseService();
        }


        void StartService()
        {
            messageTextBlock.Text = "Starting Backlog API..";

            Task.Run(() => {

                // Create the ServiceHost.
                host = new ServiceHost(typeof(BacklogAPI), baseAddress);
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                host.Description.Behaviors.Add(smb);

                // Open the ServiceHost to start listening for messages. Since
                // no endpoints are explicitly configured, the runtime will create
                // one endpoint per base address for each service contract implemented
                // by the service.
                host.Open();


                Dispatcher.InvokeAsync(()=> { messageTextBlock.Text = "The Backlog API is running."; });

            });

        }

        void CloseService()
        {
            if (host.State == CommunicationState.Opened)
            {
                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}

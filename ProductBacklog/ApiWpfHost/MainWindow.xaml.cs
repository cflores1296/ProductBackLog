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
        //Uri baseAddress = new Uri("http://localhost:8080/Backlog");
        Uri baseAddress = new Uri("net.tcp://localhost:8080/Backlog");
        

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

                //Tcp();
                TcpWithConfigFile();
                Dispatcher.InvokeAsync(()=> { messageTextBlock.Text = "The Backlog API is running."; });

            });

        }


        void TcpWithConfigFile()
        {
            host = new ServiceHost(typeof(BacklogAPI));
            host.Open();
        }



        void Tcp()
        {
            var Uri = new Uri("net.tcp://localhost:8081/Backlog");
            var MetadataUri = new Uri("http://localhost:8081/Backlog");
            host = new ServiceHost(typeof(BacklogAPI), Uri);

            var serviceBehavior = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
            if (serviceBehavior == null)
            {
                serviceBehavior = new ServiceMetadataBehavior();
                host.Description.Behaviors.Add(serviceBehavior);
            }

            serviceBehavior.HttpGetEnabled = true;
            serviceBehavior.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

            host.AddServiceEndpoint(typeof(IMetadataExchange), MetadataExchangeBindings.CreateMexTcpBinding(), MetadataUri);

            var binding = new NetTcpBinding(SecurityMode.None);
            host.AddServiceEndpoint(typeof(IBackLogAPI), binding, Uri);
        }



        void MoreTcp()
        {
            string serviceUrl = "net.tcp://localhost:9000/TestService/";

            ServiceHost serviceHost = null;

            try

            {

                Uri uri = new Uri(serviceUrl);

                serviceHost = new ServiceHost(typeof(BacklogAPI), uri);

                NetTcpBinding netTcpBinding = new NetTcpBinding();

                ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();

                serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);

                serviceHost.AddServiceEndpoint(typeof(IMetadataExchange),

                  MetadataExchangeBindings.CreateMexTcpBinding(), "mex");

                serviceHost.AddServiceEndpoint(typeof(IBackLogAPI), netTcpBinding, serviceUrl);

                serviceHost.Open();

                Console.WriteLine("Service started... " + serviceUrl);

            }

            catch (Exception ex)

            {

                serviceHost = null;

                Console.WriteLine("Error starting service" + ex.Message);

            }
            
            host = serviceHost;
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

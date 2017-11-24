using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private async void button_Click(object sender, RoutedEventArgs e)
        //{
        //    messageTextBlock.Text = "In progress...";
        //    var client = await GetBackLogAPIClientAsync();
        //    var message = await client.GetDataAsync(DateTime.Now.Second);
        //    messageTextBlock.Text = message;
        //}


        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var usersWindows = new UsersWindow();
            usersWindows.ShowDialog();
        }


        Task<BackLogAPIClient> GetBackLogAPIClientAsync()
        {
            return Task.Run(() => {
                Thread.Sleep(3000);
                return new BackLogAPIClient();
            });
        }
    }
}

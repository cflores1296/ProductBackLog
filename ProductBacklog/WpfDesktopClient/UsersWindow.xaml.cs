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
using System.Windows.Shapes;

namespace WpfDesktopClient
{
    /// <summary>
    /// Interaction logic for UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "In progress...";
            var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
            var allUsers = await client.GetAllUsersAsync();
            Title = "There are " + allUsers.Count +  " Users";
            dataGrid.ItemsSource = allUsers;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfDesktopClient.Customers;
using WpfDesktopClient.Login;
using WpfDesktopClient.Users;
using WpfDesktopClient.WorkRequests;

namespace WpfDesktopClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : DevExpress.Xpf.Core.ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(500);

            ShowLoginWindow();
        }

        void ShowLoginWindow()
        {
            var loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            loginWindow.ShowDialog();

            if (loginWindow.LoginWasSuccessfull)
            {
                Title = AppGlobals.UserThatIsLoggedin.FirstName + " " + AppGlobals.UserThatIsLoggedin.LastName;
                userGrid.Children.Add(new UsersControl(this));
                customersGrid.Children.Add(new CustomersControl(this));
                workRequestsGrid.Children.Add(new WorkRequestsControl(this));
            }
            else
            {
                Close();
            }
        }
    }
}

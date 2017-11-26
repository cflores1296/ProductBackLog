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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Owner = this;
            loginWindow.ShowDialog();

            if (loginWindow.LoginWasSuccessfull)
            {
                Title = AppGlobals.UserThatIsLoggedin.FirstName + " " + AppGlobals.UserThatIsLoggedin.FirstName;
                userGrid.Children.Add(new UsersControl(this));
                customersGrid.Children.Add(new CustomersControl(this));
            }
            else
            {
                Close();
            }
        }
    }
}

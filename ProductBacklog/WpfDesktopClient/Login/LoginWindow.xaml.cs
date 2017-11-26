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
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.Login
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool LoginWasSuccessfull { set; get; }
        public UserLogin UserLogin { set; get; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // This going to wake up the database, so that login takes little time.
            Task.Run(async()=> {
                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                var result = client.FindUserLoginAsync(".", ".");
            });

            progressBar.Visibility = Visibility.Collapsed;
            userIdTextBox.Focus();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            if (userIdTextBox.Text.Length > 0 && passwordTextBox.Password.Length > 0)
            {
                
                progressBar.Visibility = Visibility.Visible;
                cancelButton.IsEnabled = false;
                loginButton.IsEnabled = false;
                userIdTextBox.IsEnabled = false;
                passwordTextBox.IsEnabled = false;

                            
                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                            
                try
                {
                    var userLogin = await client.FindUserLoginAsync(userIdTextBox.Text.Trim(), passwordTextBox.Password);

                    if (userLogin != null)
                    {
                        var userAccessRights = await client.GetActiveUserAccessRightsAsync(userLogin.User.UserId);

                        // Check to see if user is allowed to login
                        var userIsAllowedToLoginAccessRight = userAccessRights.FirstOrDefault(userAccessRight => userAccessRight.AccessRight.Name.Equals("Can Login"));

                        if (userIsAllowedToLoginAccessRight != null)
                        {
                            LoginWasSuccessfull = true;
                            AppGlobals.UserThatIsLoggedin = userLogin.User;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("You don't have the Access Right to log in.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("The User Id and Password you entered did not match any records in the system.");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                progressBar.Visibility = Visibility.Collapsed;
                cancelButton.IsEnabled = true;
                loginButton.IsEnabled = true;
                userIdTextBox.IsEnabled = true;
                passwordTextBox.IsEnabled = true;
                passwordTextBox.Clear();
                passwordTextBox.Focus();
            }
            else
            {
                MessageBox.Show("One or more fields are empty.");
            }
        }
    }
}

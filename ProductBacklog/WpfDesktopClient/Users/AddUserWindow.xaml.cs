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
using WpfDesktopClient.UserAccessRights;

namespace WpfDesktopClient.Users
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public bool UserWasAdded { set; get; }
        public User User { set; get; }
        SelectAccessRightsControl selectAccessRightsControl;

        public AddUserWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            selectAccessRightsControl = new SelectAccessRightsControl(this);
            accessRightsGrid.Children.Add(selectAccessRightsControl);

            progressBar.Visibility = Visibility.Visible;

            try
            {
                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                var allGenders = await client.GetAllGendersAsync();

                foreach (var gender in allGenders)
                {
                    genderComboBox.Items.Add(new GenderView(gender));
                }
            }
            catch (Exception)
            {
            }

            progressBar.Visibility = Visibility.Collapsed;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (firstNameTextBox.Text.Length > 0 && lastNameTextBox.Text.Length > 0 && genderComboBox.SelectedIndex >=0)
            {
                if (userIdTextBox.Text.Length > 0 && passwordTextBox.Password.Length > 0 && confirmPasswordTextBox.Password.Length> 0)
                {
                    if (passwordTextBox.Password == confirmPasswordTextBox.Password)
                    {
                        if (selectAccessRightsControl.SelectedAccessRights.Count > 0)
                        {
                            progressBar.Visibility = Visibility.Visible;
                            cancelButton.IsEnabled = false;
                            saveButton.IsEnabled = false;

                            var selectedGenderView = genderComboBox.SelectedValue as GenderView;
                            var selectedGender = selectedGenderView.gender;

                            var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                            User = new User { FirstName = firstNameTextBox.Text, LastName = lastNameTextBox.Text, Gender = selectedGender, UserId = Guid.NewGuid() };

                            try
                            {
                                // Add user
                                User = await client.AddUserAsync(User);

                                // Add Login
                                var userLogin = new UserLogin { UserLoginId = Guid.NewGuid(), UserId = userIdTextBox.Text, PasswordHash = passwordTextBox.Password, User = User};
                                userLogin = await client.AddUserLoginAsync(userLogin);

                                // Add Access Rights
                                foreach (var selectedAccessRight in selectAccessRightsControl.SelectedAccessRights)
                                {
                                    var userAccessRight = new UserAccessRight { UserAccessRightId = Guid.NewGuid(), AccessRight = selectedAccessRight, User = User };
                                    userAccessRight = await client.AddUserAccessRightAsync(userAccessRight);
                                }

                                
                                UserWasAdded = true;
                                Close();
                                MessageBox.Show("User was added.");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }

                            progressBar.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            MessageBox.Show("You have not selected any Access Rights for this user.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("The Password and Confirm Password fields do not match.");
                    }
                }
                else
                {
                    MessageBox.Show("One or more fields in Login Info section are empty.");
                }
            }
            else
            {
                MessageBox.Show("One or more fields in Personal Info section are empty.");
            }
        }

       
    }
}

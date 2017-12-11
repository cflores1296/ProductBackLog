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
using WcfApi.AccessRights;
using WcfApi.Genders;
using WcfApi.UserLogins;
using WcfApi.Users;
using WpfDesktopClient.UserAccessRights;

namespace WpfDesktopClient.Users
{
    /// <summary>
    /// Interaction logic for EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : DevExpress.Xpf.Core.ThemedWindow
    {
        public bool UserWasUpdated { set; get; }
        public User User { set; get; }
        SelectAccessRightsControl selectAccessRightsControl;
        UserLogin userLogin;
        List<UserAccessRight> userAccessRights;

        public EditUserWindow(User user)
        {
            InitializeComponent();
            User = user;
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

                
                var userLogins = await client.GetActiveUserLoginsAsync(User.UserId);
                userLogin = userLogins.FirstOrDefault();
                User = userLogin.User;


                userAccessRights = await client.GetActiveUserAccessRightsAsync(User.UserId);

                // Populate the UI
                FirstName = User.FirstName;
                LastName = User.LastName;
                Gender = User.Gender;
                UserId = userLogin.UserId;
                AccessRights = (from userAccessRight in userAccessRights
                                select userAccessRight.AccessRight)
                               .ToList();
            }
            catch
            {
            }

            progressBar.Visibility = Visibility.Collapsed;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string FirstName
        {
            set
            {
                firstNameTextBox.Text = value;
            }

            get
            {
                return firstNameTextBox.Text.Trim();
            }
        }

        public string LastName
        {
            set
            {
                lastNameTextBox.Text = value;
            }

            get
            {
                return lastNameTextBox.Text.Trim();
            }
        }

        public Gender Gender
        {
            set
            {
                foreach (var item in genderComboBox.Items)
                {
                    var genderView = item as GenderView;
                    if (genderView.gender.GenderId == value.GenderId)
                    {
                        genderComboBox.SelectedItem = item;
                        break;
                    }
                }
            }

            get
            {
                Gender gender = null;

                if (genderComboBox.SelectedItem != null)
                {
                    var genderView = genderComboBox.SelectedItem as GenderView;
                    gender = genderView.gender;
                }

                return gender;
            }

        }


        public string UserId
        {
            set
            {
                userIdTextBox.Text = value;
            }

            get
            {
                return userIdTextBox.Text.Trim();
            }
        }

        public string Password
        {
            set
            {
                passwordTextBox.Password = value;
            }

            get
            {
                return passwordTextBox.Password;
            }
        }

        public string ConfirmPassword
        {
            set
            {
                confirmPasswordTextBox.Password = value;
            }

            get
            {
                return confirmPasswordTextBox.Password;
            }
        }

        public List<AccessRight> AccessRights
        {
            set
            {
                selectAccessRightsControl.SelectedAccessRights = value;
            }

            get
            {
               return selectAccessRightsControl.SelectedAccessRights;
            }
        }

        bool Validate()
        {
            bool validationResult = false;

            if (FirstName.Length > 0 && LastName.Length > 0 && Gender != null)
            {
                if (UserId.Length > 0)
                {
                    bool passwordValidationPassed = false;

                    if (Password.Length > 0 || ConfirmPassword.Length > 0)
                    {
                        if (Password == ConfirmPassword)
                        {
                            passwordValidationPassed = true;
                        }
                        else
                        {
                            MessageBox.Show("The Password and Confirm Password fields do not match.");
                        }
                    }
                    else
                    {
                        passwordValidationPassed = true;
                    }

                    if (passwordValidationPassed)
                    {
                        if (AccessRights.Count > 0)
                        {
                            validationResult = true;
                        }
                        else
                        {
                            MessageBox.Show("You have not selected any Access Rights for this user.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("The User Id in the Login Info section is empty.");
                }
            }
            else
            {
                MessageBox.Show("One or more fields in Personal Info section are empty.");
            }

            return validationResult;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }


        async void Save()
        {
            if (Validate())
            {
                progressBar.Visibility = Visibility.Visible;
                cancelButton.IsEnabled = false;
                saveButton.IsEnabled = false;

                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                try
                {
                    // Update User
                    var updatedUser = await client.GetUserAsync(User.UserId);
                    updatedUser.FirstName = FirstName;
                    updatedUser.LastName = LastName;
                    updatedUser.Gender = Gender;
                    updatedUser = await client.UpdateUserAsync(updatedUser);

                    // Update Login
                    var updatedUserLogin = await client.GetUserLoginAsync(userLogin.UserLoginId);
                    updatedUserLogin.UserId = UserId;

                    if (Password.Length > 0)
                    {
                        updatedUserLogin.PasswordHash = Password;
                    }

                    updatedUserLogin = await client.UpdateUserLoginAsync(updatedUserLogin);


                    // Remove User Access Rights
                    var userAccessRightsToRemove = userAccessRights
                        .Where(userAccessRight => !AccessRights
                        .Select(accessRight => accessRight.AccessRightId)
                        .Contains(userAccessRight.AccessRight.AccessRightId))
                        .ToList();
                    
                    // Remove User Access Rights
                    foreach (var userAccessRightToRemove in userAccessRightsToRemove)
                    {
                        var removedUserAccessRight = await client.RemoveUserAccessRightAsync(new RemovedUserAccessRight { DateRemoved = DateTime.Now, RemovedByUser= AppGlobals.UserThatIsLoggedin, RemovedUserAccessRightId = Guid.NewGuid(), UserAccessRight = userAccessRightToRemove });
                    }


                    // Remove User Access Rights
                    var userAccessRightsToAdd = AccessRights
                        .Where(accessRight => !userAccessRights
                        .Select(userAccessRight => userAccessRight.AccessRight.AccessRightId)
                        .Contains(accessRight.AccessRightId))
                        .ToList();

                    // Add User Access Rights
                    foreach (var userAccessRightToAdd in userAccessRightsToAdd)
                    {
                        var addedUserAccessRight = await client.AddUserAccessRightAsync(new UserAccessRight { UserAccessRightId = Guid.NewGuid(), User = updatedUser, AccessRight = userAccessRightToAdd });
                    }

                    User = updatedUser;

                    UserWasUpdated = true;
                    Close();
                    MessageBox.Show("User was updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                progressBar.Visibility = Visibility.Collapsed;
            }
        }

    }
}

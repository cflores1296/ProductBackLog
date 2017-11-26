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
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.Users
{
    /// <summary>
    /// Interaction logic for UsersControl.xaml
    /// </summary>
    public partial class UsersControl : UserControl
    {
        Window owner;
        public List<UserView> Users = new List<UserView>();
        

        public UsersControl(Window owner)
        {
            InitializeComponent();
            this.owner = owner;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Users;
            RefreshUsersList();
        }
        





        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddUserWindow();
            addUserWindow.Owner = owner;
            addUserWindow.ShowDialog();

            if (addUserWindow.UserWasAdded)
            {
                RefreshUsersList();
            }
        }

        private void editUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var user = dataGrid.SelectedItem as User;


            }
        }

        private async void removeUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Remove selected items?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                        foreach (var selectedItem in dataGrid.SelectedItems)
                        {
                            var selectedUserView = selectedItem as UserView;

                            if (selectedUserView != null)
                            {
                                var user = selectedUserView.user;

                                var removedUser = new RemovedUser { RemovedUserId = Guid.NewGuid(), DateRemoved = DateTime.Now, RemovedByUser = AppGlobals.UserThatIsLoggedin, User = user };

                                await client.RemoveUserAsync(removedUser);
                            }
                        }

                        RefreshUsersList();
                        MessageBox.Show("Done.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            else
            {
                MessageBox.Show("You have not selected any items.");
            }
        }


        async void RefreshUsersList()
        {
            progressBar.Visibility = Visibility.Visible;
            try
            {
                Users.Clear();
                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                var allUsers = await client.GetAllUsersAsync();

                foreach (var user in allUsers)
                {
                    Users.Add(new UserView(user));
                }

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = Users;
            }
            catch (Exception)
            {
                
            }

            progressBar.Visibility = Visibility.Collapsed;
            
        }
    }
}

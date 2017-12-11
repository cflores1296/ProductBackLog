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
using WcfApi.Users;
using WpfDesktopClient.BacklogApi;
using WpfDesktopClient.PushNotifications;

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
            this.Loaded += UserControl_Loaded;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= UserControl_Loaded;

            dataGrid.ItemsSource = Users;
            RefreshUsersList();

            BacklogNotifications.UserWasAddedPushNotification += OnUserWasAddedPushNotification;
            BacklogNotifications.UserWasUpdatedPushNotification += OnUserWasUpdatedPushNotification;
            BacklogNotifications.UserWasRemovedPushNotification += OnUserWasRemovedPushNotification;
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
                var userView = dataGrid.SelectedItem as UserView;

                var user = userView.user;

                var editUserWindow = new EditUserWindow(user);
                editUserWindow.Owner = owner;
                editUserWindow.ShowDialog();

                if (editUserWindow.UserWasUpdated)
                {
                    RefreshUsersList();
                }
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
                var allUsers = await client.GetAllActiveUsersAsync();

                foreach (var user in allUsers)
                {
                    Add(user);
                }

                dataGrid.RefreshData();
            }
            catch
            {
                
            }

            progressBar.Visibility = Visibility.Collapsed;
            
        }






        void Add(User user)
        {
            Users.Add(new UserView(user));
        }

        void Update(User user)
        {
            var userView = Users.FirstOrDefault(c => c.user.UserId == user.UserId);

            if (userView != null)
            {
                userView.user = user;
                dataGrid.RefreshData();
            }
        }

        void Remove(User user)
        {
            var userView = Users.FirstOrDefault(c => c.user.UserId == user.UserId);

            if (userView != null)
            {
                Users.Remove(userView);
            }
        }


        public void OnUserWasAddedPushNotification(Guid userId)
        {
            Dispatcher.Invoke(new Action(async () => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                    var user = await client.GetUserAsync(userId);
                    Add(user);
                    dataGrid.RefreshData();
                }
                catch
                {
                }

            }));
        }

        public void OnUserWasUpdatedPushNotification(Guid userId)
        {
            Dispatcher.Invoke(new Action(async () => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                    var user = await client.GetUserAsync(userId);

                    Update(user);
                    dataGrid.RefreshData();
                }
                catch
                {
                }
            }));
        }

        public void OnUserWasRemovedPushNotification(Guid removedUserId)
        {
            Dispatcher.Invoke(new Action(async () => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                    var removedUser = await client.GetRemovedUserAsync(removedUserId);

                    Remove(removedUser.User);
                    dataGrid.RefreshData();
                }
                catch
                {
                }

            }));
        }


    }
}

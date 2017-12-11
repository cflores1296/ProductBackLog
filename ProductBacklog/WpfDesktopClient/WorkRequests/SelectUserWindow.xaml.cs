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
using WcfApi.Users;
using WpfDesktopClient.Users;

namespace WpfDesktopClient.WorkRequests
{
    /// <summary>
    /// Interaction logic for SelectUserWindow.xaml
    /// </summary>
    public partial class SelectUserWindow : DevExpress.Xpf.Core.ThemedWindow
    {
        public List<User> SelectedUsers { set; get; }

        public SelectUserWindow()
        {
            InitializeComponent();
            SelectedUsers = new List<User>();
        }

        public List<WorkRequestUserView> users = new List<WorkRequestUserView>();




        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUsersList();
        }






        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void enterButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                foreach (var selectedItem in dataGrid.SelectedItems)
                {
                    var selectedWorkRequestUserView = selectedItem as WorkRequestUserView;

                    if (selectedWorkRequestUserView != null)
                    {
                        SelectedUsers.Add(selectedWorkRequestUserView.user);
                    }
                }

                Close();
            }
            else
            {
                MessageBox.Show("You have not selected any items.");
            }
        }







        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new AddUserWindow();
            addUserWindow.Owner = this;
            addUserWindow.ShowDialog();

            if (addUserWindow.UserWasAdded)
            {
                RefreshUsersList();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var userView = dataGrid.SelectedItem as WorkRequestUserView;

            }
        }

        private async void removeButton_Click(object sender, RoutedEventArgs e)
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
                            var selectedWorkRequestUserView = selectedItem as WorkRequestUserView;

                            if (selectedWorkRequestUserView != null)
                            {
                                var user = selectedWorkRequestUserView.user;

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
                users.Clear();
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = users;

                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                var allUsers = await client.GetAllUsersAsync();

                foreach (var user in allUsers)
                {
                    users.Add(new WorkRequestUserView(user));
                }

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = users;
            }
            catch (Exception)
            {

            }

            progressBar.Visibility = Visibility.Collapsed;

        }
    }
}
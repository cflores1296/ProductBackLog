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

namespace WpfDesktopClient.WorkRequests
{
    /// <summary>
    /// Interaction logic for SelectUsersControl.xaml
    /// </summary>

    public partial class SelectUsersControl : UserControl
    {
        Window owner;
        List<User> selectedUsers = new List<User>();
        public List<User> SelectedUsers
        {
            set
            {
                selectedUsers = value;
                RefreshUsersList();
            }

            get
            {
                return selectedUsers;
            }
        }

        public SelectUsersControl(Window owner)
        {
            InitializeComponent();
            this.owner = owner;
        }

        
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var selectUserWindow = new SelectUserWindow();
            selectUserWindow.Owner = owner;
            selectUserWindow.ShowDialog();

            if (selectUserWindow.SelectedUsers.Count > 0)
            {
                foreach (var selectedUser in selectUserWindow.SelectedUsers)
                {
                    if (!SelectedUsers.Contains(selectedUser))
                    {
                        SelectedUsers.Add(selectedUser);
                    }
                }

                RefreshUsersList();
            }
        }


        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItems.Count > 0)
            {
                MessageBoxResult result = MessageBox.Show("Remove selected items?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    int removedItemsCount = 0;

                    try
                    {

                        foreach (var selectedItem in dataGrid.SelectedItems)
                        {
                            var selectedWorkRequestUserView = selectedItem as WorkRequestUserView;

                            if (selectedWorkRequestUserView != null)
                            {
                                var user = selectedWorkRequestUserView.user;

                                SelectedUsers.Remove(user);

                                removedItemsCount++;
                            }
                        }

                        RefreshUsersList();
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


        void RefreshUsersList()
        {

            var users = new List<WorkRequestUserView>();

            foreach (var user in SelectedUsers)
            {
                users.Add(new WorkRequestUserView(user));
            }

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = users;

        }
    }
}

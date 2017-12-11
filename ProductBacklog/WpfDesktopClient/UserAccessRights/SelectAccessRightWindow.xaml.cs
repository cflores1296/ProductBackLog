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
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.UserAccessRights
{
    /// <summary>
    /// Interaction logic for SelectAccessRightWindow.xaml
    /// </summary>
    public partial class SelectAccessRightWindow : DevExpress.Xpf.Core.ThemedWindow
    {
        public List<AccessRight> SelectedAccessRights { set; get; }

        public SelectAccessRightWindow()
        {
            InitializeComponent();
            SelectedAccessRights = new List<AccessRight>();
        }

        public List<AccessRightView> accessRights = new List<AccessRightView>();




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
                    var selectedAccessRightView = selectedItem as AccessRightView;

                    if (selectedAccessRightView != null)
                    {
                        SelectedAccessRights.Add(selectedAccessRightView.accessRight);
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
            var addAccessRightWindow = new AddAccessRightWindow();
            addAccessRightWindow.Owner = this;
            addAccessRightWindow.ShowDialog();

            if (addAccessRightWindow.AccessRightWasAdded)
            {
                RefreshUsersList();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var accessRightView = dataGrid.SelectedItem as AccessRightView;

                if (accessRightView != null)
                {
                    var accessRight = accessRightView.accessRight;

                    var editAccessRightWindow = new EditAccessRightWindow(accessRight);
                    editAccessRightWindow.Owner = this;
                    editAccessRightWindow.ShowDialog();

                    if (editAccessRightWindow.AccessRightWasUpdated)
                    {
                        RefreshUsersList();
                    }
                }
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
                            var selectedAccessRightView = selectedItem as AccessRightView;

                            if (selectedAccessRightView != null)
                            {
                                var accessRight = selectedAccessRightView.accessRight;

                                var removedAccessRight = new RemovedAccessRight { RemovedAccessRightId = Guid.NewGuid(), DateRemoved = DateTime.Now, RemovedByUser = AppGlobals.UserThatIsLoggedin, AccessRight = accessRight };

                                await client.RemoveAccessRightAsync(removedAccessRight);
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
                accessRights.Clear();
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = accessRights;

                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                var allAccessRights = await client.GetAllAccessRightsAsync();

                foreach (var accessRight in allAccessRights)
                {
                    accessRights.Add(new AccessRightView(accessRight));
                }

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = accessRights;
            }
            catch (Exception)
            {

            }

            progressBar.Visibility = Visibility.Collapsed;

        }
    }
}
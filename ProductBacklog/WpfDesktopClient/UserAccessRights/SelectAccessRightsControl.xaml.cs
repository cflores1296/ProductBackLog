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

namespace WpfDesktopClient.UserAccessRights
{
    /// <summary>
    /// Interaction logic for SelectAccessRightsControl.xaml
    /// </summary>
    public partial class SelectAccessRightsControl : UserControl
    {
        Window owner;
        public List<AccessRight> SelectedAccessRights { set; get; }

        public SelectAccessRightsControl(Window owner)
        {
            InitializeComponent();
            this.owner = owner;
            SelectedAccessRights = new List<AccessRight>();
        }

        
        

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var selectAccessRightWindow = new SelectAccessRightWindow();
            selectAccessRightWindow.Owner = owner;
            selectAccessRightWindow.ShowDialog();

            if (selectAccessRightWindow.SelectedAccessRights.Count > 0)
            {
                foreach (var selectedAccessRight in selectAccessRightWindow.SelectedAccessRights)
                {
                    if (!SelectedAccessRights.Contains(selectedAccessRight))
                    {
                        SelectedAccessRights.Add(selectedAccessRight);
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
                            var selectedAccessRightView = selectedItem as AccessRightView;

                            if (selectedAccessRightView != null)
                            {
                                var accessRight = selectedAccessRightView.accessRight;

                                SelectedAccessRights.Remove(accessRight);
                                
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

            var accessRights = new List<AccessRightView>();

            foreach (var accessRight in SelectedAccessRights)
            {
                accessRights.Add(new AccessRightView(accessRight));
            }

            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = accessRights;
           
        }
    }
}
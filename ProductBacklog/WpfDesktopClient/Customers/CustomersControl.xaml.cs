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

namespace WpfDesktopClient.Customers
{
    /// <summary>
    /// Interaction logic for CustomersControl.xaml
    /// </summary>
    public partial class CustomersControl : UserControl
    {
        Window owner;
        public List<CustomerView> Customers = new List<CustomerView>();


        public CustomersControl(Window owner)
        {
            InitializeComponent();
            this.owner = owner;
        }

        private void customersControl_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = Customers;
            RefreshCustomersList();
        }






        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var addCustomerWindow = new AddCustomerWindow();
            addCustomerWindow.Owner = owner;
            addCustomerWindow.ShowDialog();

            if (addCustomerWindow.CustomerWasAdded)
            {
                RefreshCustomersList();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var customer = dataGrid.SelectedItem as Customer;


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
                            var selectedCustomerView = selectedItem as CustomerView;

                            if (selectedCustomerView != null)
                            {
                                var customer = selectedCustomerView.customer;

                                var removedCustomer = new RemovedCustomer { RemovedCustomerId = Guid.NewGuid(), DateRemoved = DateTime.Now, RemovedByUser = AppGlobals.UserThatIsLoggedin, Customer = customer };

                                await client.RemoveCustomerAsync(removedCustomer);
                            }
                        }

                        RefreshCustomersList();
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


        async void RefreshCustomersList()
        {
            progressBar.Visibility = Visibility.Visible;
            try
            {
                Customers.Clear();
                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                var allCustomers = await client.GetAllCustomersAsync();

                foreach (var customer in allCustomers)
                {
                    Customers.Add(new CustomerView(customer));
                }

                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = Customers;
            }
            catch (Exception)
            {

            }

            progressBar.Visibility = Visibility.Collapsed;

        }
    }
}

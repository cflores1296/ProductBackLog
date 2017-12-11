using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using WcfApi.Customers;
using WpfDesktopClient.BacklogApi;
using WpfDesktopClient.PushNotifications;

namespace WpfDesktopClient.Customers
{
    /// <summary>
    /// Interaction logic for CustomersControl.xaml
    /// </summary>
    public partial class CustomersControl : UserControl
    {
        Window owner;
        public BindingList<CustomerView> Customers = new BindingList<CustomerView>();


        public CustomersControl(Window owner)
        {
            InitializeComponent();
            this.owner = owner;
            this.Loaded += Control_Loaded;
        }

        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= Control_Loaded;

            dataGrid.ItemsSource = Customers;
            RefreshCustomersList();
            BacklogNotifications.CustomerWasAddedPushNotification += OnCustomerWasAddedPushNotification;
            BacklogNotifications.CustomerWasUpdatedPushNotification += OnCustomerWasUpdatedPushNotification;
            BacklogNotifications.CustomerWasRemovedPushNotification += OnCustomerWasRemovedPushNotification;
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
                var customerView = dataGrid.SelectedItem as CustomerView;
                var customer = customerView.customer;
                var editCustomerWindow = new EditCustomerWindow(customer);
                editCustomerWindow.Owner = owner;
                editCustomerWindow.ShowDialog();

                if (editCustomerWindow.CustomerWasUpdated)
                {
                    RefreshCustomersList();
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
                var allCustomers = await client.GetAllActiveCustomersAsync();

                foreach (var customer in allCustomers)
                {
                    Add(customer);
                }

                dataGrid.RefreshData();
            }
            catch
            {

            }

            progressBar.Visibility = Visibility.Collapsed;
        }

        void Add(Customer customer)
        {
            Customers.Add(new CustomerView(customer));
        }

        void Update(Customer customer)
        {
            var customerView = Customers.FirstOrDefault(c => c.customer.CustomerId == customer.CustomerId);

            if (customerView != null)
            {
                customerView.customer = customer;
                dataGrid.RefreshData();
            }
        }

        void Remove(Customer customer)
        {
            var customerView = Customers.FirstOrDefault(c => c.customer.CustomerId == customer.CustomerId);

            if (customerView != null)
            {
                Customers.Remove(customerView);
            }
        }


        public void OnCustomerWasAddedPushNotification(Guid customerId)
        {
            Dispatcher.Invoke(new Action(async() => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                    var customer = await client.GetCustomerAsync(customerId);
                    Add(customer);
                    dataGrid.RefreshData();
                }
                catch
                {
                }

            }));
        }

        public void OnCustomerWasUpdatedPushNotification(Guid customerId)
        {
            Dispatcher.Invoke(new Action(async () => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                    var customer = await client.GetCustomerAsync(customerId);

                    Update(customer);
                    dataGrid.RefreshData();
                }
                catch
                {
                }
            }));
        }

        public void OnCustomerWasRemovedPushNotification(Guid removedCustomerId)
        {
            Dispatcher.Invoke(new Action(async () => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                    var removedCustomer = await client.GetRemovedCustomerAsync(removedCustomerId);

                    Remove(removedCustomer.Customer);
                    dataGrid.RefreshData();
                }
                catch
                {
                }

            }));
        }
    }
}

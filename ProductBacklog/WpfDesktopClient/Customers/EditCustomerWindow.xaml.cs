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
using WcfApi.Customers;

namespace WpfDesktopClient.Customers
{
    /// <summary>
    /// Interaction logic for EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : DevExpress.Xpf.Core.ThemedWindow
    {
        public bool CustomerWasUpdated { set; get; }
        public Customer Customer { set; get; }

        public EditCustomerWindow(Customer customer)
        {
            InitializeComponent();
            Customer = customer;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Collapsed;

            CustomerName = Customer.Name;

            customerNameTextBox.Focus();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string CustomerName
        {
            set
            {
                customerNameTextBox.Text = value;
            }

            get
            {
                return customerNameTextBox.Text.Trim();
            }
        }


        bool Validate()
        {
            bool validationResult = false;

            if (customerNameTextBox.Text.Length > 0)
            {
                validationResult = true;
            }
            else
            {
                MessageBox.Show("One or more fields are empty.");
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
                    var updatedCustomer = await client.GetCustomerAsync(Customer.CustomerId);
                    updatedCustomer.Name = CustomerName;

                    updatedCustomer = await client.UpdateCustomerAsync(updatedCustomer);
                    Customer = updatedCustomer;
                    CustomerWasUpdated = true;
                    Close();
                    MessageBox.Show("Customer was updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                progressBar.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("One or more fields are empty.");
            }
        }


    }
}

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
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.Customers
{
    /// <summary>
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        public bool CustomerWasAdded { set; get; }
        public Customer Customer { set; get; }

        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Collapsed;
            customerNameTextBox.Focus();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (customerNameTextBox.Text.Length > 0)
            {
                progressBar.Visibility = Visibility.Visible;
                cancelButton.IsEnabled = false;
                saveButton.IsEnabled = false;

                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                Customer = new Customer { CustomerId = Guid.NewGuid(), Name = customerNameTextBox.Text };

                try
                {
                    Customer = await client.AddCustomerAsync(Customer);
                    CustomerWasAdded = true;
                    Close();
                    MessageBox.Show("Customer was added.");
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

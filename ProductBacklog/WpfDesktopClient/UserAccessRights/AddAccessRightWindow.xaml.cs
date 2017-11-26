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

namespace WpfDesktopClient.UserAccessRights
{
    /// <summary>
    /// Interaction logic for AddAccessRightWindow.xaml
    /// </summary>
    public partial class AddAccessRightWindow : Window
    {
        public bool AccessRightWasAdded { set; get; }
        public AccessRight AccessRight { set; get; }

        public AddAccessRightWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Collapsed;
            accessRightNameTextBox.Focus();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            if (accessRightNameTextBox.Text.Length > 0)
            {
                progressBar.Visibility = Visibility.Visible;
                cancelButton.IsEnabled = false;
                saveButton.IsEnabled = false;

                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                AccessRight = new AccessRight { AccessRightId = Guid.NewGuid(), Name = accessRightNameTextBox.Text };

                try
                {
                    AccessRight = await client.AddAccessRightAsync(AccessRight);
                    AccessRightWasAdded = true;
                    Close();
                    MessageBox.Show("Access Right was added.");
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

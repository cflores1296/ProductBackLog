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

namespace WpfDesktopClient.UserAccessRights
{
    /// <summary>
    /// Interaction logic for EditAccessRightWindow.xaml
    /// </summary>
    public partial class EditAccessRightWindow : DevExpress.Xpf.Core.ThemedWindow
    {
        public bool AccessRightWasUpdated { set; get; }
        public AccessRight AccessRight { set; get; }

        public EditAccessRightWindow(AccessRight accessRight)
        {
            InitializeComponent();
            AccessRight = accessRight;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            progressBar.Visibility = Visibility.Collapsed;
            AccessRightName = AccessRight.Name;
            accessRightNameTextBox.Focus();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string AccessRightName
        {
            set
            {
                accessRightNameTextBox.Text = value;
            }
            get
            {
                return accessRightNameTextBox.Text.Trim();
            }
        }


        bool Validate()
        {
            bool validationResult = false;

            if (accessRightNameTextBox.Text.Length > 0)
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
                if (AccessRightName.Length > 0)
                {
                    progressBar.Visibility = Visibility.Visible;
                    cancelButton.IsEnabled = false;
                    saveButton.IsEnabled = false;

                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                    var updatedAccessRight = client.GetAccessRight(AccessRight.AccessRightId);
                    updatedAccessRight.Name = AccessRightName;

                    try
                    {
                        updatedAccessRight = await client.UpdateAccessRightAsync(updatedAccessRight);
                        AccessRight = updatedAccessRight;
                        AccessRightWasUpdated = true;
                        Close();
                        MessageBox.Show("Access Right was updated.");
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
}


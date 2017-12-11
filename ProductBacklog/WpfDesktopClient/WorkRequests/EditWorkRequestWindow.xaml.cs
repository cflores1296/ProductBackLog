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
using WcfApi.SoftwareTypes;
using WcfApi.Users;
using WcfApi.WorkRequests;
using WcfApi.WorkStatuses;
using WcfApi.WorkTypes;

namespace WpfDesktopClient.WorkRequests
{
    /// <summary>
    /// Interaction logic for EditWorkRequestWindow.xaml
    /// </summary>
    public partial class EditWorkRequestWindow : DevExpress.Xpf.Core.ThemedWindow
    {
        public bool WorkRequestWasUpdated { set; get; }
        public WorkRequest WorkRequest { set; get; }
        SelectUsersControl selectUsersControl;

        public EditWorkRequestWindow(WorkRequest workRequest)
        {
            InitializeComponent();
            WorkRequest = workRequest;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            selectUsersControl = new SelectUsersControl(this);
            assignedToUserGrid.Children.Add(selectUsersControl);

            progressBar.Visibility = Visibility.Visible;

            try
            {
                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                // Status
                var allStatuses = await client.GetAllWorkStatusesAsync();

                foreach (var status in allStatuses)
                {
                    statusComboBox.Items.Add(new WorkRequestWorkStatusView(status));
                }

                // Customer
                var allCustomers = await client.GetAllActiveCustomersAsync();

                foreach (var customer in allCustomers)
                {
                    customerComboBox.Items.Add(new WorkRequestCustomerView(customer));
                }


                // Work type
                var allWorktypes = await client.GetAllWorkTypesAsync();

                foreach (var workType in allWorktypes)
                {
                    workTypeComboBox.Items.Add(new WorkRequestWorkTypeView(workType));
                }



                // Software type
                var allSoftwareTypes = await client.GetAllSoftwareTypesAsync();

                foreach (var softwareType in allSoftwareTypes)
                {
                    softwareComboBox.Items.Add(new WorkRequestSoftwareTypeView(softwareType));
                }


                RequestDate = WorkRequest.RequestDate;
                RequestDescription = WorkRequest.Description;
                WorkStatus = WorkRequest.WorkStatus;
                Customer = WorkRequest.Customer;
                SoftwareType = WorkRequest.SoftwareType;
                WorkType = WorkRequest.WorkType;
                AssignedToUsers = WorkRequest.UsersAssigned;
                

            }
            catch (Exception)
            {
            }

            progressBar.Visibility = Visibility.Collapsed;
        }

        public DateTime? RequestDate
        {
            set
            {
                requestDateTextBox.SelectedDate = value;
            }
            get
            {
                return requestDateTextBox.SelectedDate;
            }
        }

        public WorkStatus WorkStatus
        {
            set
            {
                foreach (var item in statusComboBox.Items)
                {
                    var workStatusView = item as WorkRequestWorkStatusView;
                    if (workStatusView.workStatus.WorkStatusId == value.WorkStatusId)
                    {
                        statusComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            get
            {
                WorkStatus workStatus = null;

                if (statusComboBox.SelectedValue != null)
                {
                    var view = statusComboBox.SelectedValue as WorkRequestWorkStatusView;
                    if (view != null)
                    {
                        workStatus = view.workStatus;
                    }
                }

                return workStatus;
            }
        }

        public Customer Customer
        {
            set
            {
                foreach (var item in customerComboBox.Items)
                {
                    var customerView = item as WorkRequestCustomerView;
                    if (customerView.customer.CustomerId == value.CustomerId)
                    {
                        customerComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            get
            {
                Customer customer = null;

                if (customerComboBox.SelectedValue != null)
                {
                    var view = customerComboBox.SelectedValue as WorkRequestCustomerView;
                    if (view != null)
                    {
                        customer = view.customer;
                    }
                }

                return customer;
            }
        }

        public SoftwareType SoftwareType
        {
            set
            {
                foreach (var item in softwareComboBox.Items)
                {
                    var softwareTypeView = item as WorkRequestSoftwareTypeView;
                    if (softwareTypeView.softwareType.SoftwareTypeId == value.SoftwareTypeId)
                    {
                        softwareComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            get
            {
                SoftwareType softwareType = null;

                if (softwareComboBox.SelectedValue != null)
                {
                    var view = softwareComboBox.SelectedValue as WorkRequestSoftwareTypeView;
                    if (view != null)
                    {
                        softwareType = view.softwareType;
                    }
                }

                return softwareType;
            }
        }



        public WorkType WorkType
        {
            set
            {
                foreach (var item in workTypeComboBox.Items)
                {
                    var workTypeView = item as WorkRequestWorkTypeView;
                    if (workTypeView.workType.WorkTypeId == value.WorkTypeId)
                    {
                        workTypeComboBox.SelectedItem = item;
                        break;
                    }
                }
            }
            get
            {
                WorkType workType = null;

                if (workTypeComboBox.SelectedValue != null)
                {
                    var view = workTypeComboBox.SelectedValue as WorkRequestWorkTypeView;
                    if (view != null)
                    {
                        workType = view.workType;
                    }
                }

                return workType;
            }
        }

        public string RequestDescription
        {
            set
            {
                requestDescriptionTextBox.Text = value;
            }
            get
            {
                return requestDescriptionTextBox.Text.Trim();
            }
        }

        public List<User> AssignedToUsers
        {
            set
            {
                selectUsersControl.SelectedUsers = value;
            }
            get
            {
                List<User> users = new List<User>();

                if (selectUsersControl != null)
                {
                    users = selectUsersControl.SelectedUsers;
                }

                return users;
            }
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveWorkRequest();
        }


        bool Validate()
        {
            bool validationResult = false;

            string message = string.Empty;

            if (RequestDate != null)
            {
                if (WorkStatus != null)
                {
                    if (Customer != null)
                    {
                        if (SoftwareType != null)
                        {
                            if (WorkType != null)
                            {
                                if (RequestDescription.Length > 0)
                                {
                                    validationResult = true;
                                }
                                else
                                {
                                    message = "Please enter the request description.";
                                }
                            }
                            else
                            {
                                message = "Please select the work type.";
                            }
                        }
                        else
                        {
                            message = "Please select the software type.";
                        }
                    }
                    else
                    {
                        message = "Please select the customer.";
                    }
                }
                else
                {
                    message = "Please select the work status.";
                }
            }
            else
            {
                message = "Please enter the request date.";
            }

            if (!validationResult)
            {
                MessageBox.Show(message);
            }

            return validationResult;

        }


        async void SaveWorkRequest()
        {
            if (Validate())
            {
                progressBar.Visibility = Visibility.Visible;
                cancelButton.IsEnabled = false;
                saveButton.IsEnabled = false;

               
                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                var updatedWorkRequest = await client.GetWorkRequestAsync(WorkRequest.WorkRequestId);
                updatedWorkRequest.RequestDate = RequestDate.Value;
                updatedWorkRequest.Description = RequestDescription;
                updatedWorkRequest.Customer = Customer;
                updatedWorkRequest.SoftwareType = SoftwareType;
                updatedWorkRequest.UsersAssigned = AssignedToUsers;
                updatedWorkRequest.WorkStatus = WorkStatus;
                updatedWorkRequest.WorkType = WorkType;

                try
                {
                    // Add workRequest
                    updatedWorkRequest = await client.UpdateWorkRequestAsync(updatedWorkRequest);
                    WorkRequest = updatedWorkRequest;
                    WorkRequestWasUpdated = true;

                    Close();
                    MessageBox.Show("Work request was updated.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                progressBar.Visibility = Visibility.Collapsed;
            }
        }
    }
}

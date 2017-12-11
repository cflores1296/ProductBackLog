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
using WcfApi.WorkRequests;
using WpfDesktopClient.PushNotifications;

namespace WpfDesktopClient.WorkRequests
{
    /// <summary>
    /// Interaction logic for WorkRequestsControl.xaml
    /// </summary>
    public partial class WorkRequestsControl : UserControl
    {
        Window owner;
        public List<WorkRequestView> WorkRequests = new List<WorkRequestView>();
       //BindingList<WorkRequestView> wr = new BindingList<WorkRequestView>();

        public WorkRequestsControl(Window owner)
        {
            InitializeComponent();
            this.owner = owner;
            this.Loaded += Control_Loaded;
        }

        private void Control_Loaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= Control_Loaded;

            dataGrid.ItemsSource = WorkRequests;
            RefreshWorkRequestsList();

            BacklogNotifications.WorkRequestWasAddedPushNotification += OnWorkRequestWasAddedPushNotification;
            BacklogNotifications.WorkRequestWasUpdatedPushNotification += OnWorkRequestWasUpdatedPushNotification;
            BacklogNotifications.WorkRequestWasRemovedPushNotification += OnWorkRequestWasRemovedPushNotification;
        }






        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var addWorkRequestWindow = new AddWorkRequestWindow();
            addWorkRequestWindow.Owner = owner;
            addWorkRequestWindow.ShowDialog();

            if (addWorkRequestWindow.WorkRequestWasAdded)
            {
                RefreshWorkRequestsList();
            }
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem != null)
            {
                var workRequestView = dataGrid.SelectedItem as WorkRequestView;

                var workRequest = workRequestView.workRequest;

                var editWorkRequestWindow = new EditWorkRequestWindow(workRequest);
                editWorkRequestWindow.Owner = owner;
                editWorkRequestWindow.ShowDialog();

                if (editWorkRequestWindow.WorkRequestWasUpdated)
                {
                    RefreshWorkRequestsList();
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
                            var selectedWorkRequestView = selectedItem as WorkRequestView;

                            if (selectedWorkRequestView != null)
                            {
                                var workRequest = selectedWorkRequestView.workRequest;

                                var removedWorkRequest = new RemovedWorkRequest { RemovedWorkRequestId = Guid.NewGuid(), DateRemoved = DateTime.Now, RemovedByUser = AppGlobals.UserThatIsLoggedin, WorkRequest = workRequest };

                                await client.RemoveWorkRequestAsync(removedWorkRequest);
                            }
                        }

                        RefreshWorkRequestsList();
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


        async void RefreshWorkRequestsList()
        {
            progressBar.Visibility = Visibility.Visible;
            try
            {
                WorkRequests.Clear();
                var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                var allWorkRequests = await client.GetAllActiveWorkRequestsAsync();

                foreach (var workRequest in allWorkRequests)
                {
                    Add(workRequest);
                }

                dataGrid.RefreshData();
            }
            catch
            {

            }

            progressBar.Visibility = Visibility.Collapsed;

        }





        void Add(WorkRequest workRequest)
        {
            WorkRequests.Add(new WorkRequestView(workRequest));
        }

        void Update(WorkRequest workRequest)
        {
            var workRequestView = WorkRequests.FirstOrDefault(c => c.workRequest.WorkRequestId == workRequest.WorkRequestId);

            if (workRequestView != null)
            {
                workRequestView.workRequest = workRequest;
                dataGrid.RefreshData();
            }
        }

        void Remove(WorkRequest workRequest)
        {
            var workRequestView = WorkRequests.FirstOrDefault(c => c.workRequest.WorkRequestId == workRequest.WorkRequestId);

            if (workRequestView != null)
            {
                WorkRequests.Remove(workRequestView);
            }
        }


        public void OnWorkRequestWasAddedPushNotification(Guid workRequestId)
        {
            Dispatcher.Invoke(new Action(async () => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                    var workRequest = await client.GetWorkRequestAsync(workRequestId);
                    Add(workRequest);
                    dataGrid.RefreshData();
                }
                catch
                {
                }

            }));
        }

        public void OnWorkRequestWasUpdatedPushNotification(Guid workRequestId)
        {
            Dispatcher.Invoke(new Action(async () => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();
                    var workRequest = await client.GetWorkRequestAsync(workRequestId);

                    Update(workRequest);
                    dataGrid.RefreshData();
                }
                catch
                {
                }
            }));
        }

        public void OnWorkRequestWasRemovedPushNotification(Guid removedWorkRequestId)
        {
            Dispatcher.Invoke(new Action(async () => {
                try
                {
                    var client = await BacklogAPIClientBuilder.GetBackLogAPIClientAsync();

                    var removedWorkRequest = await client.GetRemovedWorkRequestAsync(removedWorkRequestId);

                    Remove(removedWorkRequest.WorkRequest);
                    dataGrid.RefreshData();
                }
                catch
                {
                }

            }));
        }




        void Print()
        {
            dataGrid.View.Print();
        }

        private void printButton_Click(object sender, RoutedEventArgs e)
        {
            Print();
        }








    }
}

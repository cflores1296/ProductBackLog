using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using WcfApi.AccessRights;
using WcfApi.Customers;
using WcfApi.DataAccessLayer;
using WcfApi.Genders;
using WcfApi.PushNotifications;
using WcfApi.SoftwareTypes;
using WcfApi.UserAccessRights;
using WcfApi.UserLogins;
using WcfApi.Users;
using WcfApi.WorkRequests;
using WcfApi.WorkStatuses;
using WcfApi.WorkTypes;

namespace WcfApi
{

    [ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
    public class BacklogAPI : IBackLogAPI
    {
        PushNotificationsRepository pushNotificationsRepository = new PushNotificationsRepository();

        // Notifications
        public void SubscribeToNotifications()
        {
            pushNotificationsRepository.Subscribe(OperationContext.Current);
        }

        public void UnSubscribeFromNotificaitons()
        {
            pushNotificationsRepository.UnSubscribe(OperationContext.Current);
        }




        // Genders
        public List<Gender> GetAllGenders()
        {
            return new Genders.Genders().GetAllGenders();
        }

        public Gender FindGender(string name)
        {
            return new Genders.Genders().FindGender(name);
        }








        // Users
        public User AddUser(User user)
        {
            var result = new UsersRepository().AddUser(user);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.UserWasAddedNotification(result.UserId));

            return result;
        }

        public User UpdateUser(User user)
        {
            var result = new UsersRepository().UpdateUser(user);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.UserWasUpdatedNotification(result.UserId));

            return result;
        }

        public RemovedUser RemoveUser(RemovedUser removedUser)
        {
            var result = new UsersRepository().RemoveUser(removedUser);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.UserWasRemovedNotification(result.RemovedUserId));

            return result;
        }

        public List<User> GetAllUsers()
        {
            return new UsersRepository().GetAllUsers();
        }

        public List<User> GetAllActiveUsers()
        {
            return new UsersRepository().GetAllActiveUsers();
        }

        public List<RemovedUser> GetAllRemovedUsers()
        {
            return new UsersRepository().GetAllRemovedUsers();
        }

        public User GetUser(Guid userId)
        {
            return new UsersRepository().GetUser(userId);
        }

        public RemovedUser GetRemovedUser(Guid removedUserId)
        {
            return new UsersRepository().GetRemovedUser(removedUserId);
        }

        // User Logins

        public UserLogin FindUserLogin(string userId, string password)
        {
            return new UserLoginsRepository().FindUserLogin(userId, password);
        }

        public List<UserLogin> GetAllUserLogins()
        {
            return new UserLoginsRepository().GetAllUserLogins();
        }

        public List<RemovedUserLogin> GetAllRemovedUserLogins()
        {
            return new UserLoginsRepository().GetAllRemovedUserLogins();
        }

        public List<UserLogin> GetAllActiveUserLogins()
        {
            return new UserLoginsRepository().GetAllActiveUserLogins();
        }

        public List<UserLogin> GetUserLogins(Guid userId)
        {
            return new UserLoginsRepository().GetUserLogins(userId);
        }

        public List<RemovedUserLogin> GetRemovedUserLogins(Guid userId)
        {
            return new UserLoginsRepository().GetRemovedUserLogins(userId);
        }

        public List<UserLogin> GetActiveUserLogins(Guid userId)
        {
            return new UserLoginsRepository().GetActiveUserLogins(userId);
        }

        public UserLogin GetUserLogin(Guid userLoginId)
        {
            return new UserLoginsRepository().GetUserLogin(userLoginId);
        }

        public UserLogin AddUserLogin(UserLogin userLogin)
        {
            return new UserLoginsRepository().AddUserLogin(userLogin);
        }

        public UserLogin UpdateUserLogin(UserLogin userLogin)
        {
            return new UserLoginsRepository().UpdateUserLogin(userLogin);
        }

        public RemovedUserLogin RemoveUserLogin(RemovedUserLogin removedUserLogin)
        {
            return new UserLoginsRepository().RemoveUserLogin(removedUserLogin);
        }





        // Access Rights

        public List<AccessRight> GetAllAccessRights()
        {
            return new AccessRightsRepository().GetAllAccessRights();
        }

        public List<RemovedAccessRight> GetAllRemovedAccessRights()
        {
            return new AccessRightsRepository().GetAllRemovedAccessRights();
        }

        public List<AccessRight> GetAllActiveAccessRights()
        {
            return new AccessRightsRepository().GetAllActiveAccessRights();
        }


        public AccessRight GetAccessRight(Guid accessRightId)
        {
            return new AccessRightsRepository().GetAccessRight(accessRightId);
        }


        public AccessRight AddAccessRight(AccessRight accessRight)
        {
            return new AccessRightsRepository().AddAccessRight(accessRight);
        }

        public AccessRight UpdateAccessRight(AccessRight accessRight)
        {
            return new AccessRightsRepository().UpdateAccessRight(accessRight);
        }

        public RemovedAccessRight RemoveAccessRight(RemovedAccessRight removedAccessRight)
        {
            return new AccessRightsRepository().RemoveAccessRight(removedAccessRight);
        }





        // User Access Rights
        public List<UserAccessRight> GetAllUserAccessRights()
        {
            return new UserAccessRightsRepository().GetAllUserAccessRights();
        }

        public List<RemovedUserAccessRight> GetAllRemovedUserAccessRights()
        {
            return new UserAccessRightsRepository().GetAllRemovedUserAccessRights();
        }

        public List<UserAccessRight> GetAllActiveUserAccessRights()
        {
            return new UserAccessRightsRepository().GetAllActiveUserAccessRights();
        }

        public List<UserAccessRight> GetUserAccessRights(Guid userId)
        {
            return new UserAccessRightsRepository().GetUserAccessRights(userId);
        }

        public List<RemovedUserAccessRight> GetRemovedUserAccessRights(Guid userId)
        {
            return new UserAccessRightsRepository().GetRemovedUserAccessRights(userId);
        }

        public List<UserAccessRight> GetActiveUserAccessRights(Guid userId)
        {
            return new UserAccessRightsRepository().GetActiveUserAccessRights(userId);
        }


        public UserAccessRight AddUserAccessRight(UserAccessRight userAccessRight)
        {
            return new UserAccessRightsRepository().AddUserAccessRight(userAccessRight);
        }

        public UserAccessRight UpdateUserAccessRight(UserAccessRight userAccessRight)
        {
            return new UserAccessRightsRepository().UpdateUserAccessRight(userAccessRight);
        }

        public RemovedUserAccessRight RemoveUserAccessRight(RemovedUserAccessRight removedUserAccessRight)
        {
            return new UserAccessRightsRepository().RemoveUserAccessRight(removedUserAccessRight);
        }







        // Customers
        public Customer AddCustomer(Customer customer)
        {
            var result = new CustomersRepository().AddCustomer(customer);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.CustomerWasAddedNotification(result.CustomerId));

            return result;
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var result = new CustomersRepository().UpdateCustomer(customer);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.CustomerWasUpdatedNotification(result.CustomerId));

            return result;
        }

        public RemovedCustomer RemoveCustomer(RemovedCustomer removedCustomer)
        {
            var result = new CustomersRepository().RemoveCustomer(removedCustomer);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.CustomerWasRemovedNotification(result.RemovedCustomerId));

            return result;
        }

        public List<Customer> GetAllCustomers()
        {
            return new CustomersRepository().GetAllCustomers();
        }

        public List<Customer> GetAllActiveCustomers()
        {
            return new CustomersRepository().GetAllActiveCustomers();
        }

        public List<RemovedCustomer> GetAllRemovedCustomers()
        {
            return new CustomersRepository().GetAllRemovedCustomers();
        }

        public Customer GetCustomer(Guid customerId)
        {
            return new CustomersRepository().GetCustomer(customerId);
        }

        public RemovedCustomer GetRemovedCustomer(Guid removedCustomerId)
        {
            return new CustomersRepository().GetRemovedCustomer(removedCustomerId);
        }



        // Work Type
        public List<WorkType> GetAllWorkTypes()
        {
            return new WorkTypesRepository().GetAllWorkTypes();
        }

        public WorkType FindWorkType(string name)
        {
            return new WorkTypesRepository().FindWorkType(name);
        }






        // Work Status
        public List<WorkStatus> GetAllWorkStatuses()
        {
            return new WorkStatusRepository().GetAllWorkStatuses();
        }

        public WorkStatus FindWorkStatus(string name)
        {
            return new WorkStatusRepository().FindWorkStatus(name);
        }





        // Software Type
        public List<SoftwareType> GetAllSoftwareTypes()
        {
            return new SoftwareTypesRepository().GetAllSoftwareTypes();
        }

        public SoftwareType FindSoftwareType(string name)
        {
            return new SoftwareTypesRepository().FindSoftwareType(name);
        }


        // Work Requests

        public List<WorkRequest> GetAllWorkRequests()
        {
            return new WorkRequestsRepository().GetAllWorkRequests();
        }

        public List<RemovedWorkRequest> GetAllRemovedWorkRequests()
        {
            return new WorkRequestsRepository().GetAllRemovedWorkRequests();
        }

        public List<WorkRequest> GetAllActiveWorkRequests()
        {
            return new WorkRequestsRepository().GetAllActiveWorkRequests();
        }

        public WorkRequest AddWorkRequest(WorkRequest workRequest)
        {
            var result = new WorkRequestsRepository().AddWorkRequest(workRequest);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.WorkRequestWasAddedNotification(result.WorkRequestId));

            return result;
        }

        public WorkRequest UpdateWorkRequest(WorkRequest workRequest)
        {
            var result = new WorkRequestsRepository().UpdateWorkRequest(workRequest);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.WorkRequestWasUpdatedNotification(result.WorkRequestId));

            return result;
        }

        public RemovedWorkRequest RemoveWorkRequest(RemovedWorkRequest removedWorkRequest)
        {
            var result = new WorkRequestsRepository().RemoveWorkRequest(removedWorkRequest);

            // Push notification
            pushNotificationsRepository.NotifyAsync(OperationContext.Current, subscriber => subscriber.WorkRequestWasRemovedNotification(result.RemovedWorkRequestId));

            return result;
        }

        public RemovedWorkRequest GetRemovedWorkRequest(Guid removedWorkRequestId)
        {
            return new WorkRequestsRepository().GetRemovedWorkRequest(removedWorkRequestId);
        }

        public WorkRequest GetWorkRequest(Guid workRequestId)
        {
            return new WorkRequestsRepository().GetWorkRequest(workRequestId);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfApi.AccessRights;
using WcfApi.Customers;
using WcfApi.DataAccessLayer;
using WcfApi.Genders;
using WcfApi.SoftwareTypes;
using WcfApi.UserLogins;
using WcfApi.Users;
using WcfApi.WorkRequests;
using WcfApi.WorkStatuses;
using WcfApi.WorkTypes;

namespace WcfApi
{
    [ServiceContract (CallbackContract = typeof(IBacklogApiPushNotifications))]
    public interface IBackLogAPI
    {


        // Push Notifications
        [OperationContract(IsOneWay = true)]
        void SubscribeToNotifications();

        [OperationContract(IsOneWay = true)]
        void UnSubscribeFromNotificaitons();





        // Genders
        [OperationContract]
        List<Gender> GetAllGenders();

        [OperationContract]
        Gender FindGender(string name);





        // Users
        [OperationContract]
        User AddUser(User user);
        [OperationContract]
        User UpdateUser(User user);

        [OperationContract]
        RemovedUser RemoveUser(RemovedUser removedUser);

        [OperationContract]
        List<User> GetAllUsers();

        [OperationContract]
        List<User> GetAllActiveUsers();

        [OperationContract]
        List<RemovedUser> GetAllRemovedUsers();

        [OperationContract]
        User GetUser(Guid userId);

        [OperationContract]
        RemovedUser GetRemovedUser(Guid removedUserId);




        // User Logins


        [OperationContract]
        UserLogin FindUserLogin(string userId, string password);

        [OperationContract]
        List<UserLogin> GetAllUserLogins();

        [OperationContract]
        List<RemovedUserLogin> GetAllRemovedUserLogins();

        [OperationContract]
        List<UserLogin> GetAllActiveUserLogins();

        [OperationContract]
        List<UserLogin> GetUserLogins(Guid userId);

        [OperationContract]
        List<RemovedUserLogin> GetRemovedUserLogins(Guid userId);

        [OperationContract]
        List<UserLogin> GetActiveUserLogins(Guid userId);

        [OperationContract]
        UserLogin GetUserLogin(Guid userLoginId);

        [OperationContract]
        UserLogin AddUserLogin(UserLogin userLogin);

        [OperationContract]
        UserLogin UpdateUserLogin(UserLogin userLogin);

        [OperationContract]
        RemovedUserLogin RemoveUserLogin(RemovedUserLogin removedUserLogin);





        // Access Rights
        [OperationContract]
        List<AccessRight> GetAllAccessRights();

        [OperationContract]
        List<RemovedAccessRight> GetAllRemovedAccessRights();

        [OperationContract]
        List<AccessRight> GetAllActiveAccessRights();

        [OperationContract]
        AccessRight GetAccessRight(Guid accessRightId);

        [OperationContract]
        AccessRight AddAccessRight(AccessRight accessRight);

        [OperationContract]
        AccessRight UpdateAccessRight(AccessRight accessRight);

        [OperationContract]
        RemovedAccessRight RemoveAccessRight(RemovedAccessRight removedAccessRight);





        // User Access Rights
        [OperationContract]
        List<UserAccessRight> GetAllUserAccessRights();

        [OperationContract]
        List<RemovedUserAccessRight> GetAllRemovedUserAccessRights();

        [OperationContract]
        List<UserAccessRight> GetAllActiveUserAccessRights();

        [OperationContract]
        List<UserAccessRight> GetUserAccessRights(Guid userId);

        [OperationContract]
        List<RemovedUserAccessRight> GetRemovedUserAccessRights(Guid userId);

        [OperationContract]
        List<UserAccessRight> GetActiveUserAccessRights(Guid userId);

        [OperationContract]
        UserAccessRight AddUserAccessRight(UserAccessRight userAccessRight);

        [OperationContract]
        UserAccessRight UpdateUserAccessRight(UserAccessRight userAccessRight);

        [OperationContract]
        RemovedUserAccessRight RemoveUserAccessRight(RemovedUserAccessRight removedUserAccessRight);






        // Customers
        [OperationContract]
        Customer AddCustomer(Customer customer);
        [OperationContract]
        Customer UpdateCustomer(Customer customer);

        [OperationContract]
        RemovedCustomer RemoveCustomer(RemovedCustomer removedCustomer);

        [OperationContract]
        List<Customer> GetAllCustomers();

        [OperationContract]
        List<Customer> GetAllActiveCustomers();

        [OperationContract]
        List<RemovedCustomer> GetAllRemovedCustomers();

        [OperationContract]
        Customer GetCustomer(Guid customerId);

        [OperationContract]
        RemovedCustomer GetRemovedCustomer(Guid removedCustomerId);



        // Work Type
        [OperationContract]
        List<WorkType> GetAllWorkTypes();

        [OperationContract]
        WorkType FindWorkType(string name);




        // Work Status
        [OperationContract]
        List<WorkStatus> GetAllWorkStatuses();

        [OperationContract]
        WorkStatus FindWorkStatus(string name);





        // Software Type
        [OperationContract]
        List<SoftwareType> GetAllSoftwareTypes();

        [OperationContract]
        SoftwareType FindSoftwareType(string name);





        // Work Requests
        [OperationContract]
        List<WorkRequest> GetAllWorkRequests();

        [OperationContract]
        List<RemovedWorkRequest> GetAllRemovedWorkRequests();

        [OperationContract]
        List<WorkRequest> GetAllActiveWorkRequests();

        [OperationContract]
        WorkRequest AddWorkRequest(WorkRequest workRequest);

        [OperationContract]
        WorkRequest UpdateWorkRequest(WorkRequest workRequest);

        [OperationContract]
        RemovedWorkRequest RemoveWorkRequest(RemovedWorkRequest removedWorkRequest);

        [OperationContract]
        WorkRequest GetWorkRequest(Guid workRequestId);

        [OperationContract]
        RemovedWorkRequest GetRemovedWorkRequest(Guid removedWorkRequestId);
    }


}

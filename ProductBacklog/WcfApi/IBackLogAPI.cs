using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfApi.AccessRights;
using WcfApi.Customers;
using WcfApi.DataAccessLayer;
using WcfApi.UserLogins;
using WcfApi.Users;

namespace WcfApi
{
    [ServiceContract]
    public interface IBackLogAPI
    {
        // Genders
        [OperationContract]
        List<Gender> GetAllGenders();

        [OperationContract]
        Gender GetGender(string name);





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

    }


}

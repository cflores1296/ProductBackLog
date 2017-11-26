using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using WcfApi.AccessRights;
using WcfApi.Customers;
using WcfApi.DataAccessLayer;
using WcfApi.UserAccessRights;
using WcfApi.UserLogins;
using WcfApi.Users;

namespace WcfApi
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class BacklogAPI : IBackLogAPI
    {
        // Genders
        public List<Gender> GetAllGenders()
        {
            return new Genders.Genders().GetAllGenders();
        }

        public Gender GetGender(string name)
        {
            return new Genders.Genders().GetGender(name);
        }








        // Users
        public User AddUser(User user)
        {
            return new UsersRepository().AddUser(user);
        }

        public User UpdateUser(User user)
        {
            return new UsersRepository().UpdateUser(user);
        }

        public RemovedUser RemoveUser(RemovedUser removedUser)
        {
            return new UsersRepository().RemoveUser(removedUser);
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
            return new CustomersRepository().AddCustomer(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return new CustomersRepository().UpdateCustomer(customer);
        }

        public RemovedCustomer RemoveCustomer(RemovedCustomer removedCustomer)
        {
            return new CustomersRepository().RemoveCustomer(removedCustomer);
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

    }
}

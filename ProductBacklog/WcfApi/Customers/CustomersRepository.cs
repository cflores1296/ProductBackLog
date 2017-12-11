using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;
using WcfApi.Users;

namespace WcfApi.Customers
{
    public class CustomersRepository
    {
        public List<Customer> GetAllCustomers()
        {
            return new DataContext().DbCustomers.ToList().Select(customer => new Customer(customer)).ToList();
        }

        public List<RemovedCustomer> GetAllRemovedCustomers()
        {
            return new DataContext().DbRemovedCustomers.ToList().Select(removedCustomer => new RemovedCustomer(removedCustomer)).ToList();
        }

        public List<Customer> GetAllActiveCustomers()
        {
            return new DataContext().DbCustomers.Where(customer => customer.DbRemovedCustomer == null).ToList().Select(customer => new Customer(customer)).ToList();
        }


        public Customer GetCustomer(Guid customerId)
        {
            Customer customer = null;

            var dbCustomer = GetDbCustomer(new DataContext(), customerId);

            if (dbCustomer != null)
            {
                customer = new Customer(dbCustomer);
            }

            return customer;
        }

        public Customer AddCustomer(Customer customer)
        {
            var dbContext = new DataContext();
            var dbCustomer = new DbCustomer();
            dbCustomer.DbCustomerId = customer.CustomerId;
            dbCustomer.Name = customer.Name;

            dbCustomer = dbContext.DbCustomers.Add(dbCustomer);
            dbContext.SaveChanges();

            return new Customer(dbCustomer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            var dbContext = new DataContext();
            var dbCustomer = GetDbCustomer(dbContext, customer.CustomerId);

            if (dbCustomer != null)
            {
                dbCustomer.Name = customer.Name;
                dbContext.SaveChanges();
            }

            return new Customer(dbCustomer);
        }

        public RemovedCustomer RemoveCustomer(RemovedCustomer removedCustomer)
        {
            var dbContext = new DataContext();

            var dbRemovedCustomerFound = dbContext.DbRemovedCustomers.FirstOrDefault(dbRemovedCustomer => dbRemovedCustomer.DbCustomer.DbCustomerId == removedCustomer.Customer.CustomerId);

            if (dbRemovedCustomerFound == null)
            {
                dbRemovedCustomerFound = new DbRemovedCustomer();
                dbRemovedCustomerFound.DateRemoved = removedCustomer.DateRemoved;
                dbRemovedCustomerFound.DbRemovedCustomerId = removedCustomer.RemovedCustomerId;
                dbRemovedCustomerFound.DbCustomer = GetDbCustomer(dbContext, removedCustomer.Customer.CustomerId);
                dbRemovedCustomerFound.DbRemovedByUser = new UsersRepository().GetDbUser(dbContext, removedCustomer.RemovedByUser.UserId);

                dbRemovedCustomerFound = dbContext.DbRemovedCustomers.Add(dbRemovedCustomerFound);
                dbContext.SaveChanges();
            }


            return new RemovedCustomer(dbRemovedCustomerFound);
        }

        public RemovedCustomer GetRemovedCustomer(Guid removedCustomerId)
        {
            RemovedCustomer removedCustomer = null;
            var dbRemovedCustomerFound = new DataContext().DbRemovedCustomers.FirstOrDefault(dbRemovedCustomer => dbRemovedCustomer.DbRemovedCustomerId == removedCustomerId);

            if (dbRemovedCustomerFound != null)
            {
                removedCustomer = new RemovedCustomer(dbRemovedCustomerFound);
            }

            return removedCustomer;
        }


        public DbCustomer GetDbCustomer(DataContext dbContext, Guid dbCustomerId)
        {
            return dbContext.DbCustomers.FirstOrDefault(dbCustomer => dbCustomer.DbCustomerId == dbCustomerId);
        }
    }
}


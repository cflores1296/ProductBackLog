using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using WcfApi.Users;

namespace WcfApi
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class BacklogAPI : IBackLogAPI
    {
        public List<User> GetAllUsers()
        {
            return new Users.Users().GetAllUsers();
        }

        public string GetData(int value)
        {
            Thread.Sleep(2000);
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}

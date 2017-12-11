using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using WcfApi.DataAccessLayer;

namespace WcfApi.Genders
{
    [DataContract]
    public class Gender
    {
        public Gender() { }

        public Gender(DbGender dbGender)
        {
            GenderId = dbGender.DbGenderId;
            Name = dbGender.Name;
        }


        [DataMember]
        public Guid GenderId { set; get; }

        [DataMember]
        public string Name { set; get; }
    }
}

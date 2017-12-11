using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfApi.Genders;
using WpfDesktopClient.BacklogApi;

namespace WpfDesktopClient.Users
{
    public class GenderView
    {
        public Gender gender;
        public GenderView(Gender gender)
        {
            this.gender = gender;
        }

        public string Gender { get { return gender.Name; } }

        public override string ToString()
        {
            return Gender;
        }
    }
}

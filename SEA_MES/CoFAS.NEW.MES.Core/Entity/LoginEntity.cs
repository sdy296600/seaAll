using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CoFAS.NEW.MES.Core.Entity
{
    public class LoginEntity
    {
        public string user_account { get; set; }
        public string user_name { get; set; }
        public string user_password { get; set; }

        public string user_newpassword { get; set; }
    }

}

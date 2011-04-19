using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJ.Web
{
    public class Membership
    {
        public static bool CanLogin(string password)
        {
            return password == "sj90job";
        }
    }
}
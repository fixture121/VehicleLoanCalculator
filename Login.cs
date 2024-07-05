using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.Interop;
using System.Text;
using System.Threading.Tasks;

namespace A2ToufiqCharania
{
    // Created separate Login class to store login information and ValidateUser function
    class Login
    {
        string user1 = "toufiq";
        string password1 = "1234";

        string user2 = "prof";
        string password2 = "password";

        public bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || (string.IsNullOrEmpty(password)))
            {
                return false;
            }

            if ((string.Equals(username, user1) && string.Equals(password, password1)) || string.Equals(username, user2) && string.Equals(password, password2))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}

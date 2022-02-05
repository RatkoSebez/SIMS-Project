using System;

namespace Model
{
   public class User
    {
        private string username;
        private string password;

        public User() { }
        public User(string usernamee, string passwordd)
        {
            username = usernamee;
            password = passwordd;
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
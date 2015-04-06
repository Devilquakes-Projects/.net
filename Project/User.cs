// Auther: Joren Martens
// Date: 31/03/2015 18:37

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public static class User
    {
        private static bool _loggedIn;

        public static bool LoggedIn
        {
            get { return _loggedIn; }
        }

        private static int _id;

        public static int Id
        {
            get { return _id; }
        }

        private static string _username;

        public static string UserName
        {
            get { return _username; }
        }

        private static string _name;

        public static string Name
        {
            get { return _name; }
        }

        private static string _lastName;

        public static string LastName
        {
            get { return _lastName; }
        }

        private static int _permission;

        public static int Permission
        {
            get { return _permission; }
        }

        public static void Logout()
        {
            _id = 0;
            _username = null;
            _name = null;
            _lastName = null;
            _permission = 0;
            _loggedIn = false;
        }

        public static bool Login(string userName, string pass)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("username");
            }
            if (pass == null)
            {
                throw new ArgumentNullException("password");
            }

            User.Logout();

            string[] dbUser = DBController.FindFirst("users", "username", userName);
            if (dbUser != null)
            {
                // 3 is het veld van het wachtwoord.
                if (BCrypt.CheckPassword(pass, dbUser[3]))
                {
                    // Do login
                    _id = Convert.ToInt32(dbUser[0]);
                    _username = dbUser[2];
                    _name = dbUser[4];
                    _lastName = dbUser[5];
                    _permission = Convert.ToInt32(dbUser[6]);
                    _loggedIn = true;
                    return true;
                }
            }
            return false;
        }

        public static bool Register(string userName, string pass, string name, string lastName, bool teacher = false)
        {
            if (userName == null)
            {
                throw new ArgumentNullException("username");
            }
            if (pass == null)
            {
                throw new ArgumentNullException("password");
            }
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (lastName == null)
            {
                throw new ArgumentNullException("last name");
            }

            User.Logout();

            // check als user bestaat
            string[] userExists = DBController.FindFirst("users", "username", userName);
            if (userExists == null)
            {
                string salt = BCrypt.GenerateSalt();
                pass = BCrypt.HashPassword(pass, salt);

                string[] records = { userName, pass, name, lastName, "0" };

                if (teacher)
                {
                    records[4] = "1";
                }

                DBController.AddRecord("users", records);
                string[] dbUser = DBController.FindFirst("users", "username", userName);
                _id = Convert.ToInt32(dbUser[0]);
                _username = dbUser[2];
                _name = dbUser[4];
                _lastName = dbUser[5];
                _permission = Convert.ToInt32(dbUser[6]);
                _loggedIn = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Promote(int id)
        {
            try
            {
                string[] records = DBController.FindFirst("users", "id", Convert.ToString(id));
                records[6] = "1";
                DBController.ChangeRecord("users", id, records);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Demote(int id)
        {
            try
            {
                string[] records = DBController.FindFirst("users", "id", Convert.ToString(id));
                records[6] = "0";
                DBController.ChangeRecord("users", id, records);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

// Auther: Joren Martens
// Date: 31/03/2015 18:37

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public static class User
    {
        //private static bool _loggedIn;

        public static bool LoggedIn { get; set; }
        public static int Id { get; set; }
        public static string Username { get; set; }
        public static string Name { get; set; }
        public static string LastName { get; set; }
        public static int Permission { get; set; }

        public static void Logout()
        {
            Id = 0;
            Username = null;
            Name = null;
            LastName = null;
            Permission = 0;
            LoggedIn = false;
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

            string[] dbUser = DB.FindFirst("users", "username", userName);
            if (dbUser != null)
            {
                // 3 is het veld van het wachtwoord.
                if (BCrypt.CheckPassword(pass, dbUser[3]))
                {
                    // Do login
                    Id = Convert.ToInt32(dbUser[0]);
                    Username = dbUser[2];
                    Name = dbUser[4];
                    LastName = dbUser[5];
                    Permission = Convert.ToInt32(dbUser[6]);
                    LoggedIn = true;
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
            string[] userExists = DB.FindFirst("users", "username", userName);
            if (userExists == null)
            {
                string salt = BCrypt.GenerateSalt();
                pass = BCrypt.HashPassword(pass, salt);

                string[] records = { userName, pass, name, lastName, "0" };

                if (teacher)
                {
                    records[4] = "1";
                }

                DB.AddRecord("users", records);
                string[] dbUser = DB.FindFirst("users", "username", userName);
                Id = Convert.ToInt32(dbUser[0]);
                Username = dbUser[2];
                Name = dbUser[4];
                LastName = dbUser[5];
                Permission = Convert.ToInt32(dbUser[6]);
                LoggedIn = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Promote(int id)
        {
            string[] records = DB.FindFirst("users", "id", Convert.ToString(id));
            if (records != null)
            {
                records[6] = "1";
                DB.ChangeRecord("users", id, records);
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool Demote(int id)
        {
            string[] records = DB.FindFirst("users", "id", Convert.ToString(id));
            if (records != null)
            {
                records[6] = "0";
                DB.ChangeRecord("users", id, records);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

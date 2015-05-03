﻿// Auther: Joren Martens
// Date: 31/03/2015 18:37

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Exceptions;

namespace Project.Controllers
{
    public static class User
    {
        public static bool LoggedIn { get; private set; }
        public static int Id { get; private set; }
        public static string Username { get; private set; }
        public static string Name { get; private set; }
        public static string LastName { get; private set; }
        public static string Class { get; private set; }
        public static int Permission { get; private set; }

        private static string userDB = ProjectConfig.UserFile;

        public static void Logout()
        {
            Id = 0;
            Username = null;
            Name = null;
            LastName = null;
            Class = null;
            Permission = 0;
            LoggedIn = false;
        }

        public static void Login(string userName, string pass)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (String.IsNullOrEmpty(pass))
            {
                throw new ArgumentNullException("pass");
            }

            User.Logout();

            string[] dbUser = DB.FindFirst(userDB, "username", userName);
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
                    Class = dbUser[6];
                    Permission = Convert.ToInt32(dbUser[7]);
                    LoggedIn = true;
                }
                else
                {
                    throw new InvalidPasswordException();
                }
            }
            else
            {
                throw new UserNotFoundException();
            }
        }

        public static void Register(string userName, string pass, string name, string lastName, string teacher, string classText)
        {
            bool isTeacher = false;

            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (String.IsNullOrEmpty(pass))
            {
                throw new ArgumentNullException("pass");
            }
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (String.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException("lastName");
            }
            if (String.IsNullOrEmpty(classText))
            {
                throw new ArgumentNullException("classText");
            }
            if (!String.IsNullOrEmpty(teacher))
            {
                if (teacher.Equals(ProjectConfig.TeacherCode))
                {
                    isTeacher = true;
                }
                else
                {
                    throw new InvalidTeacherCodeException();
                }
            }

            User.Logout();

            // check als user bestaat
            string[] userExists = DB.FindFirst(userDB, "username", userName);
            if (userExists == null)
            {
                string salt = BCrypt.GenerateSalt();
                pass = BCrypt.HashPassword(pass, salt);

                string[] records = { userName, pass, name, lastName, classText, "0" };

                if (isTeacher)
                {
                    records[5] = "1";
                }

                DB.AddRecord("users", records);
                string[] dbUser = DB.FindFirst(userDB, "username", userName);
                Id = Convert.ToInt32(dbUser[0]);
                Username = dbUser[2];
                Name = dbUser[4];
                LastName = dbUser[5];
                Class = dbUser[6];
                Permission = Convert.ToInt32(dbUser[7]);
                LoggedIn = true;
            }
            else
            {
                throw new UserAlreadyExistsException();
            }
        }

        public static void Recover(string userName, string pass, string teacher)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (String.IsNullOrEmpty(pass))
            {
                throw new ArgumentNullException("userName");
            }
            if (String.IsNullOrEmpty(teacher))
            {
                throw new ArgumentNullException("userName");
            }

            if (!teacher.Equals(ProjectConfig.TeacherCode))
            {
                throw new InvalidTeacherCodeException();
            }
            else
            {
                string[] dbUser = DB.FindFirst(userDB, "username", userName);
                if (dbUser != null)
                {
                    string salt = BCrypt.GenerateSalt();
                    pass = BCrypt.HashPassword(pass, salt);

                    dbUser[3] = Convert.ToString(pass);
                    DB.ChangeFromRead(userDB, Convert.ToInt32(dbUser[0]), dbUser);
                }
                else
                {
                    throw new UserNotFoundException();
                }
            }
        }

        public static void Promote(int id)
        {
            string[] records = DB.FindFirst(userDB, "id", Convert.ToString(id));
            if (records != null)
            {
                records[7] = "1";
                DB.ChangeRecord("users", id, records);
            }
            else
            {
                throw new UserNotFoundException();
            }
        }

        public static void Demote(int id)
        {
            string[] records = DB.FindFirst(userDB, "id", Convert.ToString(id));
            if (records != null)
            {
                records[7] = "0";
                DB.ChangeRecord("users", id, records);
            }
            else
            {
                throw new UserNotFoundException();
            }
        }
    }
}

// Auther: Joren Martens
// Date: 03/05/2015

using Project.Controllers;
using Project.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Project
{
    static class ProjectConfig
    {
        public static string DBDestinationPath { get; private set; }
        public static char DBSeparator { get; private set; }
        public static string TeacherCode { get; private set; }
        public static int PlayTime { get; private set; }
        public static string UserFile { get; private set; }
        public static string StudentsFile { get; private set; }
        public static string StudentPointsFile { get; private set; }
        public static string QuestionsFileGeo { get; private set; }
        public static string QuestionsFileLang { get; private set; }
        public static string QuestionsFileMath { get; private set; }
        public static string SnakeFile { get; private set; }
        public static string BallFile { get; private set; }

        private static List<string[]> DBs;

        public static void StartUp()
        {
            DBDestinationPath = Path.Combine("01DBProject");
            DBSeparator = '|';

            // Check for write permissions
            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(DBDestinationPath);
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("UnauthorizedAccessException, no write access in folder path.");
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(DBDestinationPath);
            }

            // Teacher code
            TeacherCode = "123";

            // dbName + fields
            string[] userDB = { "users", "username", "password", "name", "lastname", "class", "permissions" };

            string[] coursesInProgressDB = { "courses_in_progress", "userID", "math_scores", "language_scores", "geography_scores" };
            string[] coursesPointsDB = { "courses_points", "userID", "date", "math_scores", "language_scores", "geography_scores" };

            string[] coursesGeographyDB = { "courses_geography", "question", "correct_solution", "wrong_solution" };
            string[] coursesLangDB = { "courses_lang", "question", "solution1", "solution2", "solution3" };
            string[] coursesMathDB = { "courses_math", "question", "solution" };
            
            string[] pointsSnakeDB = { "points_snake", "userID", "date", "time_played", "points" };
            string[] pointsBallDB = { "points_ball", "userID", "date", "time_played", "points" };

            DBs = new List<string[]>();
            DBs.Add(userDB);

            DBs.Add(coursesInProgressDB);
            DBs.Add(coursesPointsDB);

            DBs.Add(coursesMathDB);
            DBs.Add(coursesLangDB);
            DBs.Add(coursesGeographyDB);

            DBs.Add(pointsSnakeDB);
            DBs.Add(pointsBallDB);

            // check saved DB
            ProjectConfig.CheckDB();

            // Set userDB
            UserFile = userDB[0];

            // Set Curriculum StudentsFile
            StudentsFile = coursesInProgressDB[0];

            // Set Curriculum StudentsPointsFile
            StudentPointsFile = coursesPointsDB[0];

            // Set Curriculum QuestionsFile
            QuestionsFileGeo = coursesGeographyDB[0];
            QuestionsFileLang = coursesLangDB[0];
            QuestionsFileMath = coursesMathDB[0];

            // Set snakeDB
            SnakeFile = pointsSnakeDB[0];

            // Set snakeDB
            BallFile = pointsBallDB[0];
        }

        public static void CheckPlayTime()
        {
            if (PlayTime == 0)
            {
                try
                {
                    string[] userString = DB.FindFirst(ProjectConfig.StudentsFile, "userID", Convert.ToString(User.Id));
                    bool completedAllCourses = true;
                    for (int i = 3; i <= 5; i++)
                    {
                        if (userString[i] != "false")
                        {
                            PlayTime += Convert.ToInt32(userString[i]);
                        }
                        else
                        {
                            completedAllCourses = false;
                            MessageBox.Show("Voor te spelen moet je eerst alle oefeningen maken.");
                        }
                    }
                    if (completedAllCourses)
                        PlayTime *= 10;
                    else
                        PlayTime = 0;
                }
                catch (NoRecordFoundException)
                {
                    PlayTime = 0;
                    MessageBox.Show("Voor te spelen moet je eerst alle oefeningen maken.");
                }
            }
        }

        private static void CheckDB()
        {
            foreach (string[] db in DBs)
            {
                string[] fields = new string[db.Length - 1];
                for (int i = 1; i < db.Length; i++)
                {
                    fields[i - 1] = db[i];
                }


                if (DB.Exists(db[0]))
                {
                    string[] structure = DB.GetStructure(db[0]);
                    bool structureOk = true;

                    if (fields.Length == structure.Length - 2)
                    {
                        for (int i = 0; i < fields.Length; i++)
                        {
                            if (!fields[i].ToUpper().Equals(structure[i+2]))
                            {
                                structureOk = false;
                            }
                        }
                    } else {
                        structureOk = false;
                    }

                    if (!structureOk)
                    {
                        DB.Delete(db[0]);
                        DB.Make(db[0], fields);
                    }
                }
                else
                {
                    DB.Make(db[0], fields);
                }
            }
        }
    }
}

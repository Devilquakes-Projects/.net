// Auther: Joren Martens
// Date: 03/05/2015

using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    static class ProjectConfig
    {
        public static string TeacherCode { get; private set; }
        public static string UserFile { get; private set; }
        public static string StudentsFile { get; private set; }
        public static string QuestionsFileGeo { get; private set; }
        public static string QuestionsFileLang { get; private set; }
        public static string QuestionsFileMath { get; private set; }
        private static List<string[]> DBs { get; set; }

        public static void StartUp()
        {
            // Teacher code
            TeacherCode = "123";

            // dbName + fields
            string[] userDB = { "users", "username", "password", "name", "lastname", "class", "permissions" };

            string[] coursesDB = { "courses", "userID", "math_scores", "language_scores", "geography_scores" };

            string[] coursesGeographyDB = { "courses_geography", "question", "correct_solution", "wrong_solution" };
            string[] coursesLangDB = { "courses_lang", "question", "solution1", "solution2", "solution3" };
            string[] coursesMathDB = { "courses_math", "question", "solution" };
            
            string[] pointsSnakeDB = { "points_snake", "userID", "date", "time_played", "points" };
            string[] pointsBallDB = { "points_ball", "userID", "date", "time_played", "points" };

            DBs = new List<string[]>();
            DBs.Add(userDB);

            DBs.Add(coursesDB);
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
            StudentsFile = coursesDB[0];

            // Set Curriculum QuestionsFile
            QuestionsFileGeo = coursesGeographyDB[0];
            QuestionsFileLang = coursesLangDB[0];
            QuestionsFileMath = coursesMathDB[0];


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

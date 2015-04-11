// Auther: Joren Martens
// Date: 07/04/2015 15:22

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public static class Course
    {
        private static string[,] GetAll()
        {
            // add cources to this array
            string[,] cources = { 
                                    { "Mathmatics", "Courses_Math_Questions", "Courses_Math" }
                                };
            return cources;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cource">Name of the cource.</param>
        /// <returns>String with the db path.</returns>
        public static string DBQuestionsPath(string cource)
        {
            string[,] courcesAll = Course.GetAll();

            for (int i = 0; i < courcesAll.Length / 3; i++)
            {
                if (courcesAll[i, 0].Equals(cource))
                {
                    return courcesAll[i, 1];
                }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Array of all the cource names.</returns>
        public static string[] AllCourses()
        {
            string[,] courcesAll = Course.GetAll();
            string[] cources = new string[courcesAll.Length / 3];

            for (int i = 0; i < courcesAll.Length / 3; i++)
            {
                cources[i] = courcesAll[i, 0];
            }
            return cources;
        }
    }
}

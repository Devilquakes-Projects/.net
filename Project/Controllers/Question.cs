// Auther: Joren Martens
// Date: 07/04/2015 14:46

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public static class Question
    {
        public static bool Add(string cource, string question, string solution)
        {
            try
            {
                string dbPath = Course.DBQuestionsPath(cource);
                string[] records = { question, solution };
                DB.AddRecord(dbPath, records);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool Change(string cource, int id, string question, string solution)
        {
            try
            {
                string dbPath = Course.DBQuestionsPath(cource);
                string[] records = { question, solution };
                DB.ChangeRecord(dbPath, id, records);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

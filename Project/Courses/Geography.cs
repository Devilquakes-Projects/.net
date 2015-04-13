using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project
{
    public class Geography : Curriculum
    {
        public Geography(int studentId, int difficulty, Button gradebutton, Label time, GroupBox qBoxTitle1, RadioButton rb1, RadioButton rb2, GroupBox qBoxTitle2, RadioButton rb3, RadioButton rb4)
        {
            base.QuestionsFile = "Courses_Geography";
            base.StudentsFile = "Courses";
        }
    }
}

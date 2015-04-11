using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project
{
    class Languages : Curriculum
    {
        public Languages(int studentId, int difficulty, Label time, Button gradeButton, Label l1 = null, TextBox tb1 = null, Label l2 = null, TextBox tb2 = null, Label l3 = null, TextBox tb3 = null, Label l4 = null, TextBox tb4 = null, Label l5 = null, TextBox tb5 = null, Label l6 = null, TextBox tb6 = null)
            : base(studentId, difficulty, time, gradeButton, l1, tb1, l2, tb2, l3, tb3, l4, tb4, l5, tb5, l6, tb6)
        {
            base.QuestionsFile = "Courses_Lang";//COURSES_MATH_QUESTIONS
            base.StudentsFile = "Courses";
            base.SetAmountOfQuestions = 2;
            base.SetAmountOfAnswersPerQuestion = 3;
            base.InitializeArray();
            base.UpdateCurriculum(6);//6: index of Languages
        }

        
    }
}

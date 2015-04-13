using Project.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project
{
    class Languages : Curriculum
    {
        public Languages(int studentId, int difficulty, Button gradeButton, Label time, Label title, Label question1, Label question2, Label header1, Label header2, Label header3, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, TextBox tb6)
            : base(studentId, difficulty, gradeButton, time, title, question1, question2, header1, header2, header3, tb1, tb2, tb3, tb4, tb5, tb6)
        {
            base.QuestionsFile = "Courses_Lang";//COURSES_MATH_QUESTIONS
            base.StudentsFile = "Courses";
            LoadHeaderLabels();

            base.SetTimer += 15;//15 seconds bonus-time for everyone (the defaulted time method is just too short for this course.

            base.SetAmountOfQuestions = 2;
            base.InitializeArray(3);
            base.UpdateCurriculum(6);//6: index of Languages
        }

        private void LoadHeaderLabels()
        {
            string[] questions = DB.FindFirst(base.QuestionsFile, "ID", "1", onlyVisible: false);//onlyVisible: false --> NOTE: line with ID=1 HAS TO BE FALSE!!!, as in: will not be shown to anyone because program loads this line seperatly

            if (questions != null)
            {
                base.Title = questions[2];
                base.Header1 = questions[3];
                base.Header2 = questions[4];
                base.Header3 = questions[5];

                Console.WriteLine("l1: " + l1.Name + " l2: " + l2.Name + " l3: " + l3.Name + " l4: " + l4.Name + " l5: " + l5.Name + " l6: " + l6.Name);
            }
        }

        public override int Grade()//makes sure that answers are stored in uppercase, is this necessary!?
        {
            base.Tb1 = base.Tb1.ToUpper();
            base.Tb2 = base.Tb2.ToUpper();
            base.Tb3 = base.Tb3.ToUpper();
            base.Tb4 = base.Tb4.ToUpper();
            base.Tb5 = base.Tb5.ToUpper();
            base.Tb6 = base.Tb6.ToUpper();

            return base.Grade();
        }
    }
}

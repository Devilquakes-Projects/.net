using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Project.Controllers;

namespace Project
{
    class Mathematics : Curriculum //onderliggend van abstracte klasse Curriculum
    {

        //Constructors:
        public Mathematics()
        {
            base.AmountOfQuestions = 5;
        }

        public Mathematics(int studentId, int difficulty, Label time, Button gradeButton, Label l1 = null, TextBox tb1 = null, Label l2 = null, TextBox tb2 = null, Label l3 = null, TextBox tb3 = null, Label l4 = null, TextBox tb4 = null, Label l5 = null, TextBox tb5 = null, Label l6 = null, TextBox tb6 = null)
            : base(studentId, difficulty, time, gradeButton, l1, tb1, l2, tb2, l3, tb3, l4, tb4, l5, tb5, l6, tb6)
        {
            base.QuestionsFile = "Courses_Math_Questions";//COURSES_MATH_QUESTIONS
            base.StudentsFile = "Courses_Math";
            base.AmountOfQuestions = 5;
            base.UpdateCurriculum();
        }

        //info from: stackexchange.com
        /*  //to be implemented if time arises (with test skills radiobutton!)
        private string RandomMultiplications(int amountOfMultiplications)//test you're math skills xtra
        {
            int minVal = 1; int maxVal = 10;

            int[] uniqueNumbers = RandomNumber(amountOfMultiplications * 2, minVal, maxVal);//4 random numbers, range: 0-10
            int[,] multiplications = new int[amountOfMultiplications, 2];//2-d array: msdn-help

            for (int i = 0; i < uniqueNumbers.Length / 2; i++)//multiplied in uniqueNumbers ^
            {
                for (int j = 0; j < 2; j++)
                {
                    multiplications[i, j] = uniqueNumbers[i * 2 + j];
                }
            }

            //Console.WriteLine("test \n ...");                         //read 2d array: stackoverfloww.com
            //for (int i = 0; i < multiplications.GetLength(0); i++)
            //{
            //    for (int j = 0; j < multiplications.GetLength(1); j++)
            //    {
            //        Console.WriteLine("i: " + i + " j: " + j + " = " + (multiplications[i, j]));
            //    }
            //}

            for (int i = 0; i < multiplications.GetLength(0); i++)
            {
                for (int j = 1; j < multiplications.GetLength(1); j += 2)//per 2
                {
                    Console.WriteLine("i: " + multiplications[i, j - 1] + " j:" + multiplications[i, j]);
                }
            }

            return "tafels terugsturen";
        }
        */
    }
}

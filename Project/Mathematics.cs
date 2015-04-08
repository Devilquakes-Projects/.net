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
        }

        public Mathematics(Label time, Label l1, Label l2, Label l3, Label l4, Label l5, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, int studentId, int difficulty = 1)
            : base(time, l1, l2, l3, l4, l5, tb1, tb2, tb3, tb4, tb5, studentId, difficulty)
        {
            base.QuestionsFile = "Courses_Math_Questions";//COURSES_MATH_QUESTIONS
            base.StudentsFile = "Courses_Math";
            base.PushDbFiles();
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

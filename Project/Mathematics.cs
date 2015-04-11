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
using System.Globalization;

namespace Project
{
    class Mathematics : Curriculum //onderliggend van abstracte klasse Curriculum
    {

        //Constructors:
        public Mathematics()
        {
            base.SetAmountOfQuestions = 5;
        }

        public Mathematics(int studentId, int difficulty, Label time, Button gradeButton, Label l1 = null, TextBox tb1 = null, Label l2 = null, TextBox tb2 = null, Label l3 = null, TextBox tb3 = null, Label l4 = null, TextBox tb4 = null, Label l5 = null, TextBox tb5 = null, Label l6 = null, TextBox tb6 = null)
            : base(studentId, difficulty, time, gradeButton, l1, tb1, l2, tb2, l3, tb3, l4, tb4, l5, tb5, l6, tb6)
        {
            base.QuestionsFile = "Courses_Math";//COURSES_MATH_QUESTIONS
            base.StudentsFile = "Courses";
            base.SetAmountOfQuestions = 5;
            base.SetAmountOfAnswersPerQuestion = 1;
            base.InitializeArray();
            base.UpdateCurriculum(5);//index to check
        }

        private void MsgPopupBox(int questionNumber)
        {
            MessageBox.Show(String.Format("Invalid input in box {0}, you receive 0 points for this question!", questionNumber));
        }

        public override int Grade()//accepts decimal numbers, note: stored with a '.' NOTATION)
        {
            double result;
            NumberStyles style = NumberStyles.AllowDecimalPoint;
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");

            if (base.Tb1 != null)
            {
                if (Double.TryParse(base.Tb1.Replace(',', '.'), style, culture, out result))
                {
                    base.Tb1 = Convert.ToString(result);
                }
                else
                {
                    MsgPopupBox(1);
                }
                if (base.Tb2 != null)
                {
                    if (Double.TryParse(base.Tb2.Replace(',', '.'), style, culture, out result))
                    {
                        base.Tb2 = Convert.ToString(result);
                    }
                    else
                    {
                        MsgPopupBox(2);
                    }

                    if (base.Tb3 != null)
                    {
                        if (Double.TryParse(base.Tb3.Replace(',', '.'), style, culture, out result))
                        {
                            base.Tb3 = Convert.ToString(result);
                        }
                        else
                        {
                            MsgPopupBox(3);
                        }

                        if (base.Tb4 != null)
                        {
                            if (Double.TryParse(base.Tb4.Replace(',', '.'), style, culture, out result))
                            {
                                base.Tb4 = Convert.ToString(result);
                            }
                            else
                            {
                                MsgPopupBox(4);
                            }
                            if (base.Tb5 != null)
                            {
                                if (Double.TryParse(base.Tb5.Replace(',', '.'), style, culture, out result))
                                {
                                    base.Tb5 = Convert.ToString(result);
                                }
                                else
                                {
                                    MsgPopupBox(5);
                                }
                            }
                        }
                    }
                }
            }

            return base.Grade();
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

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
        //instance variables:
        //labels:
        private Label timeLabel;
        private Label l1;
        private Label l2;
        private Label l3;
        private Label l4;
        private Label l5;

        //textboxes:
        private TextBox tb1;
        private TextBox tb2;
        private TextBox tb3;
        private TextBox tb4;
        private TextBox tb5;

        //GradeButton:
        private Button gradeButton;

        //timer:
        private DispatcherTimer timer;

        //Constructors:
        public Mathematics()
        {/*you shouldn't use me!*/}

        public Mathematics(int studentId, int difficulty, Label timeLabel, Button gradeButton, Label l1, Label l2, Label l3, Label l4, Label l5, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5)
            : base(studentId, difficulty)
        {
            base.QuestionsFile = "Courses_Math";//COURSES_MATH_QUESTIONS
            base.StudentsFile = "Courses";
            base.SetAmountOfQuestions = 5;
            base.InitializeArray(1);
            base.IsTestGraded(5);//5: index of Mathematics

            this.timeLabel = timeLabel;
            base.UpdateTimeLabel(timeLabel);

            this.gradeButton = gradeButton;
            gradeButton.Content = "Grade";

            this.l1 = l1;
            this.l2 = l2;
            this.l3 = l3;
            this.l4 = l4;
            this.l5 = l5;

            this.tb1 = tb1;
            this.tb2 = tb2;
            this.tb3 = tb3;
            this.tb4 = tb4;
            this.tb5 = tb5;

            string[] questions = base.LoadQuestions();

            l1.Content = questions[0];
            l2.Content = questions[1];
            l3.Content = questions[2];
            l4.Content = questions[3];
            l5.Content = questions[4];

            timer = base.SetupTimer();
            timer.Tick += timer_Tick;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (base.ShouldTimerStopRunningt(timeLabel))
            {
                int grade = Grade();
                MessageBox.Show(String.Format("You earned {0}/10 points.", grade));
            }
        }

        public override int Grade()//accepts decimal numbers, note: stored with a '.' NOTATION)
        {
            timer.Stop();
            ConvertToDecimal();

            int points = 0;

            string correctAnswer;
            string wrongAnswer;
            string[,] answers = base.Answers;

            base.StartGradeValues(out correctAnswer, out wrongAnswer);

            if (tb1.Text.Equals(answers[0, 0]))
            {
                points++;
                tb1.Text += correctAnswer;
                base.LockTextBlock(tb1, true);
            }
            else
            {
                tb1.Text += wrongAnswer + answers[0, 0];
                base.LockTextBlock(tb1, false);
            }

            if (tb2.Text.Equals(answers[1, 0]))
            {
                points++;
                tb2.Text += correctAnswer;
                base.LockTextBlock(tb2, true);
            }
            else
            {
                tb2.Text += wrongAnswer + answers[1, 0];
                base.LockTextBlock(tb2, false);
            }

            if (tb3.Text.Equals(answers[2, 0]))
            {
                points++;
                tb3.Text += correctAnswer;
                base.LockTextBlock(tb3, true);
            }
            else
            {
                tb3.Text += wrongAnswer + answers[2, 0];
                base.LockTextBlock(tb3, false);
            }

            if (tb4.Text.Equals(answers[3, 0]))
            {
                points++;
                tb4.Text += correctAnswer;
                base.LockTextBlock(tb4, true);
            }
            else
            {
                tb4.Text += wrongAnswer + answers[3, 0];
                base.LockTextBlock(tb4, false);
            }

            if (tb5.Text.Equals(answers[4, 0]))
            {
                points++;
                tb5.Text += correctAnswer;
                base.LockTextBlock(tb5, true);
            }
            else
            {
                tb5.Text += wrongAnswer + answers[4, 0];
                base.LockTextBlock(tb5, false);
            }

            //pts formule math:
            if (Timer > 0)//bonus points if test complete before end of time!
            {
                points = (int)(Math.Round((points * 1.5 + base.GetDifficulty * 0.84), MidpointRounding.AwayFromZero));
            }
            else
            {
                points = (int)(Math.Round((points * 1.5), MidpointRounding.AwayFromZero));
            }

            base.GradeButtonToExit(gradeButton, timeLabel);
            base.WriteRecords(5, points);//index 5 is for column of math points!

            return points;
        }

        private void ConvertToDecimal()
        {
            try
            {
                double result;
                NumberStyles style = NumberStyles.AllowDecimalPoint;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("nl-BE");//edited on: 20/04/15 by Greg (from en-GB to nl-BE)

                if (Double.TryParse(tb1.Text.Replace('.', ','), style, culture, out result))
                {
                    tb1.Text = Convert.ToString(result);//25.0 word 25!
                }
                else
                {
                    MsgPopupBox(1);
                }

                if (Double.TryParse(tb2.Text.Replace('.', ','), style, culture, out result))
                {
                    tb2.Text = Convert.ToString(result);
                }
                else
                {
                    MsgPopupBox(2);
                }

                if (Double.TryParse(tb3.Text.Replace('.', ','), style, culture, out result))
                {
                    tb3.Text = Convert.ToString(result);
                }
                else
                {
                    MsgPopupBox(3);
                }

                if (Double.TryParse(tb4.Text.Replace('.', ','), style, culture, out result))
                {
                    tb4.Text = Convert.ToString(result);
                }
                else
                {
                    MsgPopupBox(4);
                }

                if (Double.TryParse(tb5.Text.Replace('.', ','), style, culture, out result))
                {
                    tb5.Text = Convert.ToString(result);
                }
                else
                {
                    MsgPopupBox(5);
                }
            }
            catch(CultureNotFoundException)
            {
                Console.WriteLine("keyboard culture not found, comma's and dot's will result in wrong answer!");
            }
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

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

// Auther: Gregory Malomgré
// Date: 07/04/2015 16:00
namespace Project
{
    class Mathematics : Courses_TextBoxClass //onderliggend van abstracte klasse Curriculum
    {
        //instance variables:
        //labels:
        private Label timeLabel;

        //textboxes:
        private TextBox[] tb = new TextBox[5];

        //GradeButton:
        private Button gradeButton;

        //timer:
        private DispatcherTimer timer;

        //Constructors:
        public Mathematics(int studentId, int difficulty, Label timeLabel, Button gradeButton, Label l1, Label l2, Label l3, Label l4, Label l5, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5)
            : base(studentId, difficulty)
        {
            base.QuestionsFile = ProjectConfig.QuestionsFileMath;
            base.SetAmountOfQuestions = 5;
            base.InitializeArray(1);
            base.IsTestGraded(3);//3: index of Mathematics

            this.timeLabel = timeLabel;
            base.UpdateTimeLabel(timeLabel);

            this.gradeButton = gradeButton;
            gradeButton.Content = "Grade";

            base.SetTextboxStartSize(185, 1.1, 30, tb1, tb2, tb3, tb4, tb5);//setup textboxes

            this.tb[0] = tb1;
            this.tb[1] = tb2;
            this.tb[2] = tb3;
            this.tb[3] = tb4;
            this.tb[4] = tb5;

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

        private void timer_Tick(object sender, EventArgs e)
        {
            if (base.ShouldTimerStopRunning(timeLabel))
            {
                Grade();
            }
        }

        public override void Grade()//accepts decimal numbers, note: stored with a '.' NOTATION)
        {
            timer.Stop();
            ConvertToDecimal();

            int points = 0;

            string correctAnswer;
            string wrongAnswer;
            string[,] answers = base.Answers;

            base.StartGradeValues(out correctAnswer, out wrongAnswer);

            for (int i = 0; i < tb.Length; i++)
            {
                if (tb[i].Text.Equals(answers[i, 0]))
                {
                    points++;
                    tb[i].Text += correctAnswer;
                    base.LockTextBlock(tb[i], true);
                }
                else
                {
                    tb[i].Text += wrongAnswer + answers[i, 0];
                    base.LockTextBlock(tb[i], false);
                }
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
            base.WriteRecords(3, points);//index 3 is for column of math points!

            base.ShowResults(points);
        }

        private void MsgPopupBox(int questionNumber)
        {
            MessageBox.Show(String.Format("Invalid input in box {0}, you receive 0 points for this question!", questionNumber));
        }

        private void ConvertToDecimal()
        {
            try
            {
                double result;
                NumberStyles style = NumberStyles.AllowDecimalPoint;
                CultureInfo culture = CultureInfo.CreateSpecificCulture("nl-BE");//edited on: 20/04/15 by Greg (from en-GB to nl-BE)

                for (int i = 0; i < tb.Length; i++)
                {
                    if (Double.TryParse(tb[i].Text.Replace('.', ','), style, culture, out result))
                    {
                        tb[i].Text = Convert.ToString(result);//25.0 word 25!
                    }
                    else
                    {
                        MsgPopupBox(i + 1);
                    }
                }
            }
            catch (CultureNotFoundException)
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

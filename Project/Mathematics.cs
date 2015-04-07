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
        //Instance Variables:
        private Random r1 = new Random();
        private DispatcherTimer t1;//TODO for difficulty level.

        private int[] answers = new int[5];
        private bool isCompleted;
        private int studentId;

        //dbfiles to use: placed here for easy editing later on!
        private string MathStudents = "Courses_Math";
        private string questions = "Courses_Math_Questions";
        private int difficulty;

        private Label l1;
        private Label l2;
        private Label l3;
        private Label l4;
        private Label l5;
        private Label time;
        private int timer;

        private TextBox tb1;
        private TextBox tb2;
        private TextBox tb3;
        private TextBox tb4;
        private TextBox tb5;

        //Constructors:
        public Mathematics()
        {
        }

        public Mathematics(Label time, Label l1, Label l2, Label l3, Label l4, Label l5, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, int studentId, int difficulty = 1)
        {
            this.l1 = l1;
            this.l2 = l2;
            this.l3 = l3;
            this.l4 = l4;
            this.l5 = l5;

            this.time = time;
            this.tb1 = tb1;
            this.tb2 = tb2;
            this.tb3 = tb3;
            this.tb4 = tb4;
            this.tb5 = tb5;

            this.difficulty = difficulty;

            this.studentId = studentId;

            string[] studentTestCompleted = DB.FindFirst(MathStudents, "ID", Convert.ToString(studentId));//opvragen db student

            if (studentTestCompleted[5].Equals("false"))
            {
                this.isCompleted = false;
            }
            else
            {
                this.isCompleted = true;//this moet niet maar overzichtelijker
                MessageBox.Show("NOTE: you already completed this test, you won't gain points for doing this test again", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

            timer = 30 / difficulty;
            time.Background = Brushes.Gray;

            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromSeconds(1);
            t1.Tick += t1_Tick;
            t1.Start();
        }

        void t1_Tick(object sender, EventArgs e)
        {
            time.Content = String.Format("Time: {0} seconds", timer);
            if (timer <= 0)
            {
                Grade();//when timer runs out grade this test!
            }
            timer--;
        }

        //Methods:
        public void LoadQuestions()
        {
            int totalLines;
            int visibleLines;
            DB.LineCount(questions, out visibleLines, out totalLines);//THE NEW WAY

            if (visibleLines >= 5)
            {
                int[] randomNumbers = SelectRandomQuestions(5, 1, totalLines);
                string[] isEnabled;

                for (int i = 0; i < randomNumbers.Length; i++)
                {
                    answers[i] = randomNumbers[i];
                    isEnabled = DB.FindFirst(questions, "ID", Convert.ToString(answers[i]));

                    if (isEnabled == null)
                    {
                        i--;
                    }
                }

                string[] labels;
                labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[0]));
                if (labels != null)
                {
                    l1.Content = labels[2];//show question
                    answers[0] = Convert.ToInt32(labels[3]); //save answer index
                }

                labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[1]));
                if (labels != null)
                {
                    l2.Content = labels[2];
                    answers[1] = Convert.ToInt32(labels[3]);
                }

                labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[2]));
                if (labels != null)
                {
                    l3.Content = labels[2];
                    answers[2] = Convert.ToInt32(labels[3]);
                }

                labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[3]));
                if (labels != null)
                {
                    l4.Content = labels[2];
                    answers[3] = Convert.ToInt32(labels[3]);
                }

                labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[4]));
                if (labels != null)
                {
                    l5.Content = labels[2];
                    answers[4] = Convert.ToInt32(labels[3]);
                }
            }
            else
            {
                MessageBox.Show("Not enough questions in the questions-list," + Environment.NewLine + "ask a teacher to fix this", "Error: Not enough Questions.");
            }
        }

        private string ResultOfAnswer(bool isCorrect, int i = 0)
        {
            if (isCorrect)
            {
                return " = Correct answer!";
            }
            else
            {
                return "Wrong should be: " + answers[i];
            }
        }

        public int Grade()
        {
            t1.Stop();

            if (isCompleted == false)
            {
                int points = 0;

                try
                {
                    if (Convert.ToDouble(tb1.Text.Replace('.', ',')) == answers[0]) //replace om . en , toe te staan.
                    {
                        points++;
                        tb1.Text += ResultOfAnswer(true);
                    }
                    else
                    {
                        tb1.Text = ResultOfAnswer(false, 0);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("ongeldige ingave bij vraag 1" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb1.Text = ResultOfAnswer(false, 0);
                }

                try
                {
                    if (Convert.ToDouble(tb2.Text.Replace('.', ',')) == answers[1])
                    {
                        points++;
                        tb2.Text += ResultOfAnswer(true);
                    }
                    else
                    {
                        tb2.Text = ResultOfAnswer(false, 1);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ongeldige ingave bij vraag 2" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb2.Text = ResultOfAnswer(false, 1);
                }

                try
                {
                    if (Convert.ToDouble(tb3.Text.Replace('.', ',')) == answers[2])
                    {
                        points++;
                        tb3.Text += ResultOfAnswer(true);
                    }
                    else
                    {
                        tb3.Text = ResultOfAnswer(false, 2);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ongeldige ingave bij vraag 3" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb3.Text = ResultOfAnswer(false, 2);
                }

                try
                {
                    if (Convert.ToDouble(tb4.Text.Replace('.', ',')) == answers[3])
                    {
                        points++;
                        tb4.Text += ResultOfAnswer(true);
                    }
                    else
                    {
                        tb4.Text = ResultOfAnswer(false, 3);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ongeldige ingave bij vraag 4" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb4.Text = ResultOfAnswer(false, 3);
                }

                try
                {
                    if (Convert.ToDouble(tb5.Text.Replace('.', ',')) == answers[4])
                    {
                        points++;
                        tb5.Text += ResultOfAnswer(true);
                    }
                    else
                    {
                        tb5.Text = ResultOfAnswer(false, 4);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ongeldige ingave bij vraag 5" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb5.Text = ResultOfAnswer(false, 4);
                }

                points = (int)(points * 1.5 + difficulty * 0.84);

                string[] records = DB.FindFirst(MathStudents, "ID", Convert.ToString(studentId));
                records[5] = Convert.ToString(points);
                DB.ChangeFromRead(MathStudents, studentId, records);

                isCompleted = true;
                return points;//score op 5 + moeilijkheidsgraad * 1.67 (0-5ptn waard)
            }
            else
            {
                string[] points = DB.FindFirst(MathStudents, "ID", Convert.ToString(studentId));
                MessageBox.Show("you are not allowed to re-do this test", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return Convert.ToInt32(points[5]);
            }
        }

        //info from: stackexchange.com
        private int[] SelectRandomQuestions(int amountOfNumbers, int minValue, int maxValue) //make array with amount of variables and return it
        {
            string[] test;
            int[] array = new int[amountOfNumbers];

            if (amountOfNumbers <= maxValue - minValue + 1) //check if there are enough unique numbers to choose from otherwise msgbox-popup
            {
                for (int i = 0; i < amountOfNumbers; i++)
                {
                    array[i] = r1.Next(minValue, maxValue + 1);

                    test = DB.FindFirst(questions, "ID", Convert.ToString(array[i]));
                    if (test != null)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            if (array[i] == array[j]) //if random number already in array, counter - 1 and break out of this for-loop
                            {
                                i--;
                                break;
                            }
                        }
                    }
                    else
                    {
                        i--;
                    }
                }
            }
            else
            {
                string errormsg = String.Format("You requested {0} numbers, there are only {1} numbers available!", amountOfNumbers, maxValue - minValue);
                MessageBox.Show(errormsg);
            }

            return array;
        }

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

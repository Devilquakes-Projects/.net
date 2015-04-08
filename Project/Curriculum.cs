using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Project
{
    public abstract class Curriculum
    {
        //Instance Variables:
        private Random r1 = new Random();
        private DispatcherTimer t1;//TODO for difficulty level.

        private int[] answers = new int[5];
        private bool isCompleted;
        private int studentId;

        //dbfiles to use: placed here for easy editing later on!
        protected string mathStudents;
        private string questions;
        private int amountOfQuestionsNeeded;//hangt af van hoeveel vragen u wenst te ontvangen!
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


        //klasse leerstof!

        //moeilijkheidsgraad: difficulty

        //editing: aanpassen leerstof

        //overzicht opvragen

        public Curriculum() { }

        public Curriculum(Label time, Label l1, Label l2, Label l3, Label l4, Label l5, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, int studentId, int difficulty)
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

            timer = 45 / difficulty;
            time.Background = Brushes.Gray;

            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromSeconds(1);
            t1.Tick += t1_Tick;
        }

        //timer:
        void t1_Tick(object sender, EventArgs e)
        {
            time.Content = String.Format("Time: {0} seconds", timer);
            if (timer <= 0)
            {
                Grade();//when timer runs out grade this test!
            }
            timer--;
        }

        //Setters:
        public int AmountOfQuestions
        {
            get { return amountOfQuestionsNeeded; }
            set { amountOfQuestionsNeeded = value; }
        }


        public string StudentsFile
        {
            get { return mathStudents; }
            set { mathStudents = value; }
        }

        public string QuestionsFile
        {
            get { return questions; }
            set { questions = value; }
        }

        //Methods:
        public void UpdateCurriculum()
        {

            string[] studentTestCompleted = DB.FindFirst(mathStudents, "ID", Convert.ToString(studentId));//opvragen db student

            if (studentTestCompleted[5].Equals("false"))
            {
                this.isCompleted = false;
            }
            else
            {
                this.isCompleted = true;//this moet niet maar overzichtelijker
                MessageBox.Show("NOTE: you already completed this test, you won't gain points for doing this test again", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            t1.Start();
        }

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
                string errorMsg = String.Format("You requested {0} numbers, there are only {1} numbers available!", amountOfNumbers, maxValue - minValue);
                MessageBox.Show(errorMsg);
            }

            return array;
        }

        //private string ResultOfAnswer(bool isCorrect, int i = 0)  //gewijzigd op 08/04/15 19:30-20:00
        //{
        //    if (isCorrect)
        //    {
        //        return " = Correct answer!";
        //    }
        //    else
        //    {
        //        return "Wrong should be: " + answers[i];
        //    }
        //}

        public int Grade()
        {
            t1.Stop();

            if (isCompleted == false)
            {
                int points = 0;
                string correctAnswer = " = Correct answer!";
                string invalidInput = "ongeldige ingave bij vraag 1" + Environment.NewLine + "U krijgt voor deze vraag een 0";
                string wrongAnswer = "Wrong, answer should be: ";

                try
                {
                    if (Convert.ToDouble(tb1.Text.Replace('.', ',')) == answers[0]) //replace om . en , toe te staan.
                    {
                        points++;
                        tb1.Text += correctAnswer;
                    }
                    else
                    {
                        tb1.Text = wrongAnswer + answers[0];
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show(invalidInput);
                    tb1.Text = wrongAnswer + answers[0];
                }

                try
                {
                    if (Convert.ToDouble(tb2.Text.Replace('.', ',')) == answers[1])
                    {
                        points++;
                        tb2.Text += correctAnswer;
                    }
                    else
                    {
                        tb2.Text = wrongAnswer + answers[1];
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ongeldige ingave bij vraag 2" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb2.Text = wrongAnswer + answers[1];
                }

                try
                {
                    if (Convert.ToDouble(tb3.Text.Replace('.', ',')) == answers[2])
                    {
                        points++;
                        tb3.Text += correctAnswer;
                    }
                    else
                    {
                        tb3.Text = wrongAnswer + answers[2];
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ongeldige ingave bij vraag 3" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb3.Text = wrongAnswer + answers[2];
                }

                try
                {
                    if (Convert.ToDouble(tb4.Text.Replace('.', ',')) == answers[3])
                    {
                        points++;
                        tb4.Text += correctAnswer;
                    }
                    else
                    {
                        tb4.Text = wrongAnswer + answers[3];
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ongeldige ingave bij vraag 4" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb4.Text = wrongAnswer + answers[3];
                }

                try
                {
                    if (Convert.ToDouble(tb5.Text.Replace('.', ',')) == answers[4])
                    {
                        points++;
                        tb5.Text += correctAnswer;
                    }
                    else
                    {
                        tb5.Text = wrongAnswer + answers[4];
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ongeldige ingave bij vraag 5" + Environment.NewLine + "U krijgt voor deze vraag een 0");
                    tb5.Text = wrongAnswer + answers[4];
                }

                if (timer > 0)//bonus points if test complete before end of time!
                {
                    points = (int)(points * 1.5 + difficulty * 0.84);
                }
                else
                {
                    points = (int)(points * 1.5);
                }

                string[] records = DB.FindFirst(mathStudents, "ID", Convert.ToString(studentId));
                records[5] = Convert.ToString(points);
                DB.ChangeFromRead(mathStudents, studentId, records);

                isCompleted = true;
                return points;//score op 5 + moeilijkheidsgraad * 1.67 (0-3ptn waard)
            }
            else
            {
                string[] points = DB.FindFirst(mathStudents, "ID", Convert.ToString(studentId));
                MessageBox.Show("you are not allowed to re-do this test", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return Convert.ToInt32(points[5]);
            }
        }

        public void LoadQuestions()
        {
            int totalLines;
            int visibleLines;
            DB.LineCount(questions, out visibleLines, out totalLines);//THE NEW WAY

            if (visibleLines >= 5)
            {
                int[] randomNumbers = SelectRandomQuestions(amountOfQuestionsNeeded, 1, totalLines);
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
        //add another method here?
    }
}

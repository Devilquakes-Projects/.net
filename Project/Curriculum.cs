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

        //timer:
        private DispatcherTimer t1;
        private Label time;
        private int timer;

        //questions:
        private string[] answers;
        //private bool isCompleted;//overbodig door Environment.Close();
        private int studentId;

        //dbfiles to use:
        protected string mathStudents;
        private string questions;
        private int amountOfQuestionsNeeded;
        private int difficulty;

        //labels:
        private Label l1;
        private Label l2;
        private Label l3;
        private Label l4;
        private Label l5;
        private Label l6;

        //textboxes:
        private TextBox tb1;
        private TextBox tb2;
        private TextBox tb3;
        private TextBox tb4;
        private TextBox tb5;
        private TextBox tb6;

        //Buttons:
        private Button gradeButton;

        //overzicht opvragen

        public Curriculum() { }

        public Curriculum(int studentId, int difficulty, Label time, Button gradeButton, Label l1, TextBox tb1, Label l2, TextBox tb2, Label l3, TextBox tb3, Label l4, TextBox tb4, Label l5, TextBox tb5, Label l6, TextBox tb6) /*Label l1, Label l2, Label l3, Label l4, Label l5, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5*/
        {
            this.l1 = l1;
            this.l2 = l2;
            this.l3 = l3;
            this.l4 = l4;
            this.l5 = l5;
            this.l6 = l6;

            this.time = time;
            this.tb1 = tb1;
            this.tb2 = tb2;
            this.tb3 = tb3;
            this.tb4 = tb4;
            this.tb5 = tb5;
            this.tb6 = tb6;

            this.gradeButton = gradeButton;

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
                gradeButton.Content = "Exit";

                int grade = Grade();//when timer runs out grade this test!
                string result = String.Format("You earned {0}/10 points.", grade);
                MessageBox.Show(result);
            }
            timer--;
        }

        //Setters:
        public int AmountOfQuestions
        {
            get { return amountOfQuestionsNeeded; }
            set
            {
                amountOfQuestionsNeeded = value;
                //QUESTIONMAARK!!!
                answers = new string[amountOfQuestionsNeeded];
            }
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
                t1.Start();
            }
            else
            {
                //this.isCompleted = true;//this moet niet maar overzichtelijker    //overbodig door System.Environment.Exit(0);
                MessageBox.Show("NOTE: you already completed this test," + Environment.NewLine + "Closing application.", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                System.Environment.Exit(0);//exitcode 0 means closed properly. --> close window here because you already did the test!
            }
        }

        private int[] SelectRandomQuestions(int amountOfNumbers, int minValue, int maxValue) //make array with amount of variables and return it
        {
            string[] test;
            int[] array = new int[amountOfNumbers];

            if (amountOfNumbers < maxValue - minValue) //check if there are enough unique numbers to choose from otherwise msgbox-popup
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

        private void LockTextBlock(TextBox tb, bool isCorrect)
        {
            tb.IsReadOnly = true;

            if (isCorrect)
            {
                tb.Background = Brushes.LightGreen;
            }
            else
            {
                tb.Background = Brushes.LightPink;
            }
        }

        public int Grade()
        {
            t1.Stop();

            //hide "Grade" Button:
            time.Width = 100.0;
            time.Content = "Test Completed!";

            //if (isCompleted == false) //overbodig door System.Environment.Exit(0);
            //{
            int points = 0;
            string correctAnswer = " = Correct answer!";
            string invalidInput = "ongeldige ingave bij vraag 1" + Environment.NewLine + "U krijgt voor deze vraag een 0";
            string wrongAnswer = " = Wrong, answer should be: ";


            if (tb1 != null)
            {
                if (tb1.Text.Equals(answers[0]))
                {
                    points++;
                    tb1.Text += correctAnswer;
                    LockTextBlock(tb1, true);
                }
                else
                {
                    tb1.Text += wrongAnswer + answers[0];
                    LockTextBlock(tb1, false);
                }

                if (tb2 != null)
                {
                    if (tb2.Text.Equals(answers[1]))
                    {
                        points++;
                        tb2.Text += correctAnswer;
                        LockTextBlock(tb2, true);
                    }
                    else
                    {
                        tb2.Text += wrongAnswer + answers[1];
                        LockTextBlock(tb2, false);
                    }

                    if (tb3 != null)
                    {
                        if (tb3.Text.Equals(answers[2]))
                        {
                            points++;
                            tb3.Text += correctAnswer;
                            LockTextBlock(tb3, true);
                        }
                        else
                        {
                            tb3.Text += wrongAnswer + answers[2];
                            LockTextBlock(tb3, false);
                        }

                        if (tb4 != null)
                        {
                            if (tb4.Text.Equals(answers[3]))
                            {
                                points++;
                                tb4.Text += correctAnswer;
                                LockTextBlock(tb4, true);
                            }
                            else
                            {
                                tb4.Text += wrongAnswer + answers[3];
                                LockTextBlock(tb4, false);
                            }

                            if (tb5 != null)
                            {
                                if (tb5.Text.Equals(answers[4]))
                                {
                                    points++;
                                    tb5.Text += correctAnswer;
                                    LockTextBlock(tb5, true);
                                }
                                else
                                {
                                    tb5.Text += wrongAnswer + answers[4];
                                    LockTextBlock(tb5, false);
                                }

                                if (tb6 != null)
                                {
                                    if (tb6.Text.Equals(answers[5]))
                                    {
                                        points++;
                                        tb5.Text += correctAnswer;
                                        LockTextBlock(tb6, true);
                                    }
                                    else
                                    {
                                        tb6.Text = wrongAnswer + answers[5];
                                        LockTextBlock(tb6, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (timer > 0)//bonus points if test complete before end of time!
            {
                points = (int)(Math.Round((points * 1.5 + difficulty * 0.84), MidpointRounding.AwayFromZero));
            }
            else
            {
                points = (int)(Math.Round((points * 1.5), MidpointRounding.AwayFromZero));
            }

            string[] records = DB.FindFirst(mathStudents, "ID", Convert.ToString(studentId));
            records[5] = Convert.ToString(points);
            DB.ChangeFromRead(mathStudents, studentId, records);

            //isCompleted = true;
            return points;//score op 5 + moeilijkheidsgraad * 1.67 (0-3ptn waard)

            //}     //overbodig door System.Environment.Exit(0);
            //else
            //{
            //    string[] points = DB.FindFirst(mathStudents, "ID", Convert.ToString(studentId));
            //    MessageBox.Show("you are not allowed to re-do this test", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return Convert.ToInt32(points[5]);
            //}
        }

        public void LoadQuestions()
        {
            int totalLines;
            int visibleLines;
            DB.LineCount(questions, out visibleLines, out totalLines);//THE NEW WAY

            if (visibleLines >= amountOfQuestionsNeeded)
            {
                int[] randomNumbers = SelectRandomQuestions(amountOfQuestionsNeeded, 1, totalLines);
                string[] isEnabled;

                for (int i = 0; i < randomNumbers.Length; i++)
                {
                    answers[i] = Convert.ToString(randomNumbers[i]);
                    isEnabled = DB.FindFirst(questions, "ID", Convert.ToString(answers[i]));

                    //if (isEnabled == null)//overbodige test: 10/04
                    //{
                    //    i--;
                    //}
                }

                string[] labels;

                if (l1 != null)
                {
                    labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[0]));
                    l1.Content = labels[2];//show question
                    answers[0] = Convert.ToString(labels[3]); //save answer index

                    if (l2 != null)
                    {
                        labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[1]));
                        l2.Content = labels[2];
                        answers[1] = Convert.ToString(labels[3]);

                        if (l3 != null)
                        {
                            labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[2]));
                            l3.Content = labels[2];
                            answers[2] = Convert.ToString(labels[3]);

                            if (l4 != null)
                            {
                                labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[3]));
                                l4.Content = labels[2];
                                answers[3] = Convert.ToString(labels[3]);

                                if (l5 != null)
                                {
                                    labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[4]));
                                    l5.Content = labels[2];
                                    answers[4] = Convert.ToString(labels[3]);

                                    if (l6 != null)
                                    {
                                        labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[5]));
                                        l6.Content = labels[2];
                                        answers[5] = Convert.ToString(labels[3]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Not enough questions in the questions-list," + Environment.NewLine + "ask a teacher to fix this", "Error: Not enough Questions.");
            }
        }
        //add another method here...
    }
}

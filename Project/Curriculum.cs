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
        //timer:
        private DispatcherTimer t1;
        private Label time;
        private int timer;

        //questions:
        private string[,] answers;
        //private bool isCompleted;//overbodig door Environment.Close();
        private int studentId;

        //dbfiles to use:
        protected string mathStudents;
        private string questions;
        private int amountOfQuestionsNeeded;
        private int maxAmountOfAnswersPQ;
        private int difficulty;

        //labels:
        public Label l1;
        public Label l2;
        public Label l3;
        public Label l4;
        public Label l5;
        public Label l6;

        //textboxes:
        private TextBox tb1;
        private TextBox tb2;
        private TextBox tb3;
        private TextBox tb4;
        private TextBox tb5;
        private TextBox tb6;

        //Buttons:
        private Button gradeButton;

        //Constructors:
        public Curriculum() { }

        public Curriculum(int studentId, int difficulty, Button gradeButton, Label time,Label title, Label question1, Label question2, Label header1, Label header2, Label header3 ,TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, TextBox tb6)
        {
            this.studentId = studentId;
            this.difficulty = difficulty;

            this.gradeButton = gradeButton;

            this.time = time;//6,4,5,3 --> 3,4,5,6    title: 6-3, header 1 = ok, header 2 = ok, header 3 = 3-6
            this.l1 = question1;
            this.l2 = question2;
            this.l3 = title;//NOTE: vragen worden ge-set vanaf 1! hier 2 labels voor vragen dus 1+2 worden overschreven!
            this.l4 = header1;
            this.l5 = header2;
            this.l6 = header3;
            
            this.tb1 = tb1;
            this.tb2 = tb2;
            this.tb3 = tb3;
            this.tb4 = tb4;
            this.tb5 = tb5;
            this.tb6 = tb6;

            SetupTimer(difficulty, time);
        }

        public Curriculum(int studentId, int difficulty, Button gradeButton, Label time, Label l1, Label l2, Label l3, Label l4, Label l5, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5)
        {
            this.studentId = studentId;
            this.difficulty = difficulty;

            this.gradeButton = gradeButton;

            this.time = time;
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

            SetupTimer(difficulty, time);
        }

        private void SetupTimer(int difficulty, Label time)
        {
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

        protected void StopTimer()
        {
            t1.Stop();
        }

        //Getters/Setters:
        public int SetAmountOfQuestions
        {
            set { amountOfQuestionsNeeded = value; }
        }

        public int SetAmountOfAnswersPerQuestion
        {
            set { maxAmountOfAnswersPQ = value/* + 1*/; }
        }

        //Files to use:
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

        //Getters/Setters-labels:
        protected string Title
        {
            set { l3.Content = value; }
        }

        protected string Header1
        {
            set { l4.Content = value; }
        }

        protected string Header2
        {
            set { l5.Content = value; }
        }
        protected string Header3
        {
            set { l6.Content = value; }
        }

        //getter/setters-textboxes:
        public string Tb1
        {
            get { return tb1.Text; }
            set { tb1.Text = value; }
        }
        public string Tb2
        {
            get { return tb2.Text; }
            set { tb2.Text = value; }
        }
        public string Tb3
        {
            get { return tb3.Text; }
            set { tb3.Text = value; }
        }
        public string Tb4
        {
            get { return tb4.Text; }
            set { tb4.Text = value; }
        }
        public string Tb5
        {
            get { return tb5.Text; }
            set { tb5.Text = value; }
        }
        public string Tb6
        {
            get { return tb6.Text; }
            set { tb6.Text = value; }
        }

        //setter to influence the amount of given time for the exercises:
        public int SetTimer
        {
            get { return timer; }
            set { timer = value; }
        }

        //Methods:
        protected void InitializeArray()
        {
            answers = new string[amountOfQuestionsNeeded, maxAmountOfAnswersPQ];
        }

        public void UpdateCurriculum(int index)//also tests if course has been completed!
        {
            string[] studentTestCompleted = DB.FindFirst(mathStudents, "ID", Convert.ToString(studentId));//opvragen db student

            if (studentTestCompleted[index].Equals("false"))
            {
                t1.Start();
            }
            else
            {
                MessageBox.Show("NOTE: you already completed this test," + Environment.NewLine + "Closing application.", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                System.Environment.Exit(0);//exitcode 0 means closed properly.
            }
        }

        private int[] SelectRandomQuestions(int amountOfNumbers, int minValue, int maxValue) //make array with amount of variables and return it
        {
            string[] test;
            Random r1 = new Random();
            int[] array = new int[amountOfNumbers];

            if (amountOfNumbers < maxValue - minValue) //check if there are enough unique numbers to choose from otherwise msgbox-popup
            {
                maxValue++;

                for (int i = 0; i < amountOfNumbers; i++)
                {
                    array[i] = r1.Next(minValue, maxValue);

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

        public virtual int Grade()
        {
            t1.Stop();//perhaps optional to remove?

            time.Width = 100.0;
            time.Content = "Test Completed!";

            int points = 0;//=0mag weg nadat alles in if ok is!
            string correctAnswer = " = Correct answer!";
            string wrongAnswer = " = Wrong, answer should be: ";

            if (this is Mathematics)
            {
                Console.WriteLine("This is a mathematics class!");
                points = OneQuestionPerAnwser(correctAnswer, wrongAnswer);  //deze bij one on one questions (oftewel bij Mathematics)
                WriteRecords(5, points);
            }
            else if (this is Languages)
            {
                Console.WriteLine("This is a Languages class!");
                points = OneQuestionThreeAnswers(correctAnswer, wrongAnswer);
                WriteRecords(6, points);
            }
            else
            {
                Console.WriteLine("another class? note geography should come here!");
            }

            return points;
        }

        private void WriteRecords(int index, int points)
        {
            string[] records = DB.FindFirst(mathStudents, "ID", Convert.ToString(studentId));
            records[index] = Convert.ToString(points);
            DB.ChangeFromRead(mathStudents, studentId, records);
        }

        private int OneQuestionThreeAnswers(string correctAnswer, string wrongAnswer)
        {
            //answers[,]: .rank = aantal rijen, .length: alles dat erin zit!, .length / .rank = aantal kolommen
            int points = 0;

            if (tb1 != null)
            {
                if (tb1.Text.Equals(answers[0, 0]))
                {
                    points++;
                    tb1.Text += correctAnswer;
                    LockTextBlock(tb1, true);//color + lock textbox
                }
                else
                {
                    tb1.Text += wrongAnswer + answers[0, 0];
                    LockTextBlock(tb1, false);
                }

                if (tb2 != null)
                {
                    if (tb2.Text.Equals(answers[0, 1]))
                    {
                        points++;
                        tb2.Text += correctAnswer;
                        LockTextBlock(tb2, true);
                    }
                    else
                    {
                        tb2.Text += wrongAnswer + answers[0, 1];
                        LockTextBlock(tb2, false);
                    }
                }

                if (tb3 != null)
                {
                    if (tb3.Text.Equals(answers[0, 2]))
                    {
                        points++;
                        tb3.Text += correctAnswer;
                        LockTextBlock(tb3, true);
                    }
                    else
                    {
                        tb3.Text += wrongAnswer + answers[0, 2];
                        LockTextBlock(tb3, false);
                    }
                }




                if (tb4 != null)
                {
                    if (tb4.Text.Equals(answers[1, 0]))
                    {
                        points++;
                        tb4.Text += correctAnswer;
                        LockTextBlock(tb4, true);//color + lock textbox
                    }
                    else
                    {
                        tb4.Text += wrongAnswer + answers[1, 0];
                        LockTextBlock(tb4, false);
                    }

                    if (tb5 != null)
                    {
                        if (tb5.Text.Equals(answers[1, 1]))
                        {
                            points++;
                            tb5.Text += correctAnswer;
                            LockTextBlock(tb5, true);
                        }
                        else
                        {
                            tb5.Text += wrongAnswer + answers[1, 1];
                            LockTextBlock(tb5, false);
                        }
                    }

                    if (tb6 != null)
                    {
                        if (tb6.Text.Equals(answers[1, 2]))
                        {
                            points++;
                            tb6.Text += correctAnswer;
                            LockTextBlock(tb6, true);
                        }
                        else
                        {
                            tb6.Text += wrongAnswer + answers[1, 2];
                            LockTextBlock(tb6, false);
                        }
                    }
                }
            }

            //pts forumule Languages:
            if (timer > 0)//bonus points if test complete before end of time!
            {
                points = (int)(Math.Round((points * 1.25 + difficulty * 0.84), MidpointRounding.AwayFromZero));
            }
            else
            {
                points = (int)(Math.Round((points * 1.25), MidpointRounding.AwayFromZero));
            }

            return points;
        }

        private int OneQuestionPerAnwser(string correctAnswer, string wrongAnswer)
        {
            int points = 0;

            if (tb1 != null)
            {
                if (tb1.Text.Equals(answers[0, 0]))
                {
                    points++;
                    tb1.Text += correctAnswer;
                    LockTextBlock(tb1, true);
                }
                else
                {
                    tb1.Text += wrongAnswer + answers[0, 0];
                    LockTextBlock(tb1, false);
                }

                if (tb2 != null)
                {
                    if (tb2.Text.Equals(answers[1, 0]))
                    {
                        points++;
                        tb2.Text += correctAnswer;
                        LockTextBlock(tb2, true);
                    }
                    else
                    {
                        tb2.Text += wrongAnswer + answers[1, 0];
                        LockTextBlock(tb2, false);
                    }

                    if (tb3 != null)
                    {
                        if (tb3.Text.Equals(answers[2, 0]))
                        {
                            points++;
                            tb3.Text += correctAnswer;
                            LockTextBlock(tb3, true);
                        }
                        else
                        {
                            tb3.Text += wrongAnswer + answers[2, 0];
                            LockTextBlock(tb3, false);
                        }

                        if (tb4 != null)
                        {
                            if (tb4.Text.Equals(answers[3, 0]))
                            {
                                points++;
                                tb4.Text += correctAnswer;
                                LockTextBlock(tb4, true);
                            }
                            else
                            {
                                tb4.Text += wrongAnswer + answers[3, 0];
                                LockTextBlock(tb4, false);
                            }

                            if (tb5 != null)
                            {
                                if (tb5.Text.Equals(answers[4, 0]))
                                {
                                    points++;
                                    tb5.Text += correctAnswer;
                                    LockTextBlock(tb5, true);
                                }
                                else
                                {
                                    tb5.Text += wrongAnswer + answers[4, 0];
                                    LockTextBlock(tb5, false);
                                }

                                if (tb6 != null)
                                {
                                    if (tb6.Text.Equals(answers[5, 0]))
                                    {
                                        points++;
                                        tb5.Text += correctAnswer;
                                        LockTextBlock(tb6, true);
                                    }
                                    else
                                    {
                                        tb6.Text = wrongAnswer + answers[5, 0];
                                        LockTextBlock(tb6, false);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //pts formule math:
            if (timer > 0)//bonus points if test complete before end of time!
            {
                points = (int)(Math.Round((points * 1.5 + difficulty * 0.84), MidpointRounding.AwayFromZero));
            }
            else
            {
                points = (int)(Math.Round((points * 1.5), MidpointRounding.AwayFromZero));
            }

            return points;
        }

        public void LoadQuestions()
        {
            int totalLines;
            int visibleLines;
            DB.LineCount(questions, out visibleLines, out totalLines);

            if (visibleLines >= amountOfQuestionsNeeded)
            {
                int[] randomNumbers;

                randomNumbers = SelectRandomQuestions(amountOfQuestionsNeeded, 1, totalLines);//headers MOETEN IN EEN VISIBLE: FALSE REGEL STAAN!!!!!!!!!!!!!
               
                string[] isEnabled;

                for (int i = 0; i < randomNumbers.Length; i++)
                {
                    isEnabled = DB.FindFirst(questions, "ID", Convert.ToString(Convert.ToString(randomNumbers[i])));

                    for (int j = 3; j < isEnabled.Length; j++)
                    {
                        answers[i, j - 3] = isEnabled[j];
                    }
                }

                string[] labels;

                if (1 <= answers.GetLength(0))
                {
                    labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[0]));

                    l1.Content = labels[2];//show question

                    if (2 <= answers.GetLength(0))
                    {
                        labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[1]));
                        l2.Content = labels[2];

                        if (3 <= answers.GetLength(0))
                        {
                            labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[2]));
                            l3.Content = labels[2];

                            if (4 <= answers.GetLength(0))
                            {
                                labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[3]));
                                l4.Content = labels[2];

                                if (5 <= answers.GetLength(0))
                                {
                                    labels = DB.FindFirst(questions, "Id", Convert.ToString(randomNumbers[4]));
                                    l5.Content = labels[2];
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

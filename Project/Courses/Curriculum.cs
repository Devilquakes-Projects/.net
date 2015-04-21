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
        private int timer;

        //questions:
        private string[,] answers;
        private int studentId;

        //dbfiles to use:
        private string studentsFile;
        private string questions;
        private int amountOfQuestionsNeeded;
        private int difficulty;

        //Constructors:
        public Curriculum() { }

        public Curriculum(int studentId, int difficulty)
        {
            this.studentId = studentId;
            this.difficulty = difficulty;

            this.timer = 45/difficulty;
        }
        
        //Getter: answers
        protected string[,] Answers
        {
            get { return answers; }
        }

        //Getters/Setters: Questions:
        protected int SetAmountOfQuestions
        {
            set { amountOfQuestionsNeeded = value; }
        }

        //Getters/Setters: Files to use:
        public string StudentsFile
        {
            get { return studentsFile; }
            set { studentsFile = value; }
        }

        public string QuestionsFile
        {
            get { return questions; }
            set { questions = value; }
        }

        
        //Getter: difficulty to use in Grade subclasses:
        protected int GetDifficulty
        {
            get { return difficulty; }
        }

        //Getter/Setter: to influence the amount of given time for the exercises:
        protected int Timer
        {
            get { return timer; }
            set { timer = value; }
        }

        //Methods:
        protected bool ShouldTimerStopRunningt(Label timeLabel)
        {
            timer--;
            timeLabel.Content = String.Format(" {0} Seconds", timer);
            return timer <= 0;
        }

        protected void InitializeArray(int maxAmountOfAnswersPerQuestion)
        {
            answers = new string[amountOfQuestionsNeeded, maxAmountOfAnswersPerQuestion];
        }

        protected void MsgPopupBox(int questionNumber)
        {
            MessageBox.Show(String.Format("Invalid input in box {0}, you receive 0 points for this question!", questionNumber));
        }

        protected DispatcherTimer SetupTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            return timer;
        }

        protected void IsTestGraded(int index)//also tests if course has been completed!
        {
            string[] studentTestCompleted = DB.FindFirst(studentsFile, "ID", Convert.ToString(studentId));//opvragen db student

            if (!studentTestCompleted[index].Equals("false"))
            {
                MessageBox.Show("NOTE: you already completed this test," + Environment.NewLine + "Closing application.", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                System.Environment.Exit(0);//exitcode 0 means closed properly
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

        protected void LockTextBlock(TextBox tb, bool isCorrect)
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

        protected void StartGradeValues(out string correctAnswer, out string wrongAnswer)
        {
            correctAnswer = " = Correct answer!";
            wrongAnswer = " = Wrong, answer should be: ";
        }

        public abstract void Grade();

        protected void ShowResults(int pts)
        {
            string result = String.Format("You earned {0}/{1} points.", pts, 10);//int 10 mag ook string "10" zijn
            MessageBox.Show(result);
        }

        protected void UpdateTimeLabel(Label time)
        {
            //time.Width = 100.0;
            time.Background = Brushes.LightGray;
        }

        protected void SetTextboxStartSize(double tbWidth = 200, double tbMaxWidth = 1.25, int amountOfCharactersAllowed = 20, TextBox tb1 = null, TextBox tb2 = null, TextBox tb3 = null, TextBox tb4 = null, TextBox tb5 = null, TextBox tb6 = null)
        {
                if (tb1 != null)
                {
                    tb1.MinWidth = tbWidth;
                    tb1.MaxWidth = tbWidth * tbMaxWidth;
                    tb1.MaxLength = amountOfCharactersAllowed;

                    if (tb2 != null)
                    {
                        tb2.MinWidth = tbWidth;
                        tb2.MaxWidth = tbWidth * tbMaxWidth;
                        tb2.MaxLength = amountOfCharactersAllowed;

                        if (tb3 != null)
                        {
                            tb3.MinWidth = tbWidth;
                            tb3.MaxWidth = tbWidth * tbMaxWidth;
                            tb3.MaxLength = amountOfCharactersAllowed;

                            if (tb4 != null)
                            {
                                tb4.MinWidth = tbWidth;
                                tb4.MaxWidth = tbWidth * tbMaxWidth;
                                tb4.MaxLength = amountOfCharactersAllowed;

                                if (tb5 != null)
                                {
                                    tb5.MinWidth = tbWidth;
                                    tb5.MaxWidth = tbWidth * tbMaxWidth;
                                    tb5.MaxLength = amountOfCharactersAllowed;

                                    if (tb6 != null)
                                    {
                                        tb6.MinWidth = tbWidth;
                                        tb6.MaxWidth = tbWidth * tbMaxWidth;
                                        tb6.MaxLength = amountOfCharactersAllowed;
                                    }
                                }
                            }
                        }
                    }
                }
        }

        protected void WriteRecords(int index, int points)
        {
            string[] records = DB.FindFirst(studentsFile, "ID", Convert.ToString(studentId));
            records[index] = Convert.ToString(points);
            DB.ChangeFromRead(studentsFile, studentId, records);
        }

        protected string[] LoadQuestions()
        {
            string[] questionsList = new string[answers.GetLength(0)];

            int totalLines;
            int visibleLines;
            DB.LineCount(questions, out visibleLines, out totalLines);

            if (visibleLines >= amountOfQuestionsNeeded)
            {
                string[] isEnabled;
                int[] randomNumbers = SelectRandomQuestions(amountOfQuestionsNeeded, 1, totalLines);//headers van Languages en Geography MOETEN IN EEN "VISIBLE: FALSE" REGEL STAAN!!!!!!!!!!!!!

                for (int i = 0; i < randomNumbers.Length; i++)
                {
                    isEnabled = DB.FindFirst(questions, "ID", Convert.ToString(Convert.ToString(randomNumbers[i])));
                    questionsList[i] = isEnabled[2];

                    for (int j = 3; j < isEnabled.Length; j++)
                    {
                        answers[i, j - 3] = isEnabled[j];
                    }
                }
            }
            else
            {
                MessageBox.Show("Not enough questions in the questions-list," + Environment.NewLine + "ask a teacher to fix this", "Error: Not enough Questions.");
            }

            return questionsList;
        }

        protected void GradeButtonToExit(Button gradebutton, Label timeLabel)
        {
            gradebutton.Content = "Exit";
            timeLabel.Width = 90.0;
            timeLabel.Content = "Test Complete";
        }
        //add another method here...
    }
}

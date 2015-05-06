using Project.Controllers;
using Project.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

// Auther: Gregory Malomgré
// Date: 04/04/2015 10:00
namespace Project.Controllers.Courses
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
        private string studentsFile = ProjectConfig.StudentsFile;
        private string questions;
        private int amountOfQuestionsNeeded;
        private int difficulty;

        //Constructors:
        public Curriculum() { }

        public Curriculum(int studentId, int difficulty)
        {
            this.studentId = studentId;
            this.difficulty = difficulty;

            this.timer = 45 / difficulty;
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
        protected bool ShouldTimerStopRunning(Label timeLabel)
        {
            timer--;
            timeLabel.Content = String.Format(" {0} Seconds", timer);
            return timer <= 0;
        }

        protected void InitializeArray(int maxAmountOfAnswersPerQuestion)
        {
            answers = new string[amountOfQuestionsNeeded, maxAmountOfAnswersPerQuestion];
        }

        protected DispatcherTimer SetupTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            return timer;
        }

        protected void IsTestGraded(int index)//also tests if course has been completed!
        {
            try
            {
                string[] studentTestCompleted = DB.FindFirst(studentsFile, "userID", Convert.ToString(studentId));//opvragen db student
                if (!studentTestCompleted[index].Equals("false"))
                {
                    MessageBox.Show("NOTE: you already completed this test," + Environment.NewLine + "Closing application.", "Notification", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    System.Environment.Exit(0);//exitcode 0 means closed properly
                }
            }
            catch (NoRecordFoundException)
            {
                string[] records = { Convert.ToString(User.Id), "false", "false", "false" };
                DB.AddRecord(studentsFile, records);
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

                    try
                    {
                        test = DB.FindFirst(questions, "ID", Convert.ToString(array[i]));
                        for (int j = 0; j < i; j++)
                        {
                            if (array[i] == array[j]) //if random number already in array, counter - 1 and break out of this for-loop
                            {
                                i--;
                                break;
                            }
                        }
                    }
                    catch (NoRecordFoundException)
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

        protected void WriteRecords(int index, int points)
        {
            string[] records = DB.FindFirst(studentsFile, "userID", Convert.ToString(studentId));
            records[index] = Convert.ToString(points);
            DB.ChangeFromRead(studentsFile, Convert.ToInt32(records[0]), records);
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

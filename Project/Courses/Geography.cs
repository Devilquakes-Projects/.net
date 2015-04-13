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
    public class Geography : Curriculum
    {
        private DispatcherTimer dpTimer;
        private Button gradeButton;
        private Label timeLabel;

        private GroupBox qBoxTitle1;
        private GroupBox qBoxTitle2;

        private RadioButton rb1;
        private RadioButton rb2;
        private RadioButton rb3;
        private RadioButton rb4;

        public Geography(int studentId, int difficulty, Button gradeButton, Label time, GroupBox qBoxTitle1, RadioButton rb1, RadioButton rb2, GroupBox qBoxTitle2, RadioButton rb3, RadioButton rb4) : base(studentId, difficulty)
        {
            base.QuestionsFile = "Courses_Geography";
            base.StudentsFile = "Courses";

            base.SetAmountOfQuestions = 2;  //2 questions
            base.InitializeArray(2);        //2 answers per question
            base.IsTestGraded(7);           //7 is index of file Courses (pts for geography are in this index)

            this.gradeButton = gradeButton;
            gradeButton.Content = "Grade";

            this.timeLabel = time;
            base.UpdateTimeLabel(time);

            this.qBoxTitle1 = qBoxTitle1;
            this.qBoxTitle2 = qBoxTitle2;

            this.rb1 = rb1;
            this.rb2 = rb2;
            this.rb3 = rb3;
            this.rb4 = rb4;

            string[] questionList = base.LoadQuestions();
            qBoxTitle1.Header = questionList[0];
            qBoxTitle2.Header = questionList[1];

            RandomQuestions(rb1, rb2, rb3, rb4);
            
            dpTimer = base.SetupTimer();
            dpTimer.Tick += dpTimer_Tick;
            dpTimer.Start();
        }

        void dpTimer_Tick(object sender, EventArgs e)
        {
            if (base.ShouldTimerStopRunningt(timeLabel))
            {
                int grade = Grade();
                MessageBox.Show(String.Format("You earned {0}/10 points.", grade));
            }
        }

        private void RandomQuestions(RadioButton rb1, RadioButton rb2, RadioButton rb3, RadioButton rb4)
        {
            string[,] answers = base.Answers;
            Random generator = new Random();
            int randomNumber = generator.Next(2);

            if (randomNumber == 0)
            {
                rb1.Content = answers[0, 0];
                rb2.Content = answers[0, 1];
            }
            else
            {
                rb1.Content = answers[0, 1];
                rb2.Content = answers[0, 0];
            }

            int seed = DateTime.Now.Millisecond;
            seed = seed + (int)generator.Next(10000);//is this really necessary?

            randomNumber = generator.Next(2);

            if (randomNumber == 0)
            {
                rb3.Content = answers[1, 0];
                rb4.Content = answers[1, 1];
            }
            else
            {
                rb3.Content = answers[1, 1];
                rb4.Content = answers[1, 0];
            }
        }

        public override int Grade()
        {
            dpTimer.Stop();

            //GroupBoxes no longer editable:
            qBoxTitle1.IsEnabled = false;
            qBoxTitle2.IsEnabled = false;

            int points = 0;

            string correctAnswer;
            string wrongAnswer;
            base.StartGradeValues(out correctAnswer, out wrongAnswer);

            string[,] answers = base.Answers;

            //set colored boxes:
            bool firstbox;
            Brush colorCorrect = new SolidColorBrush(Colors.Green);
            Brush colorWrong = new SolidColorBrush(Colors.Red);

            if (rb1.Content.Equals(answers[0, 0]))
            {
                firstbox = true;
                rb1.Foreground = colorCorrect;
                rb2.Foreground = colorWrong;
            }
            else
            {
                firstbox = false;
                rb1.Foreground = colorWrong;
                rb2.Foreground = colorCorrect;
            }

            if (firstbox)
            {
                if (rb1.IsChecked == true)
                {
                    points++;
                }
                else if (rb2.IsChecked == true)
                {
                    points++;
                }
            }

            if (rb3.Content.Equals(answers[1, 0]))
            {
                firstbox = true;
                rb3.Foreground = colorCorrect;
                rb4.Foreground = colorWrong;
            }
            else
            {
                firstbox = false;
                rb3.Foreground = colorWrong;
                rb4.Foreground = colorCorrect;
            }

            if (firstbox)
            {
                if (rb3.IsChecked == true)
                {
                    points++;
                }
                else if (rb4.IsChecked == true)
                {
                    points++;
                }
            }

            if (base.Timer > 0)
            {
                points = (int)(Math.Round(((points * 3.5) + base.GetDifficulty), 0, MidpointRounding.AwayFromZero));
            }
            else
            {
                points = (int)(Math.Round(points * 3.5));
            }

            base.GradeButtonToExit(gradeButton, timeLabel);
            base.WriteRecords(7, points);//index 7 is for column of Geography points!
            
            return points;
        }
    }
}

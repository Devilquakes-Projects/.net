using Project.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Project
{
    class Languages : Curriculum
    {
        private DispatcherTimer dpTimer;

        private Button gradeButton;

        private Label time;
        private Label title;
        private Label question1;
        private Label question2;
        private Label header1;
        private Label header2;
        private Label header3;

        private TextBox tb1;
        private TextBox tb2;
        private TextBox tb3;
        private TextBox tb4;
        private TextBox tb5;
        private TextBox tb6;

        public Languages(int studentId, int difficulty, Button gradeButton, Label time, Label title, Label question1, Label question2, Label header1, Label header2, Label header3, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, TextBox tb6, int bonusTime = 20)
            : base(studentId, difficulty)
        {
            base.QuestionsFile = "Courses_Lang";
            base.StudentsFile = "Courses";

            base.SetAmountOfQuestions = 2;
            base.InitializeArray(3);

            base.IsTestGraded(6);//6: index of Languages

            this.gradeButton = gradeButton;
            gradeButton.Content = "Grade";

            this.time = time;
            base.UpdateTimeLabel(time);

            this.title = title;
            this.question1 = question1;
            this.question2 = question2;
            this.header1 = header1;
            this.header2 = header2;
            this.header3 = header3;

            this.tb1 = tb1;
            this.tb2 = tb2;
            this.tb3 = tb3;
            this.tb4 = tb4;
            this.tb5 = tb5;
            this.tb6 = tb6;

            LoadHeaderLabels();

            string[] questions = base.LoadQuestions();
            question1.Content = questions[0];
            question2.Content = questions[1];

            string[,] answers = base.Answers;

            dpTimer = base.SetupTimer();
            base.Timer += bonusTime;//bonusTime: this test is just too hard without it!
            dpTimer.Tick += dpTimer_Tick;
            dpTimer.Start();
        }

        private void LoadHeaderLabels()
        {
            string[] headers = DB.FindFirst(base.QuestionsFile, "ID", "1", onlyVisible: false);
            title.Content = headers[2];
            header1.Content = headers[3];
            header2.Content = headers[4];
            header3.Content = headers[5];
        }

        void dpTimer_Tick(object sender, EventArgs e)
        {
            if (base.ShouldTimerStopRunningt(time))
            {
                int grade = Grade();
                MessageBox.Show(String.Format("You earned {0}/10 points.", grade));
            }
        }

        public override int Grade()//makes sure that answers are stored in uppercase, is this necessary!?
        {
            dpTimer.Stop();

            ConvertInputToCapitals();

            int points = 0;
            string[,] answers = base.Answers;

            string correctAnswer;
            string wrongAnswer;
            base.StartGradeValues(out correctAnswer, out wrongAnswer);

            if (tb1.Text.Equals(Answers[0, 0]))
            {
                points++;
                tb1.Text += correctAnswer;
                LockTextBlock(tb1, true);//color + lock textbox
            }
            else
            {
                tb1.Text += wrongAnswer + Answers[0, 0];
                LockTextBlock(tb1, false);
            }

            if (tb2.Text.Equals(Answers[0, 1]))
            {
                points++;
                tb2.Text += correctAnswer;
                LockTextBlock(tb2, true);
            }
            else
            {
                tb2.Text += wrongAnswer + Answers[0, 1];
                LockTextBlock(tb2, false);
            }


            if (tb3.Text.Equals(Answers[0, 2]))
            {
                points++;
                tb3.Text += correctAnswer;
                LockTextBlock(tb3, true);
            }
            else
            {
                tb3.Text += wrongAnswer + Answers[0, 2];
                LockTextBlock(tb3, false);
            }

            if (tb4.Text.Equals(Answers[1, 0]))
            {
                points++;
                tb4.Text += correctAnswer;
                LockTextBlock(tb4, true);//color + lock textbox
            }
            else
            {
                tb4.Text += wrongAnswer + Answers[1, 0];
                LockTextBlock(tb4, false);
            }


            if (tb5.Text.Equals(Answers[1, 1]))
            {
                points++;
                tb5.Text += correctAnswer;
                LockTextBlock(tb5, true);
            }
            else
            {
                tb5.Text += wrongAnswer + Answers[1, 1];
                LockTextBlock(tb5, false);
            }

            if (tb6.Text.Equals(Answers[1, 2]))
            {
                points++;
                tb6.Text += correctAnswer;
                LockTextBlock(tb6, true);
            }
            else
            {
                tb6.Text += wrongAnswer + Answers[1, 2];
                LockTextBlock(tb6, false);
            }

            //pts forumule Languages:
            if (Timer > 0)//bonus points if test complete before end of time!
            {
                points = (int)(Math.Round((points * 1.25 + GetDifficulty * 0.84), MidpointRounding.AwayFromZero));
            }
            else
            {
                points = (int)(Math.Round((points * 1.25), MidpointRounding.AwayFromZero));
            }

            base.GradeButtonToExit(gradeButton, time);
            base.WriteRecords(6, points);//index 6 is for column of language points!

            return points;
        }

        private void ConvertInputToCapitals()
        {
            //ingave op hoofdletters zetten:
            tb1.Text = tb1.Text.ToUpper();
            tb2.Text = tb2.Text.ToUpper();
            tb3.Text = tb3.Text.ToUpper();
            tb4.Text = tb4.Text.ToUpper();
            tb5.Text = tb5.Text.ToUpper();
            tb6.Text = tb6.Text.ToUpper();
        }
    }
}

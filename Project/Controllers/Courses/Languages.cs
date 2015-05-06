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

// Auther: Gregory Malomgré
// Date: 10/04/2015 16:00
namespace Project.Controllers.Courses
{
    class Languages : Courses_TextBoxClass
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

        private TextBox[] tb = new TextBox[6];

        public Languages(int studentId, int difficulty, Button gradeButton, Label time, Label title, Label question1, Label question2, Label header1, Label header2, Label header3, TextBox tb1, TextBox tb2, TextBox tb3, TextBox tb4, TextBox tb5, TextBox tb6, int bonusTime = 20)
            : base(studentId, difficulty)
        {
            base.QuestionsFile = ProjectConfig.QuestionsFileLang;

            base.SetAmountOfQuestions = 2;
            base.InitializeArray(3);

            base.IsTestGraded(4);//4: index of Languages

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

            base.SetTextboxStartSize(200, 1.25, 74, tb1, tb2, tb3, tb4, tb5, tb6);//setup textboxes
            this.tb[0] = tb1;
            this.tb[1] = tb2;
            this.tb[2] = tb3;
            this.tb[3] = tb4;
            this.tb[4] = tb5;
            this.tb[5] = tb6;

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

        private void dpTimer_Tick(object sender, EventArgs e)
        {
            if (base.ShouldTimerStopRunning(time))
            {
                Grade();
            }
        }

        public override void Grade()//makes sure that answers are stored in uppercase, is this necessary!?
        {
            dpTimer.Stop();

            ConvertInputToCapitals();

            int points = 0;
            string[,] answers = base.Answers;

            string correctAnswer;
            string wrongAnswer;
            base.StartGradeValues(out correctAnswer, out wrongAnswer);

            int x = 0;
            int y = 0;
            for (int i = 0; i < tb.Length; i++)
            {
                if (x >= 3)
                {
                    x = 0;
                    y = 1;
                }
                if (tb[i].Text.Equals(Answers[y, x]))
                {
                    points++;
                    tb[i].Text += correctAnswer;
                    LockTextBlock(tb[i], true);//color + lock textbox
                }
                else
                {
                    tb[i].Text += wrongAnswer + Answers[y, x];
                    LockTextBlock(tb[i], false);
                }
                x++;
            }

            //pts forumule Languages:
            if (Timer > 0 && points > 0)//bonus points if test complete before end of time!
            {
                    points = (int)(Math.Round((points * 1.25 + GetDifficulty * 0.84), MidpointRounding.AwayFromZero));
            }
            else
            {
                points = (int)(Math.Round((points * 1.25), MidpointRounding.AwayFromZero));
            }

            base.GradeButtonToExit(gradeButton, time);
            base.WriteRecords(4, points);//index 4 is for column of language points!

            base.ShowResults(points);
        }

        private void ConvertInputToCapitals()//Author: Greg, Date:14-04-15 14:00 - 15:00
        {
            //ingave op hoofdletters zetten:
            for (int i = 0; i < tb.Length; i++)
            {
                tb[i].Text = tb[i].Text.ToUpper();
            }
        }
    }
}

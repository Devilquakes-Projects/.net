using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Project
{
    /// <summary>
    /// Interaction logic for GregoryTest.xaml
    /// </summary>
    public partial class GregoryTestWindow : Window
    {
        private Mathematics m1;

        public GregoryTestWindow()
        {
            InitializeComponent();

            int studentId = 1;
            int difficulty = 3;

            m1 = new Mathematics(studentId, difficulty, timeLabel, gradeButton, l1, tb1, l2, tb2, l3, tb3, l4, tb4, l5, tb5);
            m1.LoadQuestions();
        }

        private void Button_Click_Grade(object sender, RoutedEventArgs e)
        {
            /*
                test dbfiles die ik op dit moment gebruik:

                01DBProject\COURSES.txt:
                ID|VISIBLE|CLASS|SURNAME|FIRSTNAME|Math_Tasks_Score|Language_Tasks_Score|Geography_Tasks_Score
                1|true|tine|achternaam|voornaam|false|false|false

                01DBProject\COURSES_MATH_QUESTIONS.txt:
                ID|VISIBLE|QUESTION|SOLUTION
                1|true|1x1|1
                2|true|2x2|4
                3|true|3x3|9
                4|true|4x4|16
                5|true|5x5|25
                6|true|6x6|36
                7|true|7x7|49
                8|true|8x8|64
                9|true|9x9|81
                10|true|10x10|100
            */
            //Mathematics Code:

            if (gradeButton.Content.Equals("Grade"))
            {
                //int grade = m1.Grade();
                int grade = m1.Grade();
                string result = String.Format("You earned {0}/10 points.", grade);
                gradeButton.Content = "Exit";
                MessageBox.Show(result);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}

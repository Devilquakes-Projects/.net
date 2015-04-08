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
            m1 = new Mathematics(timeLabel, l1, l2, l3, l4, l5, tb1, tb2, tb3, tb4, tb5, 1, 3);//labels-tboxes-stud_id-difficulty
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //test dbfiles die ik op dit moment gebruik:

            //01DBProject\COURSES_MATH.txt:
            //ID|VISIBLE|CLASS|SURNAME|FIRSTNAME|Math_Tasks_Score|Language_Tasks_Score|Geography_Tasks_Score
            //1|true|tine|achternaam|voornaam|false|false|false

            //01DBProject\COURSES_MATH_QUESTIONS.txt:
            //ID|VISIBLE|QUESTION|SOLUTION
            //1|true|1x1|1
            //2|true|2x2|4
            //3|true|3x3|9
            //4|true|4x4|16
            //5|true|5x5|25
            //6|true|6x6|36
            //7|true|7x7|49
            //8|true|8x8|64
            //9|true|9x9|81
            //10|true|10x10|100

            int grade = m1.Grade();
            string result = String.Format("You earned {0}/10 points.", grade);
            MessageBox.Show(result);
        }

        private void Window_Loaded_GregoryTest(object sender, RoutedEventArgs e)//07/04_13:00-13:10 updated window loaded event for this class
        {
            m1.LoadQuestions();
        }
    }
}

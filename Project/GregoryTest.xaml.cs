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
    public partial class GregoryTest : Window
    {
        private Mathematics m1;

        public GregoryTest()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //aanmaken database met:
            /*string classList = "Courses_Math";
            string questions = "Courses_Math_Questions";
            string[] input = { "class", "surname", "firstname", "completion" };
            DBController.MakeDB(classList, input);
            input = new []{"tine", "achternaam", "voornaam", "*"};//stackoverflow: reuse array
            DBController.AddRecord(classList, input);
            input = new[] { "tine", "lastnameofsomeone", "frontname of someone", "*" };
            DBController.AddRecord(classList, input);
            input = new[] { "Question", "Solution" };
            DBController.MakeDB(questions, input);
            input = new[] { "6x6", "36" };
            DBController.AddRecord(questions, input);
            input = new[] { "5x5", "25" };
            DBController.AddRecord(questions, input);
            input = new[] { "2x2", "4" };
            DBController.AddRecord(questions, input);*/

            int grade = m1.Grade();
            string result = String.Format("You earned {0}/10 points.", grade);
            MessageBox.Show(result);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)//window-loaded event!
        {
            m1 = new Mathematics(timeLabel, l1, l2, l3, l4, l5, tb1, tb2, tb3, tb4, tb5, 1, 2);//labels-tboxes-stud_id-difficulty
            m1.LoadQuestions();
            Console.WriteLine("test");
        }
    }
}

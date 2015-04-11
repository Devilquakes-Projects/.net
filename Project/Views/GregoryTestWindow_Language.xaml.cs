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

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for GregoryTestWindow_Language.xaml
    /// </summary>
    public partial class GregoryTestWindow_Language : Window
    {
        private Languages lang;
        public GregoryTestWindow_Language()
        {
            InitializeComponent();

            
            int studentId = 2;
            int difficulty = 3;

            lang = new Languages(studentId, difficulty, timeLabel, gradeButton, question1Label, tb1, question2Label, tb2, tb3: tb3, tb4: tb4, tb5: tb5, tb6: tb6);
            lang.LoadQuestions();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            /*
                test dbfiles die ik op dit moment gebruik:

                01DBProject\COURSES.txt:
                ID|VISIBLE|CLASS|SURNAME|FIRSTNAME|Math_Tasks_Score|Language_Tasks_Score|Geography_Tasks_Score
                1|true|tine|achternaam|voornaam|false|false|false
             
                01DBProject\COURSES_Lang.txt
            */

            if (gradeButton.Content.Equals("Grade"))
            {
                int grade = lang.Grade();
                string result = String.Format("You earned {0}/10 points.", grade);
                MessageBox.Show(result);
                gradeButton.Content = "Exit";
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}

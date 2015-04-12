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
            string titleName = "Vervoegingen";

            lang = new Languages(studentId, difficulty, gradeButton, timeLabel, title, question1Label, question2Label, header1, header2, header3, tb1, tb2, tb3, tb4, tb5, tb6);
            lang.LoadQuestions();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
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

        private void Button_Click_Grade(object sender, RoutedEventArgs e)
        {
            if (gradeButton.Content.Equals("Grade"))
            {
                int grade = lang.Grade();
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

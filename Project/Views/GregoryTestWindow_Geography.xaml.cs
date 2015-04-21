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
    /// Interaction logic for GregoryTestWindow_Geography.xaml
    /// </summary>
    public partial class GregoryTestWindow_Geography : Window
    {
        private Geography geography;

        public GregoryTestWindow_Geography()
        {
            InitializeComponent();

            //onderstaande int's moeten van buitenaf doorgegeven worden (via constructor), ik set deze tijdelijk op deze manier:
            int studentId = 4;
            int difficulty = 3;
            
            geography = new Geography(studentId, difficulty, gradeButton, timeLabel, firstQuestionBoxTitle, rButton1_1, rButton1_2, secondQuestionBoxTitle, rButton2_1, rButton2_2);
            //geography.LoadQuestions();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (gradeButton.Content.Equals("Grade"))
            {
                geography.Grade();
                
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}

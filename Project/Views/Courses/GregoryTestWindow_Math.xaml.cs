using Project.Controllers;
using Project.Controllers.Courses;
using Project.Exceptions;
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

            //onderstaande int's moeten van buitenaf doorgegeven worden (via constructor), ik set deze tijdelijk op deze manier:
            int studentId = User.Id;
            int difficulty = ProjectConfig.QuestionDifficulty;

            try
            {
                m1 = new Mathematics(studentId, difficulty, timeLabel, gradeButton, l1, l2, l3, l4, l5, tb1, tb2, tb3, tb4, tb5);
            }
            catch (CourseAlreadyCompletedException exeptionObject)
            {
                MessageBox.Show(exeptionObject.Message + Environment.NewLine + "Application will now close", "Notification:", MessageBoxButton.OK, MessageBoxImage.Information);
                Environment.Exit(1);
            }
            catch (NotEnoughQuestionsException exceptionObject)
            {
                MessageBox.Show(exceptionObject.Message + Environment.NewLine + "Application will now close", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }

        private void Button_Click_Grade(object sender, RoutedEventArgs e)
        {
            if (gradeButton.Content.Equals("Grade"))
            {
                m1.Grade();
            }
            else
            {
                MainWindow newView = new MainWindow();
                newView.Show();
                this.Close();
            }
        }
    }
}

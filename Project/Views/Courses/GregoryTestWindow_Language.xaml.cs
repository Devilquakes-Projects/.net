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

            //onderstaande int's moeten van buitenaf doorgegeven worden (via constructor), ik set deze tijdelijk op deze manier:
            int studentId = User.Id;
            int difficulty = ProjectConfig.QuestionDifficulty;

            try
            {
                lang = new Languages(studentId, difficulty, gradeButton, timeLabel, title, question1Label, question2Label, header1, header2, header3, tb1, tb2, tb3, tb4, tb5, tb6);
            }
            catch (CourseAlreadyCompletedException exeptionObject)
            {
                MessageBox.Show(exeptionObject.Message + Environment.NewLine + "Application will now go to the previous screen", "Notification:", MessageBoxButton.OK, MessageBoxImage.Information);
                ThrowWindowClosedException();
                //Environment.Exit(1);
            }
            catch (NotEnoughQuestionsException exceptionObject)
            {
                MessageBox.Show(exceptionObject.Message + Environment.NewLine + "Application will now go to the previous screen", "Error:", MessageBoxButton.OK, MessageBoxImage.Error);
                ThrowWindowClosedException();
                //Environment.Exit(1);
            }
        }

        private void Button_Click_Grade(object sender, RoutedEventArgs e)
        {
            if (gradeButton.Content.Equals("Grade"))
            {
                lang.Grade();
            }
            else
            {
                MainWindow newView = new MainWindow();
                newView.Show();
                this.Close();
            }
        }

        private void ThrowWindowClosedException()
        {
            this.Close();
            throw new WindowClosedInSubclassException();
        }
    }
}

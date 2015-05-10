// Author: Joris Meylaers

using Project.Views;
using Project.Controllers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.Exceptions;

namespace Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (User.LoggedIn)
            {
                if (User.Permission == 1)
                {
                    teacherMenu.Visibility = Visibility.Visible;
                }
                logoutButton.Visibility = Visibility.Visible;
            }
            else
            {
                loginButton.Visibility = Visibility.Visible;
                vakkenMenu.Visibility = Visibility.Collapsed;
                gameMenu.Visibility = Visibility.Collapsed;
                highscoresMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow newView = new LoginWindow();
            newView.Show();
            this.Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            User.Logout();
            MainWindow newView = new MainWindow();
            newView.Show();
            this.Close();
        }

        private void registreerButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWindow = new RegisterWindow();
            regWindow.Show();
            this.Close();
        }

        private void passwordrecoveryButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecoveryWindow pasWindow = new PasswordRecoveryWindow();
            pasWindow.Show();
            this.Close();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void gregoryButton_Click_Math(object sender, RoutedEventArgs e)
        {
            try
            {
                MathWindow newView = new MathWindow();
                newView.Show();
            }
            catch (NoWindowEnabledException exceptionObject)
            {
                //Console.WriteLine("you threw an exception in the class Curriculum in the IsTestGraded() Method" + Environment.NewLine + "exception details: " + Environment.NewLine + exceptionObject.ToString());
                MainWindow newView = new MainWindow();
                newView.Show();
            }

                this.Close();
        }

        private void gregoryButton_Click_Language(object sender, RoutedEventArgs e)
        {
            try
            {
                LanguageWindow newView = new LanguageWindow();
                newView.Show();
            }
            catch (NoWindowEnabledException exceptionObject)
            {
                //Console.WriteLine("you threw an exception in the class Curriculum in the IsTestGraded() Method" + Environment.NewLine + "exception details: " + Environment.NewLine + exceptionObject.ToString());
                MainWindow newView = new MainWindow();
                newView.Show();
            }

                this.Close();
        }

        private void gregoryButton_Click_Geography(object sender, RoutedEventArgs e)
        {
            try
            {
                GeographyWindow newView = new GeographyWindow();
                newView.Show();
            }
            catch (NoWindowEnabledException exceptionObject)
            {
                //Console.WriteLine("you threw an exception in the class Curriculum in the IsTestGraded() Method" + Environment.NewLine + "exception details: " + Environment.NewLine + exceptionObject.ToString());
                MainWindow newView = new MainWindow();
                newView.Show();
            }

                this.Close();
        }

        private void SnakeButtonButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectConfig.CheckPlayTime();

            if (ProjectConfig.PlayTime != 0)
            {
                SnakeWindow snakeView = new SnakeWindow();
                snakeView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Voor te spelen moet je eerst alle oefeningen maken.");
            }
        }

        private void BallGameButtonButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectConfig.CheckPlayTime();

            if (ProjectConfig.PlayTime != 0)
            {
                BallWindow ballView = new BallWindow();
                ballView.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Voor te spelen moet je eerst alle oefeningen maken.");
            }
        }

        private void GlobalHighscoresButton_Click(object sender, RoutedEventArgs e)
        {
            GlobalHighscores newView = new GlobalHighscores();
            newView.Show();
            this.Close();
        }

        private void highscorePerUserButtonButton_Click(object sender, RoutedEventArgs e)
        {
            HighscorePerUser newView = new HighscorePerUser();
            newView.Show();
            this.Close();
        }

        private void addQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionWindow newView = new AddQuestionWindow();
            newView.Show();
            this.Close();
        }

        private void editQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            EditQuestionWindow newView = new EditQuestionWindow();
            newView.Show();
            this.Close();
        }
    }
}

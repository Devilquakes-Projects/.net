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
using Project.Views;
using Project.Controllers;

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

        private void gregoryButton_Click_Math(object sender, RoutedEventArgs e)
        {
            GregoryTestWindow newView = new GregoryTestWindow();
            newView.Show();
            this.Close();
        }
        private void gregoryButton_Click_Language(object sender, RoutedEventArgs e)//added by greg on 10/04
        {
            GregoryTestWindow_Language newView = new GregoryTestWindow_Language();
            newView.Show();
            this.Close();
        }

        private void gregoryButton_Click_Geography(object sender, RoutedEventArgs e)//added by greg on 10/04
        {
            GregoryTestWindow_Geography newView = new GregoryTestWindow_Geography();
            newView.Show();
            this.Close();
        }

        private void addQuestionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void editQuestionButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MathematicsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            AddQuestionWindow newView = new AddQuestionWindow();
            newView.Show();
            this.Close();
        }

        private void snakeButton_Click(object sender, RoutedEventArgs e)
        {
            SnakeWindow newView = new SnakeWindow();
            newView.Show();
            this.Close();
        }
    }
}

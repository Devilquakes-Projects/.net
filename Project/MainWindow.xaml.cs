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
            if (UserController.LoggedIn)
            {
                if (UserController.Permission == 1)
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
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            UserController.Logout();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void gregoryButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

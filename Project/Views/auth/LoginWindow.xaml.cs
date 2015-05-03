// Auther: Joris Meylaers
// Date: 

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
using Project.Controllers;
using Project.Exceptions;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for Welkom.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Auther: Joren Martens
            // Date: 31/03/2015 19:32
            // Implemented Login
            string userName = usernameTextBox.Text;
            string pass = passwordTextBox.Password;

            try
            {
                User.Login(userName, pass);

                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                this.Close();
            }
            catch (ArgumentNullException ex)
            {
                // een veld = 0
            }
            catch (UserNotFoundException)
            {
                // user niet gevonden
            }
            catch(InvalidPasswordException)
            {
                // password klopt niet
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Close();
        }
    }
}

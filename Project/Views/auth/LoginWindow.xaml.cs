// Auther: Joris Meylaers
// Date: 

using Project.Controllers;
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
            catch (ArgumentNullException)
            {
                errorLabel.Content = "Gelieve alle velden in te vullen";
                errorLabel.Visibility = Visibility.Visible;
            }
            catch (UserNotFoundException)
            {
                errorLabel.Content = "Gebruiker en/of wachtwoord foutief";
                errorLabel.Visibility = Visibility.Visible;
            }
            catch(InvalidPasswordException)
            {
                errorLabel.Content = "Gebruiker en/of wachtwoord foutief";
                errorLabel.Visibility = Visibility.Visible;
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow register = new RegisterWindow();
            register.Show();
            this.Close();
        }

        private void vergetenButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordRecoveryWindow newView = new PasswordRecoveryWindow();
            newView.Show();
            this.Close();
        }
    }
}

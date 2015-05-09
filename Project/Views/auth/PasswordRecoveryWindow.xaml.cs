// Auther: Joren Martens
// Date: 03/05/2015 21:25

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
    /// Interaction logic for PasswordRecoveryWindow.xaml
    /// </summary>
    public partial class PasswordRecoveryWindow : Window
    {
        public PasswordRecoveryWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = usernameTextBox.Text;
            string pass = passwordTextBox.Password;
            string teacherCode = leerkrachtCodePasswordBox.Password;

            try
            {
                User.Recover(userName, pass, teacherCode);

                MessageBox.Show("Wachtwoord is gewijzigd");
                MainWindow newView = new MainWindow();
                newView.Show();
                this.Close();
            }
            catch (ArgumentNullException)
            {
                errorLabel.Content = "Gelieve alle velden in te vullen";
                errorLabel.Visibility = Visibility.Visible;
            }
            catch (InvalidTeacherCodeException)
            {
                errorLabel.Content = "foutieve leerkrachtcode";
                errorLabel.Visibility = Visibility.Visible;
            }
            catch (UserNotFoundException)
            {
                errorLabel.Content = "Gebruiker niet gevonden";
                errorLabel.Visibility = Visibility.Visible;
            }
        }
    }
}

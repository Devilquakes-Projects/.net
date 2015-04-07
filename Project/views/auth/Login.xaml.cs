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

namespace Project
{
    /// <summary>
    /// Interaction logic for Welkom.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            // Auther: Joren Martens
            // Date: 31/03/2015 19:32
            // Implemented Login
            string userName = usernameTextBox.Text;
            string pass = passwordTextBox.Password;

            if (UserController.Login(userName, pass))
            {
                MainWindow mainwindow = new MainWindow();
                mainwindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Gebruikersnaam en/of wachtwoord fout.");
            }
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Close();
        }

    }
}

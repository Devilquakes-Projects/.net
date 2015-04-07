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

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for Registratie.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            // Auther: Joren Martens
            // Date: 31/03/2015 20:36
            // Implemented Register
            string userName = usernameTextBox.Text;
            string pass = passwordTextBox.Password;
            string name = nameTextBox.Text;
            string lastName = lastNameTextBox.Text;

            if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(pass) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(lastName))
            {
                MessageBox.Show("Vul alle velden in");
            }
            else
            {
                if (User.Register(userName, pass, name, lastName))
                {
                    MainWindow hoofdvenster = new MainWindow();
                    hoofdvenster.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("username bestaat al.");
                }
            }
        }

    }
}

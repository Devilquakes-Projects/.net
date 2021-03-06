﻿// Auther: Joris Meylaers

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
    /// Interaction logic for Registratie.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Auther: Joren Martens
            // Date: 31/03/2015 20:36
            // Implemented Register
            string userName = usernameTextBox.Text;
            string pass = passwordTextBox.Password;
            string name = nameTextBox.Text;
            string lastName = lastNameTextBox.Text;
            string teacher = teacherCodeTextBox.Text;
            string classText = classTextBox.Text;

            try
            {
                User.Register(userName, pass, name, lastName, teacher, classText);

                MainWindow hoofdvenster = new MainWindow();
                hoofdvenster.Show();
                this.Close();
            }
            catch (ArgumentNullException)
            {
                errorLabel.Content = "Gelieve alle velden in te vullen";
                errorLabel.Visibility = Visibility.Visible;
            }
            catch (InvalidTeacherCodeException)
            {
                errorLabel.Content = "Foutieve leerkrachtcode";
                errorLabel.Visibility = Visibility.Visible;
            }
            catch (UserAlreadyExistsException)
            {
                errorLabel.Content = "Gebruiker bestaat al";
                errorLabel.Visibility = Visibility.Visible;
            }
            
        }

    }
}

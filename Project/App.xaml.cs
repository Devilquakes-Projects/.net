using Project.Controllers;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        // Auther: Joren Martens
        // Date: 03/05/2015
        private App()
        {
            // load everything
            ProjectConfig.StartUp();

            // load window
            LoginWindow startWindow = new LoginWindow();
            startWindow.Show();
        }
    }
}

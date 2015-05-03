using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Project.Controllers;

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
            MainWindow mainwindow = new MainWindow();
            mainwindow.Show();
        }
    }
}

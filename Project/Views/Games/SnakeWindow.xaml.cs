// Author: Joris Meylaers
// Date: 14/04/2015
// Time: 14:17

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
using Project.Games.SnakeGame;

namespace Project.Views
{
    /// <summary>
    /// Interaction logic for SnakeWindow.xaml
    /// </summary>
    public partial class SnakeWindow : Window
    {
        public SnakeWindow()
        {
            InitializeComponent();
            Snake s = new Snake(snakeCanvas, totalPointsLabel);

            ProjectConfig.CheckPlayTime();

            if (ProjectConfig.PlayTime != 0)
            {
                s.TimeLeft = ProjectConfig.PlayTime;
                s.startGame();
            }
        }

        private void snakeCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.Key, true);
        }

        private void snakeCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.Key, false);
        }

        public void closeWindow()
        {
            Environment.Exit(0);
        }
    }
}

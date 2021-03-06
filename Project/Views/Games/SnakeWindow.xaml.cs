﻿// Author: Joris Meylaers

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
            Snake s = new Snake(snakeCanvas, totalPointsLabel, timeLeftLabel);

            s.TimeLeft = ProjectConfig.PlayTime;
            s.startGame();
            
        }

        private void snakeCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.Key, true);
        }

        private void snakeCanvas_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.Key, false);
        }
    }
}

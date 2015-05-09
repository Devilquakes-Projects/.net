using Project.Games.BallGame;
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
    /// Interaction logic for BallWindow.xaml
    /// </summary>
    public partial class BallWindow : Window
    {
        private int amountBalls = 15;
        private int radius = 15;
        private Point mousePoint;
        private int time = ProjectConfig.PlayTime;
        private List<Ball> ballList = new List<Ball>();
        private BallGame game;
        private Random randomNumber = new Random();
        private int lives = 5;

        private Ball ballPlayer;
        private Ball ball;
        private int playerRadius = 15;
        private bool gameStarted = false;

        public BallWindow()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            ballPlayer = new PlayerBall(canvas, playerRadius);

            for (int i = 0; i < amountBalls; i++)
            {
                if (i <= Math.Ceiling((amountBalls) / 2.0))
                {
                    ball = new ComputerBall(true, radius, randomNumber);
                }
                else
                {
                    ball = new ComputerBall(false, radius, randomNumber);
                }

                ballList.Add(ball);
            }
            game = new BallGame(ballList, ballPlayer, lives, time, canvas);
            game.StartGame();
            gameStarted = true;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (gameStarted)
            {
                mousePoint = Mouse.GetPosition(this);
                mousePoint.X -= canvas.Margin.Left;
                mousePoint.Y -= canvas.Margin.Top;
                if (mousePoint.X - ballPlayer.Radius > 0 && mousePoint.X < canvas.Width - ballPlayer.Radius
                    && mousePoint.Y - ballPlayer.Radius > 0 && mousePoint.Y < canvas.Height - ballPlayer.Radius)
                {
                    ballPlayer.Position = mousePoint;
                    ballPlayer.UpdateEllipse();
                }
            }
        }

        private void canvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (gameStarted)
            {
                this.Cursor = Cursors.None;
            }
        }

        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }
    }
}

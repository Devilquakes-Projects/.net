using Project.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Project.Games.BallGame
{
    class BallGame
    {
        private DispatcherTimer gameFrameTimer = new DispatcherTimer(DispatcherPriority.Render);
        private DispatcherTimer timeLeftTimer = new DispatcherTimer();
        private Label timeLeftLabel;
        private Label totalLivesLabel;
        private Label totalPointsLabel;
        private List<Ball> ballList;
        private Canvas canvas;
        private bool constantSpeed = true;
        private Ball ballPlayer;
        private int lives;
        private int points = 0;
        private int timePlayed = 0;
        private int timeLeft;
        private Random random;

        public BallGame(List<Ball> ballList, Ball playerBall, int lives, int time, Canvas canvas, Label timeLeftLabel, Label totalLivesLabel, Label totalPointsLabel)
        {
            this.timeLeftLabel = timeLeftLabel;
            this.totalLivesLabel = totalLivesLabel;
            this.totalPointsLabel = totalPointsLabel;
            this.ballList = ballList;
            this.ballPlayer = playerBall;
            this.canvas = canvas;
            this.lives = lives;
            this.timeLeft = time;

            random = new Random();

            gameFrameTimer.Interval = TimeSpan.FromMilliseconds(10);
            timeLeftTimer.Interval = TimeSpan.FromSeconds(1);
            gameFrameTimer.Tick += gameFrameTimer_Tick;
            timeLeftTimer.Tick += timeLeftTimer_Tick;
        }

        private void timeLeftTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            timeLeftLabel.Content = timeLeft;
            timePlayed++;
            if (timeLeft == 0)
            {
                StopGame("You ran out of time!", "No time left");
            }           
        }

        public void StartGame()
        {
            SpawnBalls();
            gameFrameTimer.Start();
        }

        public void StopGame(string message, string title)
        {
            gameFrameTimer.Stop();
            timeLeftTimer.Stop();

            if (MessageBox.Show(message, title, MessageBoxButton.OK) == MessageBoxResult.OK)
            {
                // save score
                string[] records = { Convert.ToString(User.Id), Convert.ToString(DateTime.Now), Convert.ToString(timePlayed), Convert.ToString(points) };
                DB.AddRecord(ProjectConfig.BallFile, records);

                if (timeLeft <= 0)
                {
                    // save cource in progress to default db. and reset cource in progress
                    string[] studentCourcePointsTemp = DB.FindFirst(ProjectConfig.StudentsFile, "userID", Convert.ToString(User.Id));
                    string[] studentCourcePoints = new string[5];
                    studentCourcePoints[0] = Convert.ToString(User.Id);
                    studentCourcePoints[1] = Convert.ToString(DateTime.Now);
                    for (int i = 2; i <= 4; i++)
                    {
                        studentCourcePoints[i] = studentCourcePointsTemp[i + 1];
                    }
                    DB.AddRecord(ProjectConfig.StudentPointsFile, studentCourcePoints);

                    string[] newStudentCourcePoints = { Convert.ToString(User.Id), "false", "false", "false" };
                    DB.ChangeRecord(ProjectConfig.StudentsFile, Convert.ToInt32(studentCourcePointsTemp[0]), newStudentCourcePoints);
                }

                string[] timeArray = DB.FindFirst(ProjectConfig.PlayTimeFile, "userID", Convert.ToString(User.Id));
                timeArray[3] = Convert.ToString(timeLeft);
                DB.ChangeFromRead(ProjectConfig.PlayTimeFile, Convert.ToInt32(timeArray[0]), timeArray);
                ProjectConfig.PlayTime = timeLeft;

                if (MessageBox.Show(message, title, MessageBoxButton.OK) == MessageBoxResult.OK)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    for (int intCounter = App.Current.Windows.Count - 2; intCounter >= 0; intCounter--)
                        App.Current.Windows[intCounter].Close();
                }
            }
        }

        private void gameFrameTimer_Tick(object sender, EventArgs e)
        {
            // check speed
            CheckSpeed();

            for (int i = 0; i < ballList.Count; i++)
            {
                CheckBorderHit(i);
                CheckCollision(i);
            }
            for (int i = 0; i < ballList.Count; i++)
            {
                ballList[i].Position = new Point(ballList[i].Position.X + ((ballList[i].Speed / 10.0) * ballList[i].XChange), ballList[i].Position.Y + ((ballList[i].Speed / 10.0) * ballList[i].YChange));
                ballList[i].UpdateEllipse();
            }
        }

        private void SpawnBalls()
        {
            for (int i = 0; i < ballList.Count; i++)
            {
                for (int j = 0; j < ballList.Count; j++)
                {
                    if (ballList[i] != ballList[j])
                    {
                        spawnBall(ballList[i], ballList[j]);
                    }
                }
            }

            foreach (Ball ball in ballList)
            {
                ball.DrawEllipse(canvas);
            }
        }

        private void CheckBorderHit(int i)
        {
            if ((ballList[i].Position.X - ballList[i].Radius <= 0) || (ballList[i].Position.X + ballList[i].Radius >= canvas.Width))
            {
                ballList[i].XChange = -ballList[i].XChange;

                // check if ball is out of canvas
                int x = 0;
                if (ballList[i].Position.X - ballList[i].Radius <= 0) x = 1 + ballList[i].Radius;
                if (ballList[i].Position.X + ballList[i].Radius >= canvas.Width) x = (int)canvas.Width - ballList[i].Radius - 1;

                if (x != 0)
                {
                    ballList[i].Position = new Point(x, ballList[i].Position.Y);
                }

            }
            if ((ballList[i].Position.Y - ballList[i].Radius <= 0) || (ballList[i].Position.Y + ballList[i].Radius >= canvas.Height))
            {
                ballList[i].YChange = -ballList[i].YChange;

                // check if ball is out of canvas
                int y = 0;
                if (ballList[i].Position.Y - ballList[i].Radius <= 0) y = 1 + ballList[i].Radius;
                if (ballList[i].Position.Y + ballList[i].Radius >= canvas.Height) y = (int)canvas.Height - ballList[i].Radius - 1;

                if (y != 0)
                {
                    ballList[i].Position = new Point(ballList[i].Position.X, y);
                }
            }
        }

        private void CheckCollision(int i)
        {
            // Collision with user
            UserCollision(i);

            // Collision with balls
            for (int j = i + 1; j < ballList.Count; j++)
            {
                double dx = ballList[j].Position.X - ballList[i].Position.X;
                double dy = ballList[j].Position.Y - ballList[i].Position.Y;
                double dist = Math.Sqrt(dx * dx + dy * dy);
                if (dist < (ballList[j].Radius + ballList[i].Radius))
                {
                    double normalX = dx / dist;
                    double normalY = dy / dist;
                    double midpointX = (ballList[i].Position.X + ballList[j].Position.X) / 2;
                    double midpointY = (ballList[i].Position.Y + ballList[j].Position.Y) / 2;
                    ballList[i].Position = new Point((midpointX - normalX * ballList[i].Radius), (midpointY - normalY * ballList[i].Radius));
                    ballList[j].Position = new Point((midpointX + normalX * ballList[i].Radius), (midpointY + normalY * ballList[i].Radius));

                    double dVector = (ballList[i].XChange - ballList[j].XChange) * normalX;
                    dVector += (ballList[i].YChange - ballList[j].YChange) * normalY;
                    double dvx = dVector * normalX;
                    double dvy = dVector * normalY;

                    if (ballList[i] != ballList[0])
                    {
                        ballList[i].XChange -= dvx;
                        ballList[i].YChange -= dvy;
                    }
                    if (ballList[j] != ballList[0])
                    {
                        ballList[j].XChange += dvx;
                        ballList[j].YChange += dvy;
                    }
                }
            }
        }

        private void CheckSpeed()
        {
            for (int i = 0; i < ballList.Count; i++)
            {
                double offset = 0;

                if (constantSpeed)
                {
                    if (Math.Abs(ballList[i].XChange) + Math.Abs(ballList[i].YChange) != ballList[i].Speed)
                    {
                        if (Math.Abs(ballList[i].XChange) + Math.Abs(ballList[i].YChange) < ballList[i].Speed)
                        {
                            offset = (ballList[i].Speed - (Math.Abs(ballList[i].XChange) + Math.Abs(ballList[i].YChange))) / 2;
                        }
                        else
                        {
                            offset = ((Math.Abs(ballList[i].XChange) + Math.Abs(ballList[i].YChange)) - ballList[i].Speed) / 2;
                            offset = -offset;
                        }
                        if (ballList[i].XChange < 0)
                        {
                            ballList[i].XChange = ballList[i].XChange - offset;
                        }
                        else
                        {
                            ballList[i].XChange = ballList[i].XChange + offset;
                        }
                        if (ballList[i].YChange < 0)
                        {
                            ballList[i].YChange = ballList[i].YChange - offset;
                        }
                        else
                        {
                            ballList[i].YChange = ballList[i].YChange + offset;
                        }
                    }
                }
            }
        }

        private void UserCollision(int i)
        {
            double dx = ballPlayer.Position.X - ballList[i].Position.X;
            double dy = ballPlayer.Position.Y - ballList[i].Position.Y;
            double dist = Math.Sqrt(dx * dx + dy * dy);

            if (dist < (ballList[0].Radius + ballList[i].Radius))
            {
                if (ballList[i].IsEnemy)
                {
                    lives--;
                    totalLivesLabel.Content = lives;
                    spawnBall(ballList[i], ballPlayer);
                    if (lives == 0)
                    {
                        StopGame("You ran out of lives! You died!", "You died!");
                    }                  
                }
                else
                {
                    points++;
                    spawnBall(ballList[i], ballPlayer);

                    if (points < 200)
                    {
                        if (points > 10)
                        {
                            UpdateSpeed(points / 10);
                        }
                        if (points == 50)
                        {
                            constantSpeed = false;
                        }
                    }
                    totalPointsLabel.Content = points;
                }
            }
        }

        private void spawnBall(Ball ball, Ball balls)
        {
            ball.XChange = 0;
            ball.YChange = 0;

            while (ball.XChange == 0)
            {
                ball.XChange = random.Next(-1, 2);
            }
            while (ball.YChange == 0)
            {
                ball.YChange = random.Next(-1, 2);
            }
            ball.XChange = ball.XChange * 2;
            ball.YChange = ball.YChange * 2;

            double dx = balls.Position.X - ball.Position.X;
            double dy = balls.Position.Y - ball.Position.Y;
            double dist = Math.Sqrt(dx * dx + dy * dy);
            double x, y;
            while (dist < (balls.Radius + ball.Radius))
            {
                x = (double)random.Next(1 + ball.Radius, (int)(canvas.Width - ball.Radius - 1));
                y = (double)random.Next(1 + ball.Radius, (int)(canvas.Height - ball.Radius - 1));

                ball.Position = new Point(x, y);
                dx = balls.Position.X - ball.Position.X;
                dy = balls.Position.Y - ball.Position.Y;
                dist = Math.Sqrt(dx * dx + dy * dy);
            }
        }

        private void UpdateSpeed(double speed)
        {
            for (int i = 0; i < ballList.Count; i++)
            {
                ballList[i].Speed = speed;
            }
        }
    }
}

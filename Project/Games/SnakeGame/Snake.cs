﻿// Auther: Joris Meylaers
// Date: 01/04/2015

using Project.Controllers;
using Project.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Project.Games.SnakeGame
{
    class Snake
    {
        private Canvas drawOnCanvas;
        private Label pointsLabel;
        private Label timeLeftLabel;
        private DispatcherTimer snakeTimer;
        private DispatcherTimer moveTimer;
        private DispatcherTimer timeTimer;
        private int timeLeft;
        private int timePlayed;
        private int size = 10;
        private int points = 0;
        private Random random;
        private Rectangle food;
        private int foodX;
        private int foodY;
        private List<SnakePart> snake;
        private int direction; // Down = 0, Left = 1, Right = 2, Up = 3
        private bool eaten = true;
        private double difficulty = 1;

        /// <summary>
        /// Constructor of the Snake class
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="totalPointsLabel"></param>
        public Snake(Canvas canvas, Label totalPointsLabel, Label timeLeftLabel)
        {
            drawOnCanvas = canvas;
            pointsLabel = totalPointsLabel;
            this.timeLeftLabel = timeLeftLabel;
            random = new Random();
            direction = 0;
            timePlayed = 0;
            snake = new List<SnakePart>();
            SnakePart head = new SnakePart();
            head.X = ((int)(drawOnCanvas.Width / 2) / 10) * 10; // zorgt voor een veelvoud van 10
            head.Y = ((int)(drawOnCanvas.Height / 2) / 10) * 10; // zorgt voor een veelvoud van 10
            snake.Add(head);
            snakeTimer = new DispatcherTimer();
            snakeTimer.Interval = TimeSpan.FromSeconds(0.4 - (difficulty / 10.0));
            snakeTimer.Tick += snakeTimer_Tick;
            moveTimer = new DispatcherTimer();
            moveTimer.Interval = TimeSpan.FromMilliseconds(5);
            moveTimer.Tick += moveTimer_Tick;
            timeTimer = new DispatcherTimer();
            timeTimer.Interval = TimeSpan.FromSeconds(1);
            timeTimer.Tick += timeTimer_Tick;
        }

        /// <summary>
        /// reduce time left
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeTimer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            Console.WriteLine(timeLeft);
            timePlayed++;
            if (timeLeft == 0)
            {
                moveTimer.Stop();
                snakeTimer.Stop();
                timeTimer.Stop();
                SaveAndClose("You ran out of time!", "No time left");
            }
            timeLeftLabel.Content = timeLeft;
        }

        /// <summary>
        /// method used for starting the game
        /// </summary>
        public void startGame()
        {
            drawSnake();
            snakeTimer.Start();
            moveTimer.Start();
            timeTimer.Start();
        }

        /// <summary>
        /// moving the snake and check if it hits something
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void snakeTimer_Tick(object sender, EventArgs e)
        {
            UpdateSnake();
            hit();
        }

        /// <summary>
        /// check if direction has to be changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveTimer_Tick(object sender, EventArgs e)
        {
            if (Input.KeyPressed(Key.Right) && direction != 1)
            {
                direction = 2;
            }
            else if (Input.KeyPressed(Key.Left) && direction != 2)
            {
                direction = 1;
            }
            else if (Input.KeyPressed(Key.Up) && direction != 0)
            {
                direction = 3;
            }
            else if (Input.KeyPressed(Key.Down) && direction != 3)
            {
                direction = 0;
            }
        }

        /// <summary>
        /// generate food on a random location
        /// </summary>
        private void drawFood()
        {
            food = new Rectangle();
            food.Fill = Brushes.Red;
            food.Width = size;
            food.Height = size;
            bool findSpot = true;

            if (eaten)
            {
                while (findSpot)
                {
                    foodX = (random.Next(0, (int)(drawOnCanvas.Width - size)) / 10) * 10;
                    foodY = (random.Next(0, (int)(drawOnCanvas.Height - size)) / 10) * 10;

                    for (int i = 0; i < snake.Count; i++)
                    {
                        if (((foodX >= snake[i].X && (foodX <= snake[i].X + size))
                            && (foodY >= snake[i].Y && (foodY <= snake[i].Y + size)))
                            || foodX == 0 || foodX + size == drawOnCanvas.Width
                            || foodY == 0 || foodY + size == drawOnCanvas.Height)
                        {
                            findSpot = true;
                        }
                        else
                        {
                            findSpot = false;
                        }
                    }
                }
                eaten = false;
            }
            food.Margin = new Thickness(foodX, foodY, 0, 0);
            drawOnCanvas.Children.Add(food);
        }

        /// <summary>
        /// draws the snake
        /// </summary>
        public void drawSnake()
        {
            drawOnCanvas.Children.Clear();
            drawFood();
            for (int i = 0; i < snake.Count; i++)
            {
                Rectangle r = new Rectangle();
                r.Fill = Brushes.Green;
                r.Height = size;
                r.Width = size;
                if (i == 0)
                {
                    r.Margin = new Thickness(snake[i].X, snake[i].Y, 0, 0);
                }
                else
                {
                    switch (direction)
                    {
                        case 0: // Down
                            r.Margin = new Thickness(snake[i].X, snake[i].Y, 0, 0);
                            break;
                        case 1: // Left
                            r.Margin = new Thickness(snake[i].X, snake[i].Y, 0, 0);
                            break;
                        case 2: // Right
                            r.Margin = new Thickness(snake[i].X, snake[i].Y, 0, 0);
                            break;
                        case 3: // Up
                            r.Margin = new Thickness(snake[i].X, snake[i].Y, 0, 0);
                            break;
                    }
                }
                drawOnCanvas.Children.Add(r);
            }
        }

        /// <summary>
        /// moves snake
        /// </summary>
        public void UpdateSnake()
        {
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                // moving head
                if (i == 0)
                {
                    switch (direction)
                    {
                        case 0: // Down
                            snake[i].Y += 10;
                            break;
                        case 1: // Left
                            snake[i].X -= 10;
                            break;
                        case 2: // Right
                            snake[i].X += 10;
                            break;
                        case 3: // Up
                            snake[i].Y -= 10;
                            break;
                    }
                }
                else
                {
                    snake[i].X = snake[i - 1].X;
                    snake[i].Y = snake[i - 1].Y;
                }
            }
            drawSnake();
        }

        /// <summary>
        /// check if snake hits something
        /// </summary>
        private void hit()
        {
            // check hitting borders
            if (snake[0].X < 0 || snake[0].Y < 0
                    || (snake[0].X + size) > drawOnCanvas.Width || (snake[0].Y + size) > drawOnCanvas.Height)
            {
                Dead();
            }

            // check hitting food
            if (snake[0].X == foodX && snake[0].Y == foodY)
            {
                Eat();
            }

            // check hitting snake
            for (int j = 1; j < snake.Count; j++)
            {
                if (snake[0].X == snake[j].X &&
                snake[0].Y == snake[j].Y)
                {
                    // Bij update script word het 2de blokje op de locatie van het eerste
                    // blokje gezet. dus als men het eerste voedsel neemt dan zijn de coördinaten
                    // hetzelfde en ben je dood.
                    //
                    // Daarom moet je pas beginnen te controleren vanaf het 2e
                    if (points > 1)
                    {
                        Dead();
                    }
                }
            }
        }

        /// <summary>
        /// increases level, points and adds a snakePart
        /// </summary>
        private void Eat()
        {
            eaten = true;
            points++;

            if (points == 10)
            {
                difficulty++;
            }
            if (points == 20)
            {
                difficulty++;
            }

            pointsLabel.Content = points;
            SnakePart bodyPart = new SnakePart();
            bodyPart.X = snake[snake.Count - 1].X;
            bodyPart.Y = snake[snake.Count - 1].Y;
            snake.Add(bodyPart);
            drawSnake();
        }

        /// <summary>
        /// method used when user died
        /// </summary>
        public void Dead()
        {
            moveTimer.Stop();
            snakeTimer.Stop();
            timeTimer.Stop();
            SaveAndClose("You died!" + Environment.NewLine + "Score: " + points, "You died");
        }

        /// <summary>
        /// save score and close game
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        private void SaveAndClose(string message, string title)
        {
            // save score
            string[] records = { Convert.ToString(User.Id), Convert.ToString(DateTime.Now), Convert.ToString(timePlayed), Convert.ToString(points) };
            DB.AddRecord(ProjectConfig.SnakeFile, records);

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

        public int TimeLeft
        {
            get { return timeLeft; }
            set { timeLeft = value; }
        }
    }
}

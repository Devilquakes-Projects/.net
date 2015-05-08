using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.Games.BallGame
{
    class PlayerBall : Ball
    {
        public PlayerBall(Canvas canvas, int radius)
        {
            base.IsEnemy = false;
            base.Radius = radius;
            base.Speed = 0;

            Position = new Point(canvas.Width / 2, canvas.Height / 2);
            ColorString = "Black";
            XChange = 0;
            YChange = 0;

            CreateEllipse();
            DrawEllipse(canvas);
        }
    }
}

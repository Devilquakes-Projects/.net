using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Project.Games.BallGame
{
    abstract class Ball: IBall
    {
        private Ellipse ellipse;

        public void CreateEllipse()
        {
            ellipse = new Ellipse();
            ellipse.Width = Radius * 2;
            ellipse.Height = Radius * 2;
        }

        public void DrawEllipse(Canvas canvas)
        {
            ellipse.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorString));
            ellipse.Margin = new Thickness(Position.X - Radius, Position.Y - Radius, 0, 0);
            canvas.Children.Add(ellipse);
        }

        public void UpdateEllipse()
        {
            ellipse.Margin = new Thickness(Position.X - Radius, Position.Y - Radius, 0, 0);
        }

        public double Speed { get; set; }
        
        public bool Hitted { get; set; }
        
        public Point Position { get; set; }
        
        public String ColorString { get; set; }
        
        public int Radius { get; set; }
        
        public double XChange { get; set; }
        
        public double YChange { get; set; }
        
        public bool IsEnemy { get; set; }
    }
}

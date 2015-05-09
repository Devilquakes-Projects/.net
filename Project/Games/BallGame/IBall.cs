using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.Games.BallGame
{
    interface IBall
    {
        void CreateEllipse();

        void DrawEllipse(Canvas canvas);

        void UpdateEllipse();

        double Speed { get; set; }

        bool Hitted { get; set; }

        Point Position { get; set; }

        String ColorString { get; set; }

        int Radius { get; set; }

        double XChange { get; set; }

        double YChange { get; set; }

        bool IsEnemy { get; set; }

    }
}

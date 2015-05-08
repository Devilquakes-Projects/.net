using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Games.BallGame
{
    class ComputerBall : Ball
    {
        public ComputerBall(bool isEnemy, int radius, Random randomNumber)
        {
            base.IsEnemy = isEnemy;
            base.Radius = radius;
            base.Speed = 2;

            if (isEnemy)
            {
                ColorString = "Red";
            }
            else
            {
                ColorString = "Green";
            }

            CreateEllipse();
        }
    }
}
